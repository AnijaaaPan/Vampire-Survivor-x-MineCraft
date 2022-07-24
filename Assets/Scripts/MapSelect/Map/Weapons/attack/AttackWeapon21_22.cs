using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon21_22 : MonoBehaviour
{
    public RectTransform Chara;
    public Weapon weapon;
    public WeaponParn WeaponParn;
    public EnemyData EnemyData;

    private CircleCollider2D CircleCollider2D;

    private GameObject TimeLimitZone;
    private GameObject Lightning;

    private Image ThisImage;
    private Image LightningImage;

    private RectTransform ThisRectTransform;
    private RectTransform TimeLimitZoneTransform;
    private RectTransform LightningRectTransform;

    private Vector2 EnemyPos;

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);

        CircleCollider2D = GetComponent<CircleCollider2D>();

        Lightning = transform.Find("Lightning").gameObject;
        TimeLimitZone = transform.Find("TimeLimitZone").gameObject;

        ThisRectTransform = GetComponent<RectTransform>();
        TimeLimitZoneTransform = TimeLimitZone.GetComponent<RectTransform>();
        LightningRectTransform = Lightning.GetComponent<RectTransform>();

        ThisImage = GetComponent<Image>();
        LightningImage = Lightning.GetComponent<Image>();

        float EnemyX = EnemyData.Object.transform.position.x;
        float EnemyY = EnemyData.Object.transform.position.y;
        EnemyPos = new Vector2(EnemyX, EnemyY);
        transform.position = EnemyPos;

        UpdateAttackZone();
        if (weapon.GetId() == 21)
        {
            StartCoroutine(DropLightningWeaponID21());
        } else
        {
            StartCoroutine(DropLightningWeaponID22());
        }
    }

    private IEnumerator DropLightningWeaponID21()
    {
        LightningRectTransform.position = new Vector2(EnemyPos.x, EnemyPos.y + 10);

        yield return new WaitForSeconds(0.02f);

        TimeLimitZone.SetActive(false);
        ThisImage.color = new Color(1, 1, 1, 0);
        LightningRectTransform.position = new Vector2(EnemyPos.x, EnemyPos.y + 4.25f);
        StartCoroutine(OnOffAttackZone());

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private IEnumerator DropLightningWeaponID22()
    {
        float WeaponSpawnX = Random.Range(Chara.transform.position.x - 10f, Chara.transform.position.x + 10f);
        float WeaponSpawnY = Chara.transform.position.y + 6.05f;
        Vector2 WeaponVector = new Vector2(WeaponSpawnX, WeaponSpawnY);

        float ID22_Radian = GetRadian(WeaponSpawnX, WeaponSpawnY, EnemyPos.x, EnemyPos.y) * (180 / Mathf.PI);

        LightningRectTransform.position = (WeaponVector + EnemyPos) / 2;
        LightningRectTransform.sizeDelta = new Vector2(Vector2.Distance(WeaponVector, EnemyPos) * 1.4f, 0.39f);
        Lightning.transform.Rotate(0, 0, ID22_Radian + 90);

        StartCoroutine(OnOffAttackZone());

        TimeLimitZone.SetActive(true);

        Vector2 InitLocalScale = ThisRectTransform.sizeDelta;
        float ScaleValue = 1f;

        while (true)
        {
            yield return new WaitForSeconds(0.015f);


            if (IsPlaying.instance.isPlay()) {
                LightningImage.color = ScaleValue >= 0.9f ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
                TimeLimitZoneTransform.sizeDelta = InitLocalScale * ScaleValue;

                ScaleValue -= 0.01f;
                if (ScaleValue <= 0.25f)
                {
                    LightningImage.color = new Color(1, 1, 1, 1);
                    LightningRectTransform.sizeDelta = new Vector2(12.5f, 0.39f);
                    Lightning.transform.rotation = Quaternion.Euler(0, 0, 90);
                    StartCoroutine(DropLightningWeaponID21());
                    yield break;
                }
            };
        }
    }

    private void UpdateAttackZone()
    {
        Vector2 InitVector2 = new Vector2(1, 1) * WeaponParn.range;
        ThisRectTransform.sizeDelta = InitVector2;
        CircleCollider2D.radius = 0.5f * WeaponParn.range;
    }

    private IEnumerator OnOffAttackZone()
    {
        CircleCollider2D.enabled = true;
        yield return new WaitForSeconds(0.02f);
        CircleCollider2D.enabled = false;
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }
}
