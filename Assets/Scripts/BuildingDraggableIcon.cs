using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingDraggableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private Transform parent;
    private RectTransform rectTransform;
    private RectTransform canvasRectTransform;
    private Vector2 safePosition;

    private BuildingPlacementRaycast raycast;

    void Awake()
    {
        image = GetComponent<Image>();
        parent = transform.parent;
        rectTransform = GetComponent<RectTransform>();
        canvasRectTransform = FindFirstObjectByType<Canvas>().GetComponent<RectTransform>();

        raycast = GetComponent<BuildingPlacementRaycast>();
        raycast.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        safePosition = rectTransform.anchoredPosition;
        transform.SetParent(canvasRectTransform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        raycast.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 mousePosition
        );

        rectTransform.anchoredPosition = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent);
        rectTransform.anchoredPosition = safePosition;
        image.raycastTarget = true;
        raycast.enabled = false;
    }
}
