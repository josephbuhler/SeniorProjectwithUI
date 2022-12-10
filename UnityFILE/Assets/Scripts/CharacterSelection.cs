using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;
	public GameObject[] previous;
	public GameObject[] next;
	public int selectedCharacter = 0;
	public GameObject[] characterPrefabs;
	public Transform spawnPoint;

	public void NextCharacter()
	{

		previous[selectedCharacter].SetActive(false);
		next[selectedCharacter].SetActive(false);
		characters[selectedCharacter].SetActive(false);
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		previous[selectedCharacter].SetActive(true);
		next[selectedCharacter].SetActive(true);
		characters[selectedCharacter].SetActive(true);
	}

	public void PreviousCharacter()
	{
		previous[selectedCharacter].SetActive(false);
		next[selectedCharacter].SetActive(false);
		characters[selectedCharacter].SetActive(false);
		selectedCharacter--;
		if (selectedCharacter < 0)
		{
			selectedCharacter += characters.Length;
		}
		previous[selectedCharacter].SetActive(true);
		next[selectedCharacter].SetActive(true);
		characters[selectedCharacter].SetActive(true);
	}

	public void StartGame()
	{
		GameObject prefab = characterPrefabs[selectedCharacter];
		GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
		this.gameObject.SetActive(false);
	}
}
