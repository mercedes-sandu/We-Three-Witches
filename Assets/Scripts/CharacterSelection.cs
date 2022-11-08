using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] public GameObject[] characters;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] public int selectedCharacter = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
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
    /// 
    /// </summary>
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }
}