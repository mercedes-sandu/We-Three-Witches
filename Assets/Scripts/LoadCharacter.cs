using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject[] characterPrefabs;
    
    /// <summary>
    /// 
    /// </summary>
    public Transform spawnPoint;
    
    /// <summary>
    /// 
    /// </summary>
    public TextMeshProUGUI label;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        label.text = "You are playing as the " + prefab.name;
    }
}