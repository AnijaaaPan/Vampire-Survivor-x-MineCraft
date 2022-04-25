using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Change_to_SelectCharacter : MonoBehaviour, IPointerClickHandler
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ( eventData.button == PointerEventData.InputButton.Right ) {
            //Debug.Log(name + " Game Object Right Clicked!");
        } else if ( eventData.button == PointerEventData.InputButton.Left ) {
            SceneManager.LoadSceneAsync("SelectCharacter");
        }
    }

}