using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckpointController : MonoBehaviour
{
  private void Awake()
  {
    // Set on storage the total number of checkpoints
    RaceStorage.Instance.SetTotalRacerCheckpoints(transform.childCount);
  }
}
