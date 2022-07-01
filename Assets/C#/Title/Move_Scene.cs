using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Move_Scene : MonoBehaviour, IPointerClickHandler
{
    public string SceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left ||
            eventData.button == PointerEventData.InputButton.Right ||
            eventData.button == PointerEventData.InputButton.Middle)
        {
            Music.instance.ClickSound();
            SceneManager.LoadSceneAsync(SceneName);
        }
    }
}