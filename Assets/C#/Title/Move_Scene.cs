using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Move_Scene : MonoBehaviour, IPointerClickHandler
{
    public string SceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SceneManager.LoadSceneAsync(SceneName);
        }
    }
}