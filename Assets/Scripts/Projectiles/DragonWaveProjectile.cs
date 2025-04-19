using System.Collections.Generic;
using UnityEngine;

public class DragonWaveProjectile : DirectionalProjectile
{
    public float damageIncreasePerHit = 10f;

    private HashSet<Character> hitCharacters = new HashSet<Character>();

    protected override void OnTriggerEnter(Collider other)
    {
        Character hitCharacter = other.GetComponent<Character>();

        if (hitCharacter == null || hitCharacter == owner) return;

        if (hitCharacters.Contains(hitCharacter)) return;
        Debug.Log(other.name);

        hitCharacters.Add(hitCharacter);
        hitCharacter.ApplyDamage(damage);
        damage.amount += damageIncreasePerHit;
    }
}
