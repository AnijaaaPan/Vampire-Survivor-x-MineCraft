using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class backmenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left ||
            eventData.button == PointerEventData.InputButton.Right ||
            eventData.button == PointerEventData.InputButton.Middle)
        {
            Music.instance.ClickSound();
            SceneManager.LoadSceneAsync("Title");
        }
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        Music.instance.ClickSound();
        SceneManager.LoadSceneAsync("Title");
    }
}