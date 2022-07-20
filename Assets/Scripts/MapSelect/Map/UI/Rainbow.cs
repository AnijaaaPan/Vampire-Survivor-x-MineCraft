using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    public float a = 1f;
    public float light = 1f;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.color = Color.HSVToRGB(3f * Time.time % 1, light, 1);
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
    }
}