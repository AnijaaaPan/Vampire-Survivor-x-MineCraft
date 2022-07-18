using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    static public Setting instance;

    public GameObject Option;
    public GameObject SetOption;
    public GameObject LevelUpOption;
    public GameObject Death;

    public Button EndGame;
    public Button ContinueGame;
    public Button StopButton;

    private bool isOpen = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EndGame.onClick.AddListener(() =>
        {
            Music.instance.ClickSound();
            SceneManager.LoadSceneAsync("Title");
        });

        ContinueGame.onClick.AddListener(() =>
        {
            CloseOption(SetOption);
        });

        StopButton.onClick.AddListener(() =>
        {
            OpenOption(SetOption);
        });
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) ||
            LevelUpOption.activeSelf ||
            Death.activeSelf) return;

        if (!isOpen)
        {
            OpenOption(SetOption);
        } else
        {
            CloseOption(SetOption);
        }
    }

    public void ChangeStopButtonCanvas()
    {
        GameObject Object = StopButton.gameObject;
        Canvas ObjectCanvas = Object.GetComponent<Canvas>();
        if (isOpen)
        {
            if (ObjectCanvas) Destroy(ObjectCanvas);

        } else
        {
            if (!ObjectCanvas)
            {
                ObjectCanvas = Object.AddComponent<Canvas>();
            }
            ObjectCanvas.overrideSorting = true;
            ObjectCanvas.sortingOrder = -2;
        }
    }

    public void OpenOption(GameObject SelectObject)
    {
        Music.instance.ClickSound();
        ChangeStopButtonCanvas();
        ItemStatus.instance.UpdatePlayerEffect();
        ItemStatus.instance.UpdateOptionItemBar();
        WeaponStatus.instance.UpdateOptionWeaponBar();

        isOpen = true;
        IsPlaying.instance.Stop();

        Option.SetActive(true);
        SelectObject.SetActive(true);
    }

    public void CloseOption(GameObject SelectObject)
    {
        Music.instance.ClickSound();
        ChangeStopButtonCanvas();

        isOpen = false;
        IsPlaying.instance.reStart();

        Option.SetActive(false);
        SelectObject.SetActive(false);
    }
}
