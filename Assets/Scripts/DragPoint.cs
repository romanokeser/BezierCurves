using UnityEngine;
using UnityEngine.EventSystems;

public class DragPoint : MonoBehaviour, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform= GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta * _canvas.scaleFactor;
    }
}
