using UnityEngine;  // Imports the UnityEngine library, providing access to Unity classes like Debug, Collision, etc.
using TMPro;  // Imports the TMPro namespace, giving access to TextMeshProUGUI for UI text handling.

public class CollisionReporter : MonoBehaviour  // Defines a public class that inherits from MonoBehaviour, allowing it to attach to GameObjects and respond to Unity events.
{
    public TextMeshProUGUI hud;  // Declares a public variable of type TextMeshProUGUI, intended to be assigned in the Inspector to display on-screen messages.

    void OnCollisionEnter(Collision c)  // Unity event method called when this object collides physically with another object (both must have colliders, at least one with Rigidbody).
    {
        Debug.Log($"Player collided with {c.gameObject.name}");  // Logs a message to the Unity Console, using string interpolation to include the name of the collided object.
        if (hud) hud.text = $"Collision with <b>{c.gameObject.name}</b>";  // If hud is assigned, updates its text property with a bolded message using TextMeshProUGUI markup. Replaces the non-existent SetInfo method.
    }

    void OnTriggerEnter(Collider other)  // Unity event method called when this object’s trigger collider is entered by another collider (trigger must have Is Trigger checked).
    {
        Debug.Log($"Entered trigger: {other.gameObject.name}");  // Logs the name of the object entering the trigger to the Console.
        if (hud) hud.text = $"Entered trigger: <b>{other.gameObject.name}</b>";  // If hud is assigned, sets its text to a bolded message. Replaces SetInfo.
    }

    void OnTriggerExit(Collider other)  // Unity event method called when another collider exits this object’s trigger collider.
    {
        Debug.Log($"Exited trigger: {other.gameObject.name}");  // Logs the name of the object exiting the trigger.
        if (hud) hud.text = $"Exited trigger: <b>{other.gameObject.name}</b>";  // If hud is assigned, updates its text with a bolded exit message. Replaces SetInfo.
    }
}