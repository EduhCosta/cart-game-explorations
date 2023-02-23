using UnityEngine;
using System;

public class CartGameSettings : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private string _playerId;
  [SerializeField] public string PlayerName;


  private void Awake()
  {
    TagValidationException();
    SetId();
  }

  /// <summary>
  /// Identify and notify the user if the Tag is correctly setted on same GameObjet with this component is attached;
  /// </summary>
  private void TagValidationException()
  {
    if (!PlayerIdentifier.IsPlayer(gameObject))
    {
      throw new Exception("Please, set this GameObject tag as `Player`");
    }
  }

  /// <summary>
  /// Create a unique id to Cart
  /// </summary>
  private void SetId()
  {
    _playerId = Guid.NewGuid().ToString();
  }

  /// <summary>
  /// Returns the player id
  /// </summary>
  public string GetPlayerId()
  {
    return _playerId;
  }
}