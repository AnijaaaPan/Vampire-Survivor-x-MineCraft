using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointerTitle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normal;
    public Sprite hover;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
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