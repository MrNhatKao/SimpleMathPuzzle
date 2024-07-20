using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  public Vector3 startPosition; // Changed to public
  private Transform originalParent;
  public static GameObject itemBeingDragged;

  public void OnBeginDrag(PointerEventData eventData)
  {
    itemBeingDragged = gameObject;
    startPosition = transform.position;
    originalParent = transform.parent;
    GetComponent<CanvasGroup>().blocksRaycasts = false;
  }

  public void OnDrag(PointerEventData eventData)
  {
    transform.position = Input.mousePosition;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    itemBeingDragged = null;
    GetComponent<CanvasGroup>().blocksRaycasts = true;

    if (transform.parent == originalParent)
    {
      transform.position = startPosition;
    }
  }
}
