using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    public int value = 5;

    void OnCollisionEnter(Collision c)
    {
        var hud = FindFirstObjectByType<TextHud>();
        if (hud)
        {
            hud.AddScore(value);
            hud.SetInfo($"Picked up: +{value}");
        }
        if (TryGetComponent<Rigidbody>(out var rb))
            rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }
}