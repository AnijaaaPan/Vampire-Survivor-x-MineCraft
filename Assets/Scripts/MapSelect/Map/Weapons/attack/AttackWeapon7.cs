using UnityEngine;

public class AttackWeapon7 : MonoBehaviour
{
    public RectTransform Chara;

    private Rigidbody2D rb;
    private readonly static float INIT_SPEED = 7.5f;

    void Start()
    {
        float INIT_DEGREE = Random.Range(50f, 130f);;

        Vector3 vel = Vector3.zero;
        vel.y = INIT_SPEED;
        vel.x = INIT_SPEED * Mathf.Cos(INIT_DEGREE * Mathf.PI / 180f);
        vel.y = INIT_SPEED * Mathf.Sin(INIT_DEGREE * Mathf.PI / 180f);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = vel;
    }

    void Update()
    {
        if (!IsPlaying.instance.isPlay())
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        };

        float MoveX = transform.position.x;
        float MoveY = transform.position.y;

        float CameraLeftX = Chara.transform.position.x - 10.75f;
        float CameraRightX = Chara.transform.position.x + 10.75f;
        float CameraTopY = Chara.transform.position.y + 6.05f;
        float CameraBottomY = Chara.transform.position.y - 6.05f;

        if (MoveX <= CameraLeftX || CameraRightX <= MoveX ||
            MoveY <= CameraBottomY || CameraTopY <= MoveY) Destroy(gameObject);
    }
}
