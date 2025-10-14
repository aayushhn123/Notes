using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public Renderer[] colorTargets; // drop objects whose color you want to change
    public Color enterColor = Color.green;
    public Color exitColor = Color.white;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (var r in colorTargets)
            if (r) r.material.color = enterColor; // color change on enter
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (var r in colorTargets)
            if (r) r.material.color = exitColor; // revert on exit
    }
}