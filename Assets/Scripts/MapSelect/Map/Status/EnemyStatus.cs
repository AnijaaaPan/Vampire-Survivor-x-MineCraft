using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyData
{
    public int id;
    public Enemy enemy;
    public int hp;
    public bool Stop = false;
    public GameObject Object;
    public List<string> Treasure;
}

public class AllEnemyData
{
    public Enemy enemy;
    public int DefeatCount = 0;
}

public class EnemyStatus : MonoBehaviour
{
    static public EnemyStatus instance;

    public Font Font;
    public GameObject DamageList;

    private List<EnemyData> EnemyDataList = new List<EnemyData>();
    private List<AllEnemyData> AllEnemyDataList = new List<AllEnemyData>();

    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        // yield break;

        while (true)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < EnemyDataList.Count; i++)
            {
                EnemyData EnemyData = EnemyDataList[i];
                int value = Random.Range(1, 1000);
                UpdateEenmyDataHp(EnemyData.id, value);
            }
        }
    }

    public List<EnemyData> GetEnemyDataList()
    {
        return EnemyDataList;
    }

    public List<AllEnemyData> GetAllEnemyDataList()
    {
        return AllEnemyDataList;
    }

    public void AddEenmyDataList(int id, Enemy enemy, GameObject EnemyObject, List<string> Treasure = null)
    {
        if (Treasure == null) Treasure = new List<string>();

        RegistrationEnemy(enemy);
        int PlayerLv = PlayerStatus.instance.GetStatus().Lv;

        EnemyData EnemyData = new EnemyData
        {
            id = id,
            enemy = enemy,
            hp = enemy.GetMaxHealth(PlayerLv),
            Stop = false,
            Object = EnemyObject,
            Treasure = Treasure
        };

        EnemyDataList.Add(EnemyData);
    }

    public void RemoveObjectData(GameObject EnemyObject)
    {
        EnemyData EnemyData = EnemyDataList.Find(e => e.Object == EnemyObject);
        if (EnemyData == null) return;

        Destroy(EnemyData.Object);
        EnemyDataList.Remove(EnemyData);
    }

    private void RegistrationEnemy(Enemy enemy)
    {
        if (AllEnemyDataList.Any(e => e.enemy == enemy)) return;

        AllEnemyData AllEnemyData = new AllEnemyData
        {
            enemy = enemy,
            DefeatCount = 0
        };

        AllEnemyDataList.Add(AllEnemyData);
    }

    private Color DamageColor(int damage)
    {
        if (1 <= damage && damage < 25) return new Color(1, 1, 1, 1);
        if (100 <= damage) return new Color(1, 1, 0, 1);

        float Percentage = (damage - 25) / 75;
        return new Color(1, Percentage * 0.8f, 0, 1);
    }

    private void DisplayDamage(int damage, EnemyData EnemyData)
    {
        Json.PlayerData player = Json.instance.Load();
        if (!player.Damage) return;

        RectTransform EnemyDataObjectRectTransform = EnemyData.Object.GetComponent<RectTransform>();
        float EnemySizeX = EnemyDataObjectRectTransform.sizeDelta.x / 3;
        float EnemySizeY = EnemyDataObjectRectTransform.sizeDelta.y / 3;
        float EnemyX = EnemyDataObjectRectTransform.anchoredPosition.x + Random.Range(-EnemySizeX, EnemySizeX);
        float EnemyY = EnemyDataObjectRectTransform.anchoredPosition.y + Random.Range(-EnemySizeY, EnemySizeY);

        GameObject Object = new GameObject("Damage");
        Object.transform.SetParent(DamageList.transform);

        Text ObjectText = Object.AddComponent<Text>();
        ObjectText.text = damage.ToString();
        ObjectText.font = Font;
        ObjectText.color = DamageColor(damage);

        RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(ObjectText.text.Length * 5, 8);
        ObjectRectTransform.anchoredPosition = new Vector3(EnemyX, EnemyY, 0);
        ObjectRectTransform.localScale = new Vector3(0.05f / 1, 0.05f / 1);

        DamageColor ObjectDamageColor = Object.AddComponent<DamageColor>();
        ObjectDamageColor.Object = Object;
        ObjectDamageColor.Text = ObjectText;
    }

    public void UpdateEenmyDataHp(int id, int damage)
    {
        EnemyData EnemyData = EnemyDataList.Find(e => e.id == id);
        EnemyData.hp -= damage;

        DisplayDamage(damage, EnemyData);
        if (EnemyData.hp <= 0)
        {
            PlayerStatus.instance.UpdateDefeatCount();

            Destroy(EnemyData.Object.GetComponent<EnemyMoveToChara>());
            Destroy(EnemyData.Object.GetComponent<Rigidbody2D>());
            Destroy(EnemyData.Object.GetComponent<CircleCollider2D>());
            EnemyData.Object.AddComponent<KillMob>();
            ExpStatus.instance.CreateExpObject(EnemyData.Object, EnemyData.enemy.GetExp());
            if (EnemyData.Treasure.Count != 0)
            {
                DropItemStatus.instance.CreateTresure(EnemyData.Object, EnemyData.Treasure);
            }

            AllEnemyDataList.Find(e => e.enemy == EnemyData.enemy).DefeatCount++;
            EnemyDataList.Remove(EnemyData);
        }
    }
}
