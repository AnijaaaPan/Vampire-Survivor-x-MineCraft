using UnityEngine;
using UnityEngine.UI;

public class UpdateOption : MonoBehaviour
{
    public Sprite Check;
    public Sprite UnCheck;

    public Slider SoundEffect;
    public Slider SoundMusic;

    public GameObject ButtonFlash;
    public GameObject ButtonJoyStick;
    public GameObject ButtonDamage;
    public GameObject ButtonFullScreen;

    private readonly Json.PlayerData player = Json.instance.Load();

    void Start()
    {
        SoundEffect.value = player.SoundEffect;
        SoundMusic.value = player.SoundMusic;

        PushButton(ButtonFlash, "Flash");
        PushButton(ButtonJoyStick, "JoyStick");
        PushButton(ButtonDamage, "Damage");
        PushButton(ButtonFullScreen, "FullScreen");
    }

    void Update()
    {
        UpdateSoundEffectVolume();
        UpdateSoundMusicVolume();
    }

    private void UpdateSoundEffectVolume()
    {
        if (player.SoundEffect == SoundEffect.value) return;

        player.SoundEffect = SoundEffect.value;
        Json.instance.Save(player);
        Music.instance.UpdateSoundEffect(player.SoundEffect);
    }

    private void UpdateSoundMusicVolume()
    {
        if (player.SoundMusic == SoundMusic.value) return;

        player.SoundMusic = SoundMusic.value;
        Json.instance.Save(player);
        Music.instance.UpdateSoundMusic(player.SoundMusic);
    }

    private void PushButton(GameObject Object, string OptionType)
    {
        if (Object == null) return;

        GameObject ObjectCheckBox = Object.transform.Find("Check").gameObject;
        Image ObjectImage = ObjectCheckBox.GetComponent<Image>();
        ChangeImage(OptionType, ObjectImage);

        Button ObjectButton = Object.GetComponent<Button>();
        ObjectButton.onClick.AddListener(() => {
            Music.instance.ClickSound();
            UpdatePlayerData(OptionType);
            ChangeImage(OptionType, ObjectImage);
        });
    }

    private void UpdatePlayerData(string OptionType)
    {
        if (OptionType == "Flash") player.Flash = !player.Flash;
        if (OptionType == "JoyStick") player.JoyStick = !player.JoyStick;
        if (OptionType == "Damage") player.Damage = !player.Damage;
        if (OptionType == "FullScreen")
        {
            player.FullScreen = !player.FullScreen;
            Screen.fullScreen = player.FullScreen;
        }
        Json.instance.Save(player);
    }

    private void ChangeImage(string OptionType, Image ObjectImage)
    {
        if (OptionType == "Flash") ObjectImage.sprite = player.Flash ? Check : UnCheck;
        if (OptionType == "JoyStick") ObjectImage.sprite = player.JoyStick ? Check : UnCheck;
        if (OptionType == "Damage") ObjectImage.sprite = player.Damage ? Check : UnCheck;
        if (OptionType == "FullScreen") ObjectImage.sprite = player.FullScreen ? Check : UnCheck;
    }
}

