using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [Header("Enemy Targetting")]
    [SerializeField] private HighlightManager highlightManager;

    public Animator anim;
    private float motionSmoothTime = 0.1f;

    public override void Initialize()
    {
        base.Initialize();
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
        if (!GameManager.Instance.isAbilitySelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        Move(hit.point);
                    }
                    else if (hit.collider.tag == "Enemy" && hit.collider.GetComponent<Character>() != null)
                    {
                        Move(hit.collider.gameObject);
                    }
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

    public override void Move(Vector3 position)
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

    public override void Move(GameObject enemy)
    {
        target = enemy;
        agent.SetDestination(enemy.transform.position);
        agent.stoppingDistance = stoppingDistance;

        Rotate(target.transform.position);
        highlightManager.SelectedHighlight();
    }
}
