using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFX01 : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalLocalPosition; // Store the original local position


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalLocalPosition = transform.localPosition; // Store the original local position
    }

    private void Update()
    {
        // Example: Trigger the slash animation on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            TriggerSlashAnimation();
            // Get mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            FlipBasedOnCursor(mousePosition);
        }
    }

    private void TriggerSlashAnimation()
    {
        animator.SetTrigger("Slash"); // Trigger the slash animation using the "SlashTrigger" parameter
    }

    // Method called by animation event to set sprite opacity
    public void SetAnimationSpriteOpacity(float opacity)
    {
        // Assuming your sprite renderer is on the same GameObject as this script
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = opacity;
            spriteRenderer.color = color;
        }
    }

    private void FlipBasedOnCursor(Vector3 mousePosition)
    {
        // Get current object's position
        Vector3 objectPosition = transform.position;

        // Determine direction vector from object to mouse
        Vector3 direction = mousePosition - objectPosition;

        // Calculate half of the sprite's width
        float offsetFlip = spriteRenderer.bounds.size.x * 0.1f;

        // Flip sprite based on direction
        if (direction.x > 0)
        {
            // Mouse is to the right of the object
            spriteRenderer.flipX = false; // No flip
            transform.localPosition = originalLocalPosition; // Reset to original position
        }
        else if (direction.x < 0)
        {
            // Mouse is to the left of the object
            spriteRenderer.flipX = true; // Flip horizontally
            // Adjust position to simulate left pivot flip
            transform.localPosition = originalLocalPosition - new Vector3(offsetFlip, 0, 0);
        }
        // If direction.x == 0, no change in orientation is needed
    }


}
