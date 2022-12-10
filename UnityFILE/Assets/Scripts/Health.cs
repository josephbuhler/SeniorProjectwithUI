using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    public Animator animator;
    private GameObject GM;
    private Image UI;
    public string barName;
    private Respawn respawn;

    
    
    PlayerMovement movement;
    public float maxHealth = 100;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        movement = this.GetComponent<PlayerMovement>();
        UI = GameObject.Find(barName).GetComponent<Image>();
        GM = GameObject.Find("Game Manager");
        currentHealth = maxHealth;
        respawn = GM.GetComponent<Respawn>();
    }

    public void Heal()
    {
        
        currentHealth = currentHealth + 20f;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        UI.rectTransform.localScale = new Vector3 (currentHealth/100, 1.0f, 1.0f);

    }
    public void TakeDamage(int damage, bool right, int updog)
    {
        currentHealth -= damage;
        FindObjectOfType<AudioManager>().Play("PlayerHit");
        UI.rectTransform.localScale = new Vector3 (currentHealth/100, 1.0f, 1.0f);

        // play hurt animation
        animator.SetTrigger("hurt");
        StartCoroutine(this.GetComponent<PlayerMovement>().Knockback(right, updog));
        if(currentHealth <= 0)
        {
            Die();
            
        }
    }
    void Die()
    {
        currentHealth = 100;

        
        //die animation
        
        //trigger respawn
        respawn.startRespawn(gameObject);
        movement.Reset();
        
        //disable the dead
        gameObject.SetActive(false);
        UI.rectTransform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);

    
    }
    
}