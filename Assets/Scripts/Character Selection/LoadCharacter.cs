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
    /// The list of character portrait sprites.
    /// </summary>
    public Sprite[] characterPortraitSprites;
    
    /// <summary>
    /// The place where the character will initially spawn.
    /// </summary>
    public Transform spawnPoint;
    
    /// <summary>
    /// Displays which character has been selected by the player.
    /// </summary>
    public TextMeshProUGUI label;

    /// <summary>
    /// Displays the portrait of the character that has been selected by the player.
    /// </summary>
    public Image characterPortrait;

    /// <summary>
    /// Instantiates the character the player has selected and other relevant information.
    /// </summary>
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        GameObject prefab = characterPrefabs[selectedCharacter];
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        label.text = prefab.name;
        characterPortrait.sprite = characterPortraitSprites[selectedCharacter];
    }
}