using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AttackWeapon19_20_Zone : MonoBehaviour
{
    public WeaponParn WeaponParn;

    private Sprite[] Images;
    private int ImageCount;
    private int ImageIndex = 0;

    IEnumerator Start()
    {
        Images = Resources.LoadAll<Sprite>($"Particle/circle_explosion/");
        ImageCount = Images.Length - 1;

        Image ObjectImage = GetComponent<Image>();
        CircleCollider2D ObjectCircleCollider2D = GetComponent<CircleCollider2D>();

        while (true)
        {
            yield return new WaitForSeconds(0.015f);

            if (IsPlaying.instance.isPlay())
            {
                if (ObjectCircleCollider2D.radius <= 1.25f)
                {
                    ObjectCircleCollider2D.radius += 0.1f;
                }

                ObjectImage.sprite = Images[ImageIndex];
                ImageIndex++;

                if (ImageIndex >= ImageCount) Destroy(gameObject);
            };
        }
    }
}
