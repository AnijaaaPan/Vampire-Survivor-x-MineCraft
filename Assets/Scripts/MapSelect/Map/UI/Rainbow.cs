using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.color = Color.HSVToRGB(3f * Time.time % 1, 1, 1);
    }
}