using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MousePointerTitle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer spriteRenderer;
    public Sprite normal;
    public Sprite hover;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.sprite = normal;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      