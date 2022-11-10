using System;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    /// <summary>
    /// The normal cursor texture.
    /// </summary>
    [SerializeField] private Texture2D cursor;

    /// <summary>
    /// The clicked cursor texture.
    /// </summary>
    [SerializeField] private Texture2D cursorClicked;

    /// <summary>
    /// Changes the cursor to the normal texture.
    /// </summary>
    void Awake()
    {
        ChangeCursor(cursor);
    }

    /// <summary>
    /// Changes the cursor to the specified texture.
    /// </summary>
    /// <param name="cursorType">The new cursor texture.</param>
    private void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, new Vector2(cursorType.width / 2, cursorType.height / 2), 
            CursorMode.Auto);
    }

    /// <summary>
    /// Changes the cursor texture when the mouse is clicked.
    /// </summary>
    void Update()
    {
        ChangeCursor(Input.GetMouseButton(0) ? cursorClicked : cursor);
    }
}