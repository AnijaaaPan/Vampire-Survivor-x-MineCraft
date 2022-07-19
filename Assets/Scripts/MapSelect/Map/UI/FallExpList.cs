using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FallExpList : MonoBehaviour
{
    static public FallExpList instance;

    public GameObject Chara;
    public List<Sprite> ImageList;

    private bool isFallExp = false;

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
        if (!isFallExp) return;

        SpawnLeftX = Chara.transform.position.x - 10.75f;
        SpawnRightX = Chara.transform.position.x + 10.75f;
        SpawnBottomY = Chara.transform.position.y + 6;
        SpawnTopY = Chara.transform.position.y + 8;

        for (int i = 0; i <= 5; i++)
        {
            CraeteFallExpObject();
        }

        if (Time.time - LatestTime >= 1 && LatestTime != 0)
        {
            LatestTime = 0;
            DestroyChildren();
        }
    }

    public void onFallExp()
    {
        isFallExp = true;
        this.gameObject.SetActive(isFallExp);
    }

    public void offFallExp()
    {
        LatestTime = Time.time;
        isFallExp = false;
        this.gameObject.SetActive(isFallExp);
    }

    private void CraeteFallExpObject()
    {
        int id = Random.Range(0, 3);
        float SpawnX = Random.Range(SpawnLeftX, SpawnRightX);
        float SpawnY = Random.Range(SpawnBottomY, SpawnTopY);

        GameObject Object = new GameObject("FallExp");
        Object.transform.position = new Vector3(SpawnX, SpawnY, 0);
        if (id == 2) Object.transform.Rotate(0, 0, Random.Range(0, 360));
        Object.transform.SetParent(this.gameObject.transform);

        RectTransform ObjectRectTransform = Object.AddComponent<RectTransform>();
        ObjectRectTransform.sizeDelta = id == 2 ? new Vector2(0.75f, 0.75f) : new Vector2(0.5f, 0.5f);

        Image ObjectImage = Object.AddComponent<Image>();
        ObjectImage.sprite = ImageList[id];
        ObjectImage.preserveAspect = true;

        Object.AddComponent<FallExp>();
    }

    public void DestroyChildren()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
}
