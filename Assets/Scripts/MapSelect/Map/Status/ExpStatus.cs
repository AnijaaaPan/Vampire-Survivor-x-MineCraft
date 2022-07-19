using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExpData
{
    public GameObject Object;
    public float X;
    public float Y;
    public float EXP;
}

public class ExpStatus : MonoBehaviour
{
    static public ExpStatus instance;

    public Sprite ExpOrb_Lv1;
    public Sprite ExpOrb_Lv2;
    public Sprite ExpOrb_Lv3;

    public GameObject Chara;
    public GameObject ExpOrbList;

    public AudioClip GetExpSound;

    private int CountDisplay;
    private int MaxCountDisplay = 400;
    private ExpData LatestMaxExpData;
    private List<ExpData> ExpDataList = new List<ExpData>();

    private void Awake()
    {
        instance = this;
    }

    public void CreateExpObject(GameObject EnemyObject, float EXP)
    {
        if (!Probability(60)) return;

        CountDisplay = CountDisplayExp();
        if (CountDisplay > MaxCountDisplay)
        {
            LatestMaxExpData.EXP += EXP;
            return;
        }

        RectTransform EnemyDataObjectRectTransform = EnemyObject.GetComponent<RectTransform>();
        float EnemyX = EnemyDataObjectRectTransform.anchoredPosition.x;
        float EnemyY = EnemyDataObjectRectTransform.anchoredPosition.y;

        GameObject Object = new GameObject("DamageEffect");
        Object.transform.SetParent(ExpOrbList.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = CountDisplay == MaxCountDisplay ? new Vector2(0.5f, 0.5f) : new Vector2(0.3f, 0.3f);
        ObjectRectTransform.anchoredPosition = new Vector3(EnemyX, EnemyY, 0);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ReturnExpimage(EXP);
        ObjectImage.preserveAspect = true;

        CircleCollider2D ObjectCircleCollider2D = Object.AddComponent<CircleCollider2D>();
        ObjectCircleCollider2D.radius = 0.15f;
        ObjectCircleCollider2D.isTrigger = true;

        VacuumItem ObjectVacuumItem = Object.AddComponent<VacuumItem>();
        ObjectVacuumItem.Chara = Chara;

        AddExpDataList(Object, EXP);
    }

    public void GetExpOrb(GameObject Object)
    {
        Music.instance.SoundEffect(GetExpSound);

        ExpData ExpData = ExpDataList.Find(e => e.Object == Object);
        int GetExp = Mathf.FloorToInt(ExpData.EXP);
        PlayerStatus.instance.UpdateExpStatus(GetExp);

        ExpDataList.Remove(ExpData);
        Destroy(Object);
    }

    public void AddExpDataList(GameObject Object, float EXP)
    {
        ExpData ExpData = new ExpData
        {
            Object = Object,
            X = Object.transform.position.x,
            Y = Object.transform.position.y,
            EXP = EXP,
        };

        if (CountDisplay == MaxCountDisplay)
        {
            LatestMaxExpData = ExpData;
        };

        ExpDataList.Add(ExpData);
    }

    private int CountDisplayExp()
    {
        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;
        return ExpDataList.FindAll(e => CameraLeftX <= e.X && e.X <= CameraRightX && CameraBottomY <= e.Y && e.Y <= CameraTopY).Count;
    }

    private Sprite ReturnExpimage(float EXP)
    {
        if (CountDisplay == MaxCountDisplay) return ExpOrb_Lv3;
        if (EXP <= 5) return ExpOrb_Lv1;
        if (EXP <= 25) return ExpOrb_Lv2;
        return ExpOrb_Lv3;
    }

    public bool Probability(float fPercent)
    {
        float fProbabilityRate = UnityEngine.Random.value * 100.0f;

        if (fPercent == 100.0f && fProbabilityRate == fPercent) return true;
        if (fProbabilityRate < fPercent) return true;
        return false;
    }
}
