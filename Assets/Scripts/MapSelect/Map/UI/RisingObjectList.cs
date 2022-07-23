using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RisingObjectList : MonoBehaviour
{
    static public RisingObjectList instance;

    public GameObject Chara;
    public List<Sprite> ImageList;

    private bool isRisingExp = false;

    private float SpawnLeftX;
    private float SpawnRightX;
    private float SpawnBottomY;
    private float SpawnTopY;

    private float LatestTime = 0;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isRisingExp) return;

        SpawnLeftX = Chara.transform.position.x - 10.75f;
        SpawnRightX = Chara.transform.position.x + 10.75f;
        SpawnBottomY = Chara.transform.position.y - 8;
        SpawnTopY = Chara.transform.position.y - 6;

        for (int i = 0; i <= 15; i++)
        {
            CraeteRisingExpObject();
        }

        if (Time.time - LatestTime >= 1 && LatestTime != 0)
        {
            LatestTime = 0;
            DestroyChildren();
        }
    }

    public void onRisingExp()
    {
        isRisingExp = true;
        this.gameObject.SetActive(isRisingExp);
    }

    public void offRisingExp()
    {
        LatestTime = Time.time;
        isRisingExp = false;
        this.gameObject.SetActive(isRisingExp);
    }

    private void CraeteRisingExpObject()
    {
        int id = Random.Range(0, 4);
        float SpawnX = Random.Range(SpawnLeftX, SpawnRightX);
        float SpawnY = Random.Range(SpawnBottomY, SpawnTopY);

        GameObject Object = new GameObject("RisingExp");
        Object.transform.position = new Vector3(SpawnX, SpawnY, 0);
        Object.transform.SetParent(this.gameObject.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = new Vector2(0.75f, 0.75f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ImageList[id];
        ObjectImage.preserveAspect = true;

        Object.AddComponent<RisingExp>();
        RotAmimation ObjectRotAmimation = Object.AddComponent<RotAmimation>();
        ObjectRotAmimation.angle = 10;
        ObjectRotAmimation.isImportant = true;
    }

    public void DestroyChildren()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
}
