using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler
{
  public void OnDrop(PointerEventData eventData)
  {
    if (DragHandler.itemBeingDragged != null)
    {
      var draggedButton = DragHandler.itemBeingDragged.GetComponent<Button>();
      var targetButton = GetComponent<Button>();

      if (draggedButton != null && targetButton != null)
      {
        int draggedValue = int.Parse(draggedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        string targetOperation = targetButton.GetComponentInChildren<TextMeshProUGUI>().text;

        var gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
          gameManager.CheckAnswer(targetButton, draggedButton, draggedValue, targetOperation);
        }

        // Return the dragged item to its original position if it's not a correct match
        var dragHandler = draggedButton.GetComponent<DragHandler>();
        if (dragHandler != null && draggedButton.transform.parent == draggedButton.transform)
        {
          draggedButton.transform.position = dragHandler.startPosition; // Use the method to get the start position
        }
      }
    }
  }
}
