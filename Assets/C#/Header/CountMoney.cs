using UnityEngine;
using UnityEngine.UI; // Text関連を操作するときに必要な名前空間

public class CountMoney : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { 
        Json.PlayerData player = Json.instance.Load();
        gameObject.GetComponent<Text>().text = player.Money.ToString();
    }
}
