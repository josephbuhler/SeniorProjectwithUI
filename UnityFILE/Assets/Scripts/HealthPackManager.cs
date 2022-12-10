using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackManager : MonoBehaviour
{
    private float hpRespawnTime = 10f;
    System.Random random = new System.Random();
    public GameObject[] hpSpawnPoints;
    public GameObject hpPrefab;
    private GameObject currentHealthPack;
    private Transform hpSpawnPoint;
    public Vector2 hpSize = new Vector2(0.5f, 0.5f);
    public LayerMask enemyLayers;
    private Vector2 hpPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(hpRespawn());
    }
    
    void Update()
    {

        
        if (currentHealthPack != null)
        {
            try
            {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(hpPosition, hpSize, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
                {
                    
                    enemy.GetComponent<Health>().Heal();
                    FindObjectOfType<AudioManager>().Play("HealthPack");
                    StartCoroutine (hpRespawn());
                    Destroy(currentHealthPack);
                }
            }
            catch{}
        }
    }
    public IEnumerator hpRespawn()
        {
            
            yield return new WaitForSeconds(hpRespawnTime);
            
            var randomIndex = random.Next(0, hpSpawnPoints.Length);
            hpSpawnPoint = hpSpawnPoints[randomIndex].GetComponent<Transform>();
            
            
		    currentHealthPack = Instantiate(hpPrefab, hpSpawnPoint.position, Quaternion.identity);
            hpPosition = new Vector2(currentHealthPack.GetComponent<Transform>().position.x, currentHealthPack.GetComponent<Transform>().position.y);
        }
}
