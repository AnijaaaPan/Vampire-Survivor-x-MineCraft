using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointerTitle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer spriteRenderer;
    public Sprite normal;
    public Sprite hover;
    public void OnPointerEnter(PointerEventData eventData)
    {
        spriteRenderer.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        spriteRenderer.sprite = normal;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      