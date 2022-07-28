using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LightSource : MonoBehaviour
{
    public GameObject Chara;
    public GameObject LightSourceObject;
    public int DropItemID;

    private SpecialItem SpecialItem;
    private readonly SpecialItemDataBase SpecialItemDataBase = Json.instance.SpecialItemDataBase;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("AttackWeapon")) return;

        DropItemStatus.instance.RemoveLightSourceData(gameObject);
        if (!ExpStatus.instance.Probability(50) && DropItemID == 0) return;

        RectTransform RectTransform = gameObject.GetComponent<RectTransform>();

        if (DropItemID == 0)
        {
            SpecialItem = GetRandom(SpecialItemDataBase.GetSpecialItemLists().FindAll(i => i.GetId() != 12));
        } else
        {
            SpecialItem = SpecialItemDataBase.GetSpecialItemLists().Find(i => i.GetId() == DropItemID);
        }
        GameObject Object = new GameObject($"LightSourceDropItem_{SpecialItem}");

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.5f, 0.5f);
        ObjectRectTransform.anchoredPosition = new Vector3(RectTransform.position.x, RectTransform.position.y, 0);

        Image ImageObject = Object.AddComponent<Image>();
        ImageObject.sprite = SpecialItem.GetIcon();
        ImageObject.preserveAspect = true;

        CircleCollider2D CircleCollider2DObject = Object.AddComponent<CircleCollider2D>();
        CircleCollider2DObject.radius = 0.5f;
        CircleCollider2DObject.isTrigger = true;

        VacuumItem ObjectVacuumItem = Object.AddComponent<VacuumItem>();
        ObjectVacuumItem.Chara = Chara;
        ObjectVacuumItem.SpecialItem = SpecialItem;

        Object.transform.SetParent(LightSourceObject.transform);
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
