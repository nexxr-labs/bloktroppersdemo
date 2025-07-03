using UnityEngine;
using UnityEngine.EventSystems;

public class DragMovementUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public ThirdPersonMovement player;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 drag = eventData.delta.normalized;
        player.SetUIInput(drag);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        player.SetUIInput(Vector2.zero);
    }
}
