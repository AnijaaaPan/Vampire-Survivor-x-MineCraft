using UnityEngine;

public class FallExp : MonoBehaviour
{
    void Update()
    {
        UpdatePositionObject();
        if (transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdatePositionObject()
    {
        transform.position += Vector3.down * 5f * Time.deltaTime;
        transform.localScale -= new Vector3(0.4f * Time.deltaTime, 0.4f * Time.deltaTime);
    }
}
