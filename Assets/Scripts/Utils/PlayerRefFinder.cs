using UnityEngine;

public static class PlayerRefFinder
{
    private static PlayerController _cachedPlayer;

    public static PlayerController GetPlayer()
    {
        if (_cachedPlayer == null)
        {
            _cachedPlayer = Object.FindFirstObjectByType<PlayerController>();
        }
        return _cachedPlayer;
    }
}