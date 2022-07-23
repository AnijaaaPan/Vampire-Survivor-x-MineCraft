using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    private Image EnemyImage;
    private Sprite[] DamageEffectImages;
    private int ImagePageIndex = 0;
    private readonly static int ImageListCount = 28;

    void Start()
    {
        EnemyImage = GetComponent<Image>();
        DamageEffectImages = Resources.LoadAll<Sprite>($"Particle/damage/");
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        UpdateImagePage();
    }

    private void UpdateImagePage()
    {
        ImagePageIndex++;
        int index = Mathf.FloorToInt(ImagePageIndex / 2.5f);
        if (index <= ImageListCount)
        {
            EnemyImage.sprite = DamageEffectImages[index];
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
