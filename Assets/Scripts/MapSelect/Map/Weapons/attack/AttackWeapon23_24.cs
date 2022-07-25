using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AttackWeapon23_24 : MonoBehaviour
{
    public Weapon weapon;
    public RectTransform Chara;

    public GameObject SpawnEnemy;
    public GameObject ExpOrbList;
    public GameObject TresureBoxs;
    public GameObject StageItems;
    public GameObject LightSource;

    public AudioClip StartSound;
    public AudioClip BeatSound;
    public AudioClip EndSound;

    public CircleCollider2D CircleCollider2D;

    private WeaponParn WeaponParn;

    private GameObject ClockBreak;
    private GameObject EvolutionZone;
    private GameObject AttackZone;

    private Image ClockBreakImage;
    private Image EvolutionZoneImage;

    private Sprite[] Images;
    private int ImageCount;

    private float CameraLeftX;
    private float CameraRightX;
    private float CameraTopY;
    private float CameraBottomY;

    IEnumerator Start()
    {
        WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);

        ClockBreak = transform.Find("ClockBreak").gameObject;
        EvolutionZone = transform.Find("EvolutionZone").gameObject;
        AttackZone = transform.Find("AttackWeaponZone").gameObject;

        ClockBreakImage = ClockBreak.GetComponent<Image>();
        EvolutionZoneImage = EvolutionZone.GetComponent<Image>();

        Images = Resources.LoadAll<Sprite>($"Particle/clock_break/");
        ImageCount = Images.Length - 1;

        while (true)
        {
            if (IsPlaying.instance.isPlay())
            {
                if (weapon.GetId() == 23)
                {
                    StartCoroutine(ShowPentagram());
                }
                else
                {
                    StartCoroutine(ShowAttackZone());
                }
            }

            WeaponParn = WeaponParameter.instance.GetWeaponParameter(weapon);
            yield return new WaitForSeconds(WeaponParn.cooldown);
        }
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        CameraLeftX = Chara.transform.position.x - 10.75f;
        CameraRightX = Chara.transform.position.x + 10.75f;
        CameraTopY = Chara.transform.position.y + 6.05f;
        CameraBottomY = Chara.transform.position.y - 6.05f;

        transform.position = new Vector3(Chara.position.x, Chara.position.y - 0.475f);
    }

    private IEnumerator ShowAttackZone()
    {
        Music.instance.SoundEffect(StartSound);
        EvolutionZone.SetActive(true);

        EvolutionZoneImage.color = new Color(1, 1, 1, 0);
        while (EvolutionZoneImage.color.a <= 0.5f)
        {
            yield return new WaitForSeconds(0.015f);

            EvolutionZoneImage.color += new Color(0, 0, 0, 0.01f);
        }

        yield return new WaitForSeconds(1.15f);

        int Index = 0;
        float WaitTime = 0.55f;
        yield return new WaitForSeconds(WaitTime);

        while (true)
        {
            if (IsPlaying.instance.isPlay())
            {
                Music.instance.SoundEffect(BeatSound);
                StartCoroutine(UpdateAttackZone());

                Index++;
                if (Index == 8) WaitTime = 0.35f;
                if (Index == 12) WaitTime = 0.2f;
                if (Index == 20)
                {
                    Music.instance.SoundEffect(EndSound);
                    StartCoroutine(ShowPentagram());
                    yield break;
                }
            }

            yield return new WaitForSeconds(WaitTime);
        }
    }

    private IEnumerator UpdateAttackZone()
    {
        CreateExplodeZone();

        int Count = 0;
        while (Count < 5)
        {
            yield return new WaitForSeconds(0.015f);

            EvolutionZone.transform.localScale += new Vector3(0.1f, 0.1f);
            EvolutionZoneImage.color += new Color(0, 0, 0, 0.05f);
            Count++;
        }

        while (Count < 10)
        {
            yield return new WaitForSeconds(0.015f);

            EvolutionZone.transform.localScale -= new Vector3(0.1f, 0.1f);
            EvolutionZoneImage.color -= new Color(0, 0, 0, 0.05f);
            Count++;
        }
    }

    private void CreateExplodeZone()
    {
        List<EnemyData> GetEnemyDataList = EnemyStatus.instance.GetEnemyDataList().FindAll(e => {
            Vector3 p = e.Object.transform.position;
            return CameraLeftX <= p.x && p.x <= CameraRightX && CameraBottomY <= p.y && p.y <= CameraTopY;
        });
        if (GetEnemyDataList.Count == 0) return;

        EnemyData EnemyData = GetRandom(GetEnemyDataList);

        GameObject Object = Instantiate(AttackZone);
        Object.transform.position = EnemyData.Object.transform.position;
        Object.transform.SetParent(transform.parent);
        Object.GetComponent<AttackWeaponOnTrigger>().Weapon = weapon;
        Object.SetActive(true);

    }

    private void DeleteScreenItem()
    {
        void DeleteChildObject(GameObject ParantObject)
        {
            for (int i = 0; i < ParantObject.transform.childCount; i++)
            {
                GameObject Object = ParantObject.transform.GetChild(i).gameObject;

                Vector3 p = Object.transform.position;
                if (CameraLeftX <= p.x && p.x <= CameraRightX && CameraBottomY <= p.y && p.y <= CameraTopY) Destroy(Object);
            }
        }

        DeleteChildObject(SpawnEnemy);
        DeleteChildObject(ExpOrbList);
        DeleteChildObject(TresureBoxs);
        DeleteChildObject(StageItems);
        DeleteChildObject(LightSource);
    }

    private IEnumerator VacuumAllExp()
    {
        CircleCollider2D.radius = 1000;
        yield return new WaitForSeconds(0.05f);
        CircleCollider2D.radius = 1;
    }

    private IEnumerator ShowPentagram() {
        ClockBreak.SetActive(true);
        EvolutionZone.SetActive(false);

        if (ExpStatus.instance.Probability(WeaponParn.DropItem) || weapon.GetId() == 24)
        {
            ClockBreakImage.color = new Color(0.652f, 1, 1, 0.5f);

            List<EnemyData> GetEnemyDataList = EnemyStatus.instance.GetEnemyDataList();
            for (int i = 0; i < GetEnemyDataList.Count; i++)
            {
                EnemyData EnemyData = GetEnemyDataList[i];

                Vector3 p = EnemyData.Object.transform.position;
                if (CameraLeftX <= p.x && p.x <= CameraRightX && CameraBottomY <= p.y && p.y <= CameraTopY) {
                    WeaponStatus.instance.WeaponDamage(weapon, WeaponParn.damage);
                    EnemyStatus.instance.UpdateEenmyDataHp(EnemyData.id, (int)WeaponParn.damage);
                };
            }

            if (weapon.GetId() == 24) StartCoroutine(VacuumAllExp());

        } else
        {
            ClockBreakImage.color = new Color(1, 1, 1, 0.5f);
            DeleteScreenItem();
        }

        for (int index = 0; index < ImageCount; index++)
        {
            yield return new WaitForSeconds(0.02f);

            ClockBreakImage.sprite = Images[index];
        }

        if (weapon.GetId() == 24)
        {
            for (int index = ImageCount; index > 0; index--)
            {
                yield return new WaitForSeconds(0.02f);

                ClockBreakImage.sprite = Images[index];
            }

            yield return new WaitForSeconds(0.375f);
        }
        ClockBreak.SetActive(false);
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
