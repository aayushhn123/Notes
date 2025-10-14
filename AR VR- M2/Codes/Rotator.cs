using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 eulerPerSecond = new Vector3(0f, 90f, 0f);

    void Update()
    {
        transform.Rotate(eulerPerSecond * Time.deltaTime, Space.World);
    }
}