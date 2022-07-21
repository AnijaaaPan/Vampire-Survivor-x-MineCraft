using UnityEngine;

public class VacuumItem : MonoBehaviour
{
    public GameObject Chara;
    public SpecialItem SpecialItem = null;

    private Transform CharaTransform;
    private Transform ObjectTransform;

    private bool TouchObject = false;
    private int Counter = 0;
    private const string VacuumItemObjectName = "VacuumItem";
    private const string CharaObjectName = "CharaImage";

    void Start()
    {
        CharaTransform = Chara.transform;
        ObjectTransform = this.gameObject.transform;
    }

    void LateUpdate()
    {
        if (!IsPlaying.instance.isPlay()) return;

        UpdateObjectCoordinate();
        CheckOnVacuumEXP();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == VacuumItemObjectName)
        {
            TouchObject = true;
        };

        if (collision.gameObject.name != CharaObjectName) return;
        if (gameObject.name == "DropExp") ExpStatus.instance.GetExpOrb(gameObject);
        if (gameObject.name.Contains("LightSourceDropItem_")) DropItemStatus.instance.GetLightSourceItem(gameObject, SpecialItem);
    }

    private void CheckOnVacuumEXP()
    {
        if (PlayerStatus.instance.GetStatus().OnExpVacuum == 0) return;
        if (gameObject.name != "DropExp") return;

        TouchObject = true;
    }

    private float GetRadian(float x, float y, float x2, float y2)
    {
        return Mathf.Atan2(y2 - y, x2 - x);
    }

    private void UpdateObjectCoordinate()
    {
        if (!TouchObject) return;

        Vector3 CharaPosition = CharaTransform.position;
        Vector3 ObjectPosition = ObjectTransform.position;

        float Radian = GetRadian(CharaPosition.x, CharaPosition.y, ObjectPosition.x, ObjectPosition.y) * (180 / Mathf.PI);
        float sin = Mathf.Sin(Radian * (Mathf.PI / 180));
        float cos = Mathf.Cos(Radian * (Mathf.PI / 180));

        float MathMoveDistanceX = 4.5f * Time.deltaTime * cos;
        float MathMoveDistanceY = 4.5f * Time.deltaTime * sin;
        if (Counter <= 45)
        {
            MathMoveDistanceX = -MathMoveDistanceX;
            MathMoveDistanceY = -MathMoveDistanceY;
            Counter++;
        }

        float MoveX = ObjectTransform.position.x - MathMoveDistanceX;
        float MoveY = ObjectTransform.position.y - MathMoveDistanceY;
        ObjectTransform.position = new Vector3(MoveX, MoveY, 0);
    }
}
