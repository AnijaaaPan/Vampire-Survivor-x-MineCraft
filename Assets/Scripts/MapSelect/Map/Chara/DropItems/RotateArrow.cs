using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RotateArrow : MonoBehaviour
{
    public List<Sprite> ArrowImages;

    private Image ArrowImage;

    private int Index = 0;
    private int Length = 8;
    private readonly static float RotateSpeed = 0.1f;

    void Start()
    {
        ArrowImage = GetComponent<Image>();
        StartCoroutine(nameof(Rotate));
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            yield return new WaitForSeconds(RotateSpeed);

            if (IsPlaying.instance.isPlay()) {
                if (Index == Length) Index = 0;

                ArrowImage.sprite = ArrowImages[Index];
                Index++;
            };
            
        }
    }
}
