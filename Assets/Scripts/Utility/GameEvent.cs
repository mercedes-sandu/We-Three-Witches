using UnityEngine;

public static class GameEvent
{
    /// <summary>
    /// An event handler for the creation of the player.
    /// </summary>
    public delegate void PlayerHandler(GameObject player);

    /// <summary>
    /// Called when the player is created.
    /// </summary>
    public static event PlayerHandler OnPlayerCreate;

    /// <summary>
    /// Creates the player in other scripts.
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public static void CreatePlayer(GameObject player) => OnPlayerCreate?.Invoke(player);
}