using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform UpAttackPoint;
    public Transform DownAttackPoint;
    public Transform SideAttackPoint;
    public Transform SideAttackPoint2;
    public LayerMask enemyLayers;

    public float UpAttackRange = 0.5f;
    public float DownAttackRange = 0.5f;
    public float sideAttackRange = 0.5f;
    public int attackDamage = 15;

    public KeyCode UpAttack;
    public KeyCode DownAttack;
    public KeyCode Attack;

    private int direction = 3;
    public bool canAttack = true;
    private bool isAttacking;
    private bool alreadyHit;
    

    private float attackingTime = 0.15f;
    private float attackingCooldown = 0.4f;
    // Update is called once per frame


    void Update()
    {
        if (isAttacking)
        {
            if (direction == 0)
            {
                
                animator.SetTrigger("sideAttack");
                if (alreadyHit == false)
                {
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(SideAttackPoint.position, sideAttackRange, enemyLayers);
                        foreach(Collider2D enemy in hitEnemies)
                            {
                               
                                enemy.GetComponent<Health>().TakeDamage(attackDamage, this.GetComponent<PlayerMovement>().isFacingRight, 0);
                                StartCoroutine(this.GetComponent<PlayerMovement>().selfKnockback(0));
                                 alreadyHit = true;
                                
                                 
                            }
                    if (alreadyHit == false)
                {
                    
                    hitEnemies = Physics2D.OverlapCircleAll(SideAttackPoint2.position, sideAttackRange, enemyLayers);
                        foreach(Collider2D enemy in hitEnemies)
                            {
                                
                                enemy.GetComponent<Health>().TakeDamage(attackDamage, this.GetComponent<PlayerMovement>().isFacingRight, 0);
                                StartCoroutine(this.GetComponent<PlayerMovement>().selfKnockback(0));
                                 alreadyHit = true;
                            }

                }
                }
            }

            if (direction == 2)
            {
                animator.SetTrigger("upAttack");
                if (alreadyHit == false)
                {
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(UpAttackPoint.position, UpAttackRange, enemyLayers);
                        foreach(Collider2D enemy in hitEnemies)
                            {
                                enemy.GetComponent<Health>().TakeDamage(attackDamage, this.GetComponent<PlayerMovement>().isFacingRight, 1);
                                StartCoroutine(this.GetComponent<PlayerMovement>().selfKnockback(2));
                                 alreadyHit = true;
                            }
                }
                
            }

            if (direction == 1)
            {
                animator.SetTrigger("downAttack");
                if (alreadyHit == false)
                {
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DownAttackPoint.position, DownAttackRange, enemyLayers);
                        foreach(Collider2D enemy in hitEnemies)
                            {
                                enemy.GetComponent<Health>().TakeDamage(attackDamage, this.GetComponent<PlayerMovement>().isFacingRight, 2);
                                StartCoroutine(this.GetComponent<PlayerMovement>().selfKnockback(1));
                                 alreadyHit = true;
                            }
                }
            }
            
        }
        else if (canAttack)
        {
            if (Input.GetKey(UpAttack) && Input.GetKeyDown(Attack))
            {
                
                direction = 2;
                StartCoroutine(AttackCooldown());
            }
            else if (Input.GetKey(DownAttack) && Input.GetKeyDown(Attack))
            {
                
                direction = 1;
                StartCoroutine(AttackCooldown());
            }
        
            else if (Input.GetKeyDown(Attack))
            {
                
                direction = 0;
                StartCoroutine(AttackCooldown());
            }
        }
        
    }

    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        isAttacking = true;
        alreadyHit = false;
        
        

        yield return new WaitForSeconds(attackingTime);
        isAttacking = false;
        
        yield return new WaitForSeconds(attackingCooldown);
        canAttack = true;
        
        
    }

    public void Reset()
    {
    isAttacking = false;
    canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if (SideAttackPoint == null)
        
        return;
        
        Gizmos.DrawWireSphere(SideAttackPoint.position, sideAttackRange);
        if (UpAttackPoint == null)
        
        return;
        Gizmos.DrawWireSphere(SideAttackPoint2.position, sideAttackRange);
        if (UpAttackPoint == null)
        
        return;
        
        Gizmos.DrawWireSphere(UpAttackPoint.position, UpAttackRange);
        if (DownAttackPoint == null)
        
        return;
        
        Gizmos.DrawWireSphere(DownAttackPoint.position, DownAttackRange);
    }
}
