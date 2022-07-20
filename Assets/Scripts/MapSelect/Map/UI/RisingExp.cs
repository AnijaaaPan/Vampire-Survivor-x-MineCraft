using UnityEngine;

public class RisingExp : MonoBehaviour
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
        transform.position += Vector3.up * 6f * Time.deltaTime;
        transform.localScale -= new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime);
    }
}
