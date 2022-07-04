using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundEffect : MonoBehaviour
{
    private Slider GetSlider;
    private float LatestVolume;
	private Json.PlayerData player = Json.instance.Load();

    void Start()
    {
        GetSlider = this.gameObject.GetComponent<Slider>();
        GetSlider.value = player.SoundEffect;
        LatestVolume = player.SoundEffect;
    }

    void Update()
    {
        if (LatestVolume == GetSlider.value) return;

        player.SoundEffect = GetSlider.value;
        Json.instance.Save(player);
    }
}
