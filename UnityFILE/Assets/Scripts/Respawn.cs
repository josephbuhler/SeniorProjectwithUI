using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Respawn : MonoBehaviour
{
    private float respawnTime = 4f;
    System.Random random = new System.Random();
    public GameObject[] spawnPoints;
    private Transform spawnPoint;
    
    private GameObject originalPlayer;
    private int counter1 = 1;
    private int counter2 = 1;



    
    public void startRespawn(GameObject player)
    {
        StartCoroutine(timeRespawn(player));
    }

    private void win()
    {
        SceneManager.LoadScene("gameover");

    }
    public IEnumerator timeRespawn(GameObject player)
        {
            Debug.Log("originalPlayer");
            if (originalPlayer == null)
            {
                
                originalPlayer = player;
            } 
            Debug.Log(originalPlayer);
            if (player == originalPlayer)
            {
                counter1--;

                if (counter1 == 0)
                {
                    win();
                }
            }

            else
            {
                counter2--;

                if (counter2 == 0)
                {
                    win();
                }
            }

            yield return new WaitForSeconds(respawnTime);
            
            var randomIndex = random.Next(0, spawnPoints.Length);
            spawnPoint = spawnPoints[randomIndex].GetComponent<Transform>();
            player.GetComponent<Transform>().position = spawnPoint.position;
            player.SetActive(true);
            
        }
}
