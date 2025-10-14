using UnityEngine;

public class MoverPingPong : MonoBehaviour
{
    public Vector3 localAxis = Vector3.right;
    public float distance = 2f;
    public float speed = 1f;
    Vector3 start;

    void Start() => start = transform.position;

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f; // 0..1
        transform.position = start + localAxis.normalized * (t * distance);
    }
}