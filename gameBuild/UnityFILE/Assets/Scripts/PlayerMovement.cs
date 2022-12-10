using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 32f;
    private float jumpingPower = 65f;
    public bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 28f;
    private float dashingTime = 0.15f;
    private float dashingCooldown = 0.4f;
    private float knockbackTime = 0.1f;
    private float knockbackSpeed = 100f;
    private float originalGravity = 13.3f;

    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Jump;
    public KeyCode DashKey;

    private bool doubleJump;
    private bool isHit = false;
    private bool knockbackDirection;
    private int dogup;
    private int catup = 0;
    private bool isGettingKnockedback = false;
    private float selfKnockbackPower = 12f;

    PlayerCombat combat;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    
    public Animator animator;

    

    private void Start()
    {
        combat = this.GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        
        
        
        if (IsGrounded())
        {
            
            animator.SetBool("IsJumping", false);
            
        }
        if (isHit)
        {
            
            if (dogup == 0)
            {
                if (knockbackDirection == true)
                    {
                    rb.velocity = new Vector2(knockbackSpeed, rb.velocity.y);
                    }
                else
                    {
                        rb.velocity = new Vector2(-knockbackSpeed, rb.velocity.y);
                    }
            }
                else if (dogup == 1)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, knockbackSpeed);
                    }
                else if (dogup == 2)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -knockbackSpeed);
                    }
            
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsDashing", false);
            combat.canAttack = false;

        }
        else if (isDashing)
        {
            

            animator.SetFloat("Speed", 0);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsDashing", true);
            doubleJump = true;
            
        }
        else 
        {
            animator.SetBool("IsDashing", false);
            
            //control settings
                if (Input.GetKey(Left))
                {
                    horizontal = -1;
                }

                else if (Input.GetKey(Right))
                {
                    horizontal = 1;
                }

                else if (Input.GetKey(Left) && Input.GetKey(Right))
                {
                    horizontal = 0;
                }
            
                else
                {
                    horizontal = 0;
                }
            
            
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            if (IsGrounded() && !Input.GetKey(Jump))
            {
                doubleJump = false;
            }

            if (Input.GetKeyDown(Jump))
            {
                
                if (IsGrounded() || doubleJump)
                {
                
                animator.SetTrigger("Jump");
                animator.SetBool("IsJumping", true);
                FindObjectOfType<AudioManager>().Play("PlayerJump");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                doubleJump = !doubleJump;
                
                
                }
                
            }

            if (Input.GetKeyUp(Jump) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                
            }

            if (Input.GetKeyDown(DashKey) && canDash)
            {
                StartCoroutine(Dash());
                FindObjectOfType<AudioManager>().Play("PlayerDash");
            }

        Flip();
        }
    }

    private void FixedUpdate()
    {
        //Time.timeScale = 0.1f;

        if (isDashing || isHit)
        {
            return;
        }
        if (catup == 0)
        {

            if (!isGettingKnockedback)
            {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
            else
            {
            rb.velocity = new Vector2(horizontal * speed - selfKnockbackPower * transform.localScale.x , rb.velocity.y);
            }
        }
        else if (catup == 1)
        {
            
            rb.velocity = new Vector2(horizontal * speed, selfKnockbackPower * 3f);
            
        }
        else if (catup == 2)
        {
            rb.velocity = new Vector2(horizontal * speed, -selfKnockbackPower * 0.5f);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        
        yield return new WaitForSeconds(dashingTime);
        
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    
    
    public IEnumerator Knockback(bool right, int updog)
    {
        
        knockbackDirection = right;
        isHit = true;
        dogup = updog;
        
        yield return new WaitForSeconds(knockbackTime);
        
        isHit = false;
        combat.canAttack = true;
    }
    
    public void Reset()
    {
                Debug.Log(originalGravity);

        isHit = false;
        combat.canAttack = true;
        isGettingKnockedback = false;
        catup = 0;
        isDashing = false; 
        canDash = true;
        try {
            
        rb.gravityScale = originalGravity;
        }
        catch{}
        
        combat.Reset();
    }
    public IEnumerator selfKnockback(int upcat)
    {
        isGettingKnockedback = true;
        catup = upcat;
        if (catup == 1)
        {
            doubleJump = true;
        }
        yield return new WaitForSeconds(knockbackTime);
        isGettingKnockedback = false;
        catup = 0;
    }
}
