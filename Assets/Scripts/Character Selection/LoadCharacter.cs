using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacter : MonoBehaviour
{
    /// <summary>
    /// The list of character prefabs.
    /// </summary>
    public GameObject[] characterPrefabs;

    /// <summary>
    /// The list of enemy prefabs.
    /// </summary>
    public GameObject[] enemyPrefabs;

    /// <summary>
    /// The list of character portrait sprites.
    /// </summary>
    public Sprite[] characterPortraitSprites;
    
    /// <summary>
    /// The place where the character will initially spawn.
    /// </summary>
    public Transform spawnPoint;
    
    /// <summary>
    /// The list of enemy spawn points.
    /// </summary>
    public Transform[] enemySpawnPoints;

    /// <summary>
    /// Displays which character has been selected by the player.
    /// </summary>
    public TextMeshProUGUI label;

    /// <summary>
    /// Displays the portrait of the character that has been selected by the player.
    /// </summary>
    public Image characterPortrait;

    /// <summary>
    /// The reference to the currently selected character.
    /// </summary>
    private GameObject _player;

    /// <summary>
    /// Instantiates the character the player has selected and other relevant information. Calls for enemies to be
    /// spawned every 15 seconds after 2 seconds have passed in the level. 
    /// </summary>
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        GameObject prefab = characterPrefabs[selectedCharacter];
        _player = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        GameEvent.CreatePlayer(_player);
        label.text = prefab.name;
        characterPortrait.sprite = characterPortraitSprites[selectedCharacter];
        InvokeRepeating(nameof(SpawnEnemy), 2, 15);
    }

    /// <summary>
    /// Spawns a random enemy at a random spawn point.
    /// </summary>
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], 
            enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position, Quaternion.identity);
        GameEvent.CreatePlayer(_player);
    }
}