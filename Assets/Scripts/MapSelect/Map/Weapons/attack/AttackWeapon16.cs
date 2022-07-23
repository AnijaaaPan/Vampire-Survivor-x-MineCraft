using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon16 : MonoBehaviour
{
    IEnumerator Start()
    {
        Vector3 InitLocalScale = transform.localScale;
        Image ObjectImage = GetComponent<Image>();

        float ColorValue = 1f;
        float ScaleValue = 1f;
        while (true)
        {
            yield return new WaitForSeconds(0.015f);

            ObjectImage.color = new Color(ObjectImage.color.r, ObjectImage.color.g, ObjectImage.color.b, ColorValue);
            transform.localScale = InitLocalScale * ScaleValue;

            ColorValue -= 0.02f;
            if (ColorValue <= 0) ColorValue = 1f;

            ScaleValue -= 0.01f;
            if (ScaleValue <= 0)
            {
                ScaleValue = 1f;
                ColorValue = 1f;
                transform.localScale = InitLocalScale;
            }
        }
    }
}
