using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    void Update()
    {
        // Get cursor position in screen space
        Vector3 cursorScreenPosition = Input.mousePosition;

        // Convert cursor position to world space
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);
        cursorWorldPosition.z = 99f; // Ensure the crosshair is on the same z-axis as the game objects

        // Update crosshair position
        transform.position = cursorWorldPosition;
    }
}
