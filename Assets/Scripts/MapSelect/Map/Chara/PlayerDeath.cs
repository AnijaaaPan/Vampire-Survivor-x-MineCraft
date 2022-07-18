using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerDeath : MonoBehaviour
{
    public GameObject GameOver;
    public PlayerData PlayerData;
    public List<Sprite> Angels;

    private float Height = 1.5f;

    private RectTransform RectTransformChara;

    private GameObject EndGame;
    private GameObject Revive;
    private GameObject Angel;
    private GameObject Continuing;

    private Image CharaImage;
    private Image AngelImage;
    private Image ContinuingImage;

    private Button EndGameButton;
    private Button ReviveButton;

    private BoxCollider2D CharaBoxCollider2D;
    private Vector3 CharaPosition;
    private Vector3 InitCharaPosition;

    private bool IsRevive = false;

    private readonly static float DeathSpeed = 0.0075f;
    private readonly static float HalfDeathSpeed = DeathSpeed / 2;
    private readonly static float ReviveSpeed = 0.075f;

    void Start()
    {
        IsPlaying.instance.Stop();
        Music.instance.PauseMusic();
        Setting.instance.ChangeStopButtonCanvas();

        EndGame = GameOver.transform.Find("EndButton").gameObject;
        Revive = GameOver.transform.Find("ReviveButton").gameObject;

        EndGameButton = EndGame.GetComponent<Button>();
        ReviveButton = Revive.GetComponent<Button>();

        EndGame.SetActive(false);
        Revive.SetActive(false);

        Angel = GameOver.transform.Find("Angel").gameObject;
        Continuing = GameOver.transform.Find("Continuing").gameObject;

        CharaImage = GetComponent<Image>();
        AngelImage = Angel.GetComponent<Image>();
        ContinuingImage = Continuing.GetComponent<Image>();

        RectTransformChara = GetComponent<RectTransform>();
        CharaPosition = RectTransformChara.anchoredPosition;
        InitCharaPosition = RectTransformChara.anchoredPosition;

        CharaBoxCollider2D = GetComponent<BoxCollider2D>();
        CharaBoxCollider2D.size = Vector2.zero;

        int DeathCount = PlayerData.DeathCount;
        int RemainReviveCount = ItemStatus.instance.GetAllStatusPhase(16) - DeathCount;

        if (RemainReviveCount == 0)
        {
            ClickEndButton();

        } else
        {
            ClickReviveButton();
        }
    }

        void Update()
    {
        if (GameOver.activeSelf || IsRevive) return;

        Height -= DeathSpeed;
        CharaPosition.y -= HalfDeathSpeed;
        RectTransformChara.anchoredPosition = CharaPosition;
        RectTransformChara.sizeDelta = new Vector2(1.5f, Height);
        if (Height <= 0.05f)
        {
            GameOver.SetActive(true);
        }
    }

    private void ClickEndButton()
    {
        EndGame.SetActive(true);
        EndGameButton.onClick.RemoveAllListeners();
        EndGameButton.onClick.AddListener(() =>
        {
            EndGameButton.onClick.RemoveAllListeners();

            SceneManager.LoadSceneAsync("Title");
            Music.instance.UnPauseMusic();
            Music.instance.PlayRandomMusic();
        });
    }

    private void ClickReviveButton()
    {
        Revive.SetActive(true);
        ReviveButton.onClick.RemoveAllListeners();
        ReviveButton.onClick.AddListener(() =>
        {
            ReviveButton.onClick.RemoveAllListeners();

            IsRevive = true;
            Angel.SetActive(true);
            StartCoroutine(ContinuingGame());
        });
    }

    IEnumerator ContinuingGame()
    {
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[1];
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[2];
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[3];
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[4];
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[5];
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[6];
        ContinuingImage.color = new Color(1, 1, 1, 0.1f);
        yield return new WaitForSeconds(ReviveSpeed);
        AngelImage.sprite = Angels[0];
        Angel.SetActive(false);
        ContinuingImage.color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(ReviveSpeed);
        ContinuingImage.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(ReviveSpeed);
        ContinuingImage.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(ReviveSpeed);
        ContinuingImage.color = new Color(1, 1, 1, 0);
        CharaImage.color = new Color(1, 1, 0, 1);
        ReviveGame();
        yield return new WaitForSeconds(3);
        CharaImage.color = new Color(1, 1, 1, 1);
        yield break;
    }

    private void ReviveGame()
    {
        RectTransformChara.anchoredPosition = InitCharaPosition;
        RectTransformChara.sizeDelta = new Vector2(1.5f, 1.5f);

        CharaBoxCollider2D.size = new Vector2(0.635f, 1);

        GameOver.SetActive(false);

        Music.instance.UnPauseMusic();
        Setting.instance.ChangeStopButtonCanvas();
        IsPlaying.instance.reStart();

        PlayerStatus.instance.UpdateHpStatus(PlayerData.MaxHP / 2);
        PlayerStatus.instance.UpdateDeathCount();
    }
}
