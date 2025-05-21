using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingItemInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private Transform parent;
    private RectTransform rectTransform;
    private RectTransform canvasRectTransform;
    private Vector2 safePosition;

    private Building building;
    private BuildingItemPlacement placement;

    void Awake()
    {
        image = GetComponent<Image>();
        parent = transform.parent;
        rectTransform = GetComponent<RectTransform>();
        canvasRectTransform = FindFirstObjectByType<Canvas>().GetComponent<RectTransform>();

        building = parent.GetComponent<BuildingItem>().building;
        placement = GetComponent<BuildingItemPlacement>();
        placement.isSelected = false;
        placement.size = building.info.size;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        safePosition = rectTransform.anchoredPosition;
        transform.SetParent(canvasRectTransform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
        rectTransform.localScale = Vector3.one * 0.5f;
        placement.isSelected = true;
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
        image.color = Color.white;
        rectTransform.localScale = Vector3.one;
        placement.isSelected = false;
        placement.PlaceBuilding(building);
    }
}
