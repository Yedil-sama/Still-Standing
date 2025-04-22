using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public float stoppingDistance = 1.5f;
    public float rotationSpeed = 0.05f;
    protected float rotateVelocity;

    private bool isDashing = false;
    private float dashTime;
    private float dashDistance;
    private Vector3 dashDirection;

    public virtual void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Stop()
    {
        agent.ResetPath();
        agent.velocity = Vector3.zero;
    }

    public virtual void LookAt(Vector3 worldPosition)
    {
        Vector3 direction = worldPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f) return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }

    public virtual void Move(Vector3 destination)
    {
        if (Vector3.Distance(transform.position, destination) > stoppingDistance)
        {
            agent.SetDestination(destination);
        }
        else
        {
            Stop();
        }
    }

    public virtual void Move(Transform target)
    {
        if (target == null) return;

        Move(target.position);
    }

    public virtual void Move(GameObject target) => Move(target.gameObject.transform);

    protected virtual void Rotate(Vector3 lookAtPosition)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotationSpeed * Time.deltaTime * 5);
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void StartDash(float distance, float height, float duration)
    {
        if (isDashing) return;

        isDashing = true;
        dashDistance = distance;
        dashTime = duration;
        dashDirection = transform.forward;

        StartCoroutine(DashCoroutine(height));
    }

    private IEnumerator DashCoroutine(float height)
    {
        float dashProgress = 0f;

        while (dashProgress < dashTime)
        {
            dashProgress += Time.deltaTime;

            float dashSpeed = dashDistance / dashTime;
            Vector3 dashMove = dashDirection * dashSpeed * Time.deltaTime;
            transform.position += dashMove;

            float dashHeight = Mathf.Sin(dashProgress / dashTime * Mathf.PI) * height;
            transform.position = new Vector3(transform.position.x, dashHeight, transform.position.z);

            yield return null;
        }

        isDashing = false;
    }
}
