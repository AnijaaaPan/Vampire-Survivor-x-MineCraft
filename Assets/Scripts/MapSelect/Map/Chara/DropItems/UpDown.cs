using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float speed = 0.015f;
    public RectTransform ObjectRectTransform;

    void Update()
    {
        ObjectRectTransform.anchoredPosition = new Vector3(0, Mathf.Sin(Time.frameCount * speed) * 0.15f, 0);
    }
}
