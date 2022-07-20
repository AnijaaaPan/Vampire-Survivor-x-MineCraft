using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class GetDropItem : MonoBehaviour
{
    public Item item;

    public GameObject Option;
    public GameObject GetDropItemObject;
    public GameObject WeaponItemBar;
    public GameObject Slot_1;

    private GameObject EffectBackGround;
    private Image OptionImage;

    private Button GetButton;
    private Button DontGetButton;

    private void Finnaly()
    {
        Setting.instance.StopButtonCanvasRemove();
        RisingObjectList.instance.offRisingExp();
        IsPlaying.instance.reStart();

        EffectBackGround.SetActive(true);
        OptionImage.color = new Color(0, 0, 0, 0.75f);

        WeaponItemBar.SetActive(true);
        GetDropItemObject.SetActive(false);
        Option.SetActive(false);

        Destroy(gameObject);
    }

    private void UpdateItemDescription()
    {
        ItemData ItemData = ItemStatus.instance.GetStatusList().Find(i => i.item == item);

        GameObject SlotItemObject = Slot_1.transform.Find("SlotItemBackGround").transform.Find("SlotItemImage").gameObject;
        Image SlotItemObjectImage = SlotItemObject.GetComponent<Image>();
        SlotItemObjectImage.sprite = item.GetIcon();

        GameObject NameObject = Slot_1.transform.Find("Name").gameObject;
        Text NameObjectText = NameObject.GetComponent<Text>();
        NameObjectText.text = item.GetName();

        GameObject PhaseObject = Slot_1.transform.Find("Phase").gameObject;
        Text PhaseObjectText = PhaseObject.GetComponent<Text>();
        if (ItemData != null)
        {
            PhaseObjectText.text = $"レベルアップ：{ItemData.phase + 1}";
            PhaseObjectText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            PhaseObjectText.text = "NEW!";
            PhaseObjectText.color = new Color(1, 1, 0, 1);
        }

        GameObject DescriptionObject = Slot_1.transform.Find("Description").gameObject;
        Text DescriptionObjectText = DescriptionObject.GetComponent<Text>();
        DescriptionObjectText.text = item.GetDescription();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("CharaImage")) return;

        IsPlaying.instance.Stop();
        Setting.instance.StopButtonCanvasAdd();

        UpdateItemDescription();

        Option.SetActive(true);
        RisingObjectList.instance.onRisingExp();
        GetDropItemObject.SetActive(true);
        WeaponItemBar.SetActive(false);

        OptionImage = Option.GetComponent<Image>();
        OptionImage.color = new Color(0, 0, 0, 0);

        EffectBackGround = Option.transform.Find("effect_background").gameObject;
        EffectBackGround.SetActive(false);

        GetButton = GetDropItemObject.transform.Find("GetButton").GetComponent<Button>();
        GetButton.onClick.RemoveAllListeners();
        GetButton.onClick.AddListener(() =>
        {
            List<ItemData> ItemDatas = ItemStatus.instance.GetStatusList();
            if (ItemDatas.Any(i => i.item == item))
            {
                ItemStatus.instance.UpdateItemPhase(item);
            }
            else
            {
                ItemStatus.instance.AddItemDataList(item);
                PlayerStatus.instance.GetDropItem(item);
            }
            Finnaly();
        });

        DontGetButton = GetDropItemObject.transform.Find("DontGetButton").GetComponent<Button>();
        DontGetButton.onClick.RemoveAllListeners();
        DontGetButton.onClick.AddListener(() =>
        {
            Finnaly();
        });
    }
}
