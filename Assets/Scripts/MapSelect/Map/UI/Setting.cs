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
    public GameObject TreasureSlot;

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
            Death.activeSelf ||
            TreasureSlot.activeSelf) return;

        if (!isOpen)
        {
            OpenOption(SetOption);
        }
        else
        {
            CloseOption(SetOption);
        }
    }

    public void StopButtonCanvasAdd()
    {
        Canvas ObjectCanvas = StopButton.gameObject.GetComponent<Canvas>();
        if (!ObjectCanvas)
        {
            ObjectCanvas = StopButton.gameObject.AddComponent<Canvas>();
        }
        ObjectCanvas.overrideSorting = true;
        ObjectCanvas.sortingOrder = -2;
    }

    public void StopButtonCanvasRemove()
    {
        Canvas ObjectCanvas = StopButton.gameObject.GetComponent<Canvas>();
        if (ObjectCanvas) Destroy(ObjectCanvas);
    }

    public void OpenOption(GameObject SelectObject)
    {
        Music.instance.ClickSound();
        StopButtonCanvasAdd();
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
        StopButtonCanvasRemove();

        isOpen = false;
        IsPlaying.instance.reStart();

        Option.SetActive(false);
        SelectObject.SetActive(false);
    }
}
