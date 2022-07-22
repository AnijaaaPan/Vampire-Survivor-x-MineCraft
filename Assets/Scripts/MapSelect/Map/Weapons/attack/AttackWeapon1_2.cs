using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon1_2 : MonoBehaviour
{
    IEnumerator Start()
    {
        Image ObjectImage = GetComponent<Image>();
        float ColorA = 1f;

        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            ObjectImage.color = new Color(1, 1, 1, ColorA);
            ColorA -= 0.3f;
            if (ColorA <= 0) Destroy(gameObject);
        }
    }
}
