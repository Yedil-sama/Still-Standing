using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float rotationSpeed = 0.05f;
    protected float rotateVelocity;

    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    protected virtual void MoveToPosition(Vector3 position) { }
    protected virtual void MoveToTarget(GameObject target) { }

    protected virtual void Rotate(Vector3 lookAtPosition)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotationSpeed * Time.deltaTime * 5);
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

}