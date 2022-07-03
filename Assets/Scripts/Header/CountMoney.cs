using UnityEngine;
using UnityEngine.UI;

public class CountMoney : MonoBehaviour
{
    private Json.PlayerData player = Json.instance.Load();

    void Start() { 
        gameObject.GetComponent<Text>().text = player.Money.ToString();
    }
}
