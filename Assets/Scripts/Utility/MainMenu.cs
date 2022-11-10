using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts the game at the character selection scene.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
    }
}