using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // The player's transform
    private float smoothSpeed = 0.10f; // The speed of the camera's smoothing
    private Vector3 offset; // The initial offset from the player
    private float cursorInfluence = 0.10f; // How much the cursor influences the camera position
    private float cameraZPosition = -10f; // Fixed Z position of the camera

    private void FixedUpdate()
    {
        Vector3 desiredPosition = GetDesiredPosition();
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, cameraZPosition);
    }

    private Vector3 GetDesiredPosition()
    {
        Vector3 playerPosition = player.position + offset;
        Vector3 cursorPosition = GetCursorWorldPosition();

        Vector3 directionToCursor = cursorPosition - player.position;
        Vector3 adjustedPosition = playerPosition + directionToCursor * cursorInfluence;

        return adjustedPosition;
    }

    private Vector3 GetCursorWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // Set z to match camera's distance from the player

        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return cursorWorldPosition;
    }
}
