using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        selectedCharacter = next ? (selectedCharacter + 1) % characters.Length : 
            selectedCharacter = (selectedCharacter - 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        
    }
}