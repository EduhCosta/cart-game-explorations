using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] public int CheckpointOrder = 0;

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.parent.gameObject.CompareTag("Player"))
    {
      CartGameSettings cart = other.GetComponentInParent<CartGameSettings>();
      string id = cart.GetPlayerId();
      string name = cart.PlayerName;

      RaceStorage.Instance.RegisterCheckpointByPlayer(id, name, CheckpointOrder);
    }
  }
}