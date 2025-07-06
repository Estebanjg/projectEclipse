using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("MeleeAttack"))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        // Play Melee Attack animation
        animator.SetTrigger("MeleeAttack1");
        animator.SetBool("Attacking", true);
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Apply Damage
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name); /* enters name of enemy from array to log */
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage); 
        }
    }

    void FinishAttack()
    {
        animator.SetBool("Attacking", false);
    }

    private void OnDrawGizmosSelected() 
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}