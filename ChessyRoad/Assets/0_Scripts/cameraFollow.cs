using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
    }
}