using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    /// <summary>
    /// The list of characters in the scene.
    /// </summary>
    [SerializeField] public GameObject[] characters;
    
    /// <summary>
    /// The index of the selected character.
    /// </summary>
    [SerializeField] public int selectedCharacter = 0;

    /// <summary>
    /// Activates the selected character and deactivates the others. 
    /// </summary>
    /// <param name="next">True if the next button was pressed, false if the previous button was pressed.</param>
    public void ChangeCharacter(bool next)
    {
        characters[selectedCharacter].SetActive(false);
        if (next)
        {
            selectedCharacter = (selectedCharacter + 1) % characters.Length;
        }
        else
        {
            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter += characters.Length;
            }
        }
        characters[selectedCharacter].SetActive(true);
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }
}