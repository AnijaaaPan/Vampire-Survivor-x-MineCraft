using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject Option;
    public GameObject SetOption;

    public Button EndGame;
    public Button ContinueGame;
    public Button StopButton;

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
            CloseOption();
        });

        StopButton.onClick.AddListener(() =>
        {
            OpenOption();
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

    private void ChangeStopButtonCanvas()
    {
        GameObject Object = StopButton.gameObject;
        if (isOpen)
        {
            Canvas ObjectCanvas = Object.GetComponent<Canvas>();
            if (ObjectCanvas) Destroy(ObjectCanvas);

        } else
        {
            Canvas ObjectCanvas = Object.AddComponent<Canvas>();
            ObjectCanvas.overrideSorting = true;
            ObjectCanvas.sortingOrder = -2;
        }
    }

    private void OpenOption()
    {
        Music.instance.ClickSound();
        ChangeStopButtonCanvas();
        ItemStatus.instance.UpdatePlayerEffect();
        ItemStatus.instance.UpdateOptionItemBar();
        WeaponStatus.instance.UpdateOptionWeaponBar();

        isOpen = true;
        IsPlaying.instance.Stop();

        Option.SetActive(true);
        SetOption.SetActive(true);
    }

    private void CloseOption()
    {
        Music.instance.ClickSound();
        ChangeStopButtonCanvas();

        isOpen = false;
        IsPlaying.instance.reStart();

        Option.SetActive(false);
        SetOption.SetActive(false);
    }
}
