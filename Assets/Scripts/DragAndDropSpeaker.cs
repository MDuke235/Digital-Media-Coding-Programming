using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DragAndDropSpeaker : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float zCoordinate;

    // The invisible walls of your virtual room!
    public float minX = -4.5f;
    public float maxX = 4.5f;
    public float minZ = -4.5f;
    public float maxZ = 4.5f;

    void OnMouseDown()
    {
        // Remember how far away the object is from the camera
        zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        
        // Calculate the offset between the exact mouse click point and the center of the object
        mouseOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        // Find out where the mouse is trying to drag the speaker
        Vector3 targetPosition = GetMouseWorldPosition() + mouseOffset;

        // Clamp the position so it hits an invisible wall and stays on the floor
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        // Move the speaker
        transform.position = targetPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}