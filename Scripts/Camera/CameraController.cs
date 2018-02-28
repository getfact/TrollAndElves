using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.12f;
    public Vector3 offset;

    void LateUpdate () {

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        transform.position = smoothedPos;
        transform.LookAt(target);
    }
}
