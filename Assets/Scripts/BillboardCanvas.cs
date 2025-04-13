using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    private Transform cameraTransform;
    public void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    public void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.rotation * -Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
