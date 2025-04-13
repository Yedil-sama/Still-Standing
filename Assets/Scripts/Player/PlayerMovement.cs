using UnityEngine;

public class PlayerMovement : Movement
{
    [Header("Enemy Targetting")]
    public GameObject target;
    public float stoppingDistance = 1.5f;
    [SerializeField] private HighlightManager highlightManager;

    public Animator anim;
    private float motionSmoothTime = 0.1f;
    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        Move();
        Animation();
    }
    public void Animation()
    {
        float normalizedSpeed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", normalizedSpeed, motionSmoothTime, Time.deltaTime);
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Ground")
                {
                    MoveToPosition(hit.point);
                }
                else if (hit.collider.tag == "Enemy" && hit.collider.GetComponent<Character>() != null)
                {
                    MoveToTarget(hit.collider.gameObject);
                }

            }
        }

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > stoppingDistance)
            {
                agent.SetDestination(target.transform.position);
            }
        }
    }
    protected override void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        agent.stoppingDistance = 0;

        Rotate(position);

        if (target != null)
        {
            highlightManager.DeselectHighlight();
            target = null;
        }
        else if (position.y >= 0.1f)
        {
            target = null;
        }
    }
    protected override void MoveToTarget(GameObject enemy)
    {
        target = enemy;
        agent.SetDestination(enemy.transform.position);
        agent.stoppingDistance = stoppingDistance;

        Rotate(target.transform.position);
        highlightManager.SelectedHighlight();
    }
}
