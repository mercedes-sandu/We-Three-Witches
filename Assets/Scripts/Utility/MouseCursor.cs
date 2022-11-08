using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Texture2D cursorTexture;
    
    /// <summary>
    /// 
    /// </summary>
    private const CursorMode Mode = CursorMode.Auto;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly Vector2 _hotSpot = Vector2.zero;
    
    /// <summary>
    /// 
    /// </summary>
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, _hotSpot, Mode);
    }

    /// <summary>
    /// 
    /// </summary>
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, Mode);
    }
}