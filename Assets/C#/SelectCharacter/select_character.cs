using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class select_character : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Sprite no_select;
    public Sprite select;
    public Image character_Object;
    public Sprite character;
    public GameObject Name_Object;
    public GameObject Description_Object;

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
        if ( eventData.button == PointerEventData.InputButton.Left ) {
            image.sprite = image.sprite == no_select ? select : no_select;

        }
    }
}
