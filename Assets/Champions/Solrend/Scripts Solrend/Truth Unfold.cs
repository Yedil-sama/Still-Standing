using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player Ability/Truth Unfold", fileName = "Truth Unfold")]
public class TruthUnfold : PlayerAbility
{
    public float dashDistance;
    public float dashHeight;
    public float dashDuration;
    public float damageRadius;

    private bool isDashing = false;
    private float dashProgress;

    public override void Activate()
    {
        StartDash();
        base.Activate();
    }

    private void StartDash()
    {
        owner.StartCoroutine(DashCoroutine(dashDistance, dashHeight, dashDuration));
        isDashing = true;
        dashProgress = 0f;
    }

    private IEnumerator DashCoroutine(float distance, float height, float duration)
    {
        float dashProgress = 0f;

        Vector3 dashDirection = owner.transform.forward;

        while (dashProgress < 1f)
        {
            dashProgress += Time.deltaTime / duration;
            Vector3 dashMove = dashDirection * (distance * Time.deltaTime / duration);
            owner.transform.position += dashMove;

            float dashHeightValue = Mathf.Sin(dashProgress * Mathf.PI) * height;
            owner.transform.position = new Vector3(owner.transform.position.x, dashHeightValue, owner.transform.position.z);

            yield return null;
        }

        EndDash();
    }

    private void EndDash()
    {
        ApplyDamageInArea();
        isDashing = false;
    }

    private void ApplyDamageInArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(owner.transform.position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Character>(out Character enemy) && enemy != owner)
            {
                owner.DealDamage(new Damage(baseAmount + owner.attackDamage.Current * attackDamageScale + owner.spellDamage.Current * spellDamageScale, DamageType.True), enemy);
            }
        }
    }
}
