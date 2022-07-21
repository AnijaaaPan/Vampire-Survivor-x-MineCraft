using UnityEngine;

public class FireFritta : MonoBehaviour
{
    public float LatestRadian;
    public float MoveSpeed;

    private float sin;
    private float cos;

    void Start()
    {
        LatestRadian += Random.Range(-10f, 10f);
        sin = Mathf.Sin(LatestRadian * (Mathf.PI / 180));
        cos = Mathf.Cos(LatestRadian * (Mathf.PI / 180));
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay()) return;

        UpdatePositionObject();
        if (transform.localScale.x >= 2f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdatePositionObject()
    {
        transform.position += new Vector3(MoveSpeed * cos * 3f, MoveSpeed * sin * 3f);
        transform.localScale += new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime);
    }
}
