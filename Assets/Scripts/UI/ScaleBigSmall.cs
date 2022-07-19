using UnityEngine;
using System.Collections;

public class ScaleBigSmall : MonoBehaviour
{
    public float Speed;
    public float Max;
    public float Min;
    public float Default = 0.05f;
    public bool Invisible = true;

    private float MaxScale;
    private float MinScale;

    IEnumerator Start()
    {
        MaxScale = transform.localScale.x * Max;
        MinScale = transform.localScale.x * Min;
        Default = transform.localScale.x * Default;

        while (true)
        {
            yield return new WaitForSeconds(Speed);
            if (Invisible)
            {
                transform.localScale += new Vector3(Default, Default);
            } else
            {
                transform.localScale -= new Vector3(Default, Default);
            }

            if (transform.localScale.x <= MinScale) Invisible = true;
            if (transform.localScale.x >= MaxScale) Invisible = false;
        }
    }
}
