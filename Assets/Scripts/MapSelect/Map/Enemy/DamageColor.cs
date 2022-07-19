using UnityEngine;
using UnityEngine.UI;

public class DamageColor : MonoBehaviour
{
    public GameObject Object;
    public Text Text;

    private bool Inversion = true;
    private Vector3 InitPositon;

    void Start()
    {
        InitPositon = transform.position;
    }

    void LateUpdate()
    {
        UpdatePositionObject();

        Text.color = Color.Lerp(Text.color, new Color(0f, 0f, 0f, 0f), 2f * Time.deltaTime);
        if (Text.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdatePositionObject()
    {
        float DifferenceY = Mathf.Abs(InitPositon.y - transform.position.y);

        if (Inversion == true && DifferenceY > 0.1f)
        {
            Inversion = false;
        }

        if (Inversion)
        {
            transform.position += Vector3.up * 0.3f * Time.deltaTime;
            transform.localScale += new Vector3(0.075f * Time.deltaTime, 0.075f * Time.deltaTime);
        }
        else
        {
            transform.position += Vector3.down * 0.125f * Time.deltaTime;
            transform.localScale -= new Vector3(0.05f * Time.deltaTime, 0.05f * Time.deltaTime);
        }
    }
}
