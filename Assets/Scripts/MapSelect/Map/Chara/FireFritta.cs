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
        if (transform.localScale.x >= 3f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdatePositionObject()
    {
        transform.position += new Vector3(MoveSpeed * cos * 3f, MoveSpeed * sin * 2.5f);
        transform.localScale += new Vector3(3f * Time.deltaTime, 3f * Time.deltaTime);
    }
}
