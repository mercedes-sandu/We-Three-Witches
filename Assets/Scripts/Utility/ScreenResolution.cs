using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    /// <summary>
    /// The width of the screen.
    /// </summary>
    [SerializeField] private int width = 640;
    
    /// <summary>
    /// The height of the screen.
    /// </summary>
    [SerializeField] private int height = 340;
    
    /// <summary>
    /// Sets the screen resolution to the specified width and height.
    /// </summary>
    void Start()
    {
        Screen.SetResolution(width, height, false);
    }
}