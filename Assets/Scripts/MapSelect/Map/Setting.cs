using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject Option;
    public GameObject SetOption;
    public GameObject JoyStick;
    public Button EndGame;
    public Button ContinueGame;

    private bool isOpen = false;

    void Start()
    {
        EndGame.onClick.AddListener(() =>
        {
            Music.instance.ClickSound();
            SceneManager.LoadSceneAsync("Title");
        });

        ContinueGame.onClick.AddListener(() =>
        {
            Music.instance.ClickSound();
            CloseOption();
        });
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (!isOpen)
        {
            OpenOption();
        } else
        {
            CloseOption();
        }
    }

    private void OpenOption()
    {
        Music.instance.ClickSound();
        isOpen = true;
        isPlaying.instance.Stop();

        Option.SetActive(true);
        SetOption.SetActive(true);
        JoyStick.SetActive(false);
    }

    private void CloseOption()
    {
        Music.instance.ClickSound();
        isOpen = false;
        isPlaying.instance.reStart();

        Option.SetActive(false);
        SetOption.SetActive(false);
        JoyStick.SetActive(true);
    }
}
