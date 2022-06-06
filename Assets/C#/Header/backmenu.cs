using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class backmenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log(name + " Game Object Right Clicked!");
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            SceneManager.LoadSceneAsync("Title");
        }
    }

}