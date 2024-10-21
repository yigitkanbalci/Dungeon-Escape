using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canAttack = true;  // Global attack cooldown.
    private HashSet<Collider2D> hitTargets = new HashSet<Collider2D>();  // Track hit targets during one swing.

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Log the target for debugging.

        // Check if the target implements IDamagable.
        IDamagable hit = other.GetComponent<IDamagable>();

        if (hit != null && canAttack && !hitTargets.Contains(other))
        {
            // Damage the target and add it to the hit list.
            hit.Damage();
            hitTargets.Add(other);

            // Prevent further attacks until cooldown is complete.
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        // Wait for 1 second before resetting the attack.
        yield return new WaitForSeconds(0.5f);

        // Reset attack state.
        canAttack = true;
        hitTargets.Clear();  // Clear hit targets for the next swing.
    }
}
