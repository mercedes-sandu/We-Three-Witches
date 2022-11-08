using TMPro;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    /// <summary>
    /// The list of character prefabs.
    /// </summary>
    public GameObject[] characterPrefabs;
    
    /// <summary>
    /// The place where the character will initially spawn.
    /// </summary>
    public Transform spawnPoint;
    
    /// <summary>
    /// Displays which character has been selected by the player.
    /// </summary>
    public TextMeshProUGUI label;

    /// <summary>
    /// Instantiates the character the player has selected and other relevant information.
    /// </summary>
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        label.text = "You are playing as the " + prefab.name;
    }
}