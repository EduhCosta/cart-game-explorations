using UnityEngine;
public static class PlayerIdentifier
{
    const string PLAYER_TAG = "Player";

    public static bool IsPlayer(Collider collider)
    {
        return collider.transform.parent.gameObject.CompareTag(PLAYER_TAG);
    }

    public static bool IsPlayer(GameObject gameObject)
    {
        return gameObject.CompareTag(PLAYER_TAG);
    }
}
