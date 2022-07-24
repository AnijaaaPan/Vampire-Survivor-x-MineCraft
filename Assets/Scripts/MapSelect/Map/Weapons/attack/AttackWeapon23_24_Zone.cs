using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon23_24_Zone : MonoBehaviour
{
    private Sprite[] Images;
    private int ImageCount;
    private int ImageIndex = 0;

    IEnumerator Start()
    {
        Images = Resources.LoadAll<Sprite>($"Particle/circle_explosion/");
        ImageCount = Images.Length / 2;

        Image ObjectImage = GetComponent<Image>();
        CapsuleCollider2D ObjectCapsuleCollider2D = GetComponent<CapsuleCollider2D>();

        while (true)
        {
            yield return new WaitForSeconds(0.005f);

            if (IsPlaying.instance.isPlay())
            {

                if (ObjectCapsuleCollider2D.size.x <= 3)
                {
                    ObjectCapsuleCollider2D.size += new Vector2(0.035f, 0.035f);
                }

                ObjectImage.sprite = Images[ImageIndex];
                ImageIndex++;

                if (ImageIndex >= ImageCount) Destroy(gameObject);
            };
        }
    }
}
