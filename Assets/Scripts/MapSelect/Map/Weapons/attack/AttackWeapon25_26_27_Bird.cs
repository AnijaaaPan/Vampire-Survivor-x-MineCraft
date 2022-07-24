using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackWeapon25_26_27_Bird : MonoBehaviour
{
    public RectTransform Chara;
    public string MobName;
    public float BirdX;

    private Sprite[] Images;
    private int ImageCount;
    private int ImageIndex = 0;

    IEnumerator Start()
    {
        Images = Resources.LoadAll<Sprite>($"Enemy/{MobName}/");
        ImageCount = Images.Length - 1;

        Image ObjectImage = GetComponent<Image>();
        while (true)
        {
            yield return new WaitForSeconds(0.015f);

            if (IsPlaying.instance.isPlay())
            {
                transform.position = new Vector3(Chara.position.x + BirdX, Chara.position.y + 0.65f);

                ObjectImage.sprite = Images[ImageIndex];
                ImageIndex++;

                if (ImageIndex >= ImageCount) ImageIndex = 0;
            };
        }
    }
}
