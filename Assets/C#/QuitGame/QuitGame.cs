using UnityEngine;
using UnityEngine.EventSystems;


public class QuitGame : MonoBehaviour, IPointerClickHandler
{
    void Quit() {
        #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
          UnityEngine.Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log(name + " Game Object Right Clicked!");
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            Quit();
        }
    }
}
