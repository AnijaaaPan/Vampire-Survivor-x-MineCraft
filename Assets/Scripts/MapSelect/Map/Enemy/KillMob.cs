using UnityEngine;
using UnityEngine.UI;

public class KillMob : MonoBehaviour
{
    private Image EnemyImage;

    private Sprite[] KillImageList;
    private int KillImagePageIndex = 0;
    private readonly static int KillImageListCount = 60;

    void Start()
    {
        EnemyImage = GetComponent<Image>();
        EnemyImage.color = Color.white;

        KillImageList = Resources.LoadAll<Sprite>($"Particle/killed/");
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        UpdateImagePage();
    }

    private void UpdateImagePage()
    {
        KillImagePageIndex++;
        if (KillImagePageIndex <= KillImageListCount)
        {
            EnemyImage.sprite = KillImageList[KillImagePageIndex];
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
