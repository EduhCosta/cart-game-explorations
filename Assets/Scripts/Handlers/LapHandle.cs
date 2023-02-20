using System;
using System.Collections.Generic;
using UnityEngine;

public class LapHandle : MonoBehaviour
{
  [Header("Race Settings")]
  [SerializeField] public int LapQuantity = 0;

  private int _currentLap = 1;

  private void Start()
  {
    RaceStorage.Instance.SetTotalRaceLap(LapQuantity);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.parent.gameObject.CompareTag("Player"))
    {
      CartGameSettings cart = other.GetComponentInParent<CartGameSettings>();
      string racerId = cart.GetPlayerId();

      if (ValidateCheckpointCounter(racerId))
      {
        RaceStorage.Instance.SetLastLapTimeStamp(racerId, _currentLap);
        _currentLap++;
      }

      if (_currentLap - 1 == LapQuantity)
      {
        Debug.Log("Corrida encerrada");
      }
    }
  }

  private bool ValidateCheckpointCounter(string racerId)
  {
    // Get data on storage
    Queue<LapData> lapsQueue = RaceStorage.Instance.GetLapsByRacerID(racerId);
    int totalCheckpoints = RaceStorage.Instance.GetTotalCheckpoints();
    Queue<CheckpointData> checkpointsQueue = RaceStorage.Instance.GetCheckpointsByRacer(racerId);

    CheckpointData[] currentCheckpointData = GetCheckpointDataByLap(lapsQueue, checkpointsQueue, racerId);

    // If we have less checkpoints on queue then the total on race this is a invalid lap
    int sizeOfCheckpointsQueue = currentCheckpointData.Length;
    if (sizeOfCheckpointsQueue < totalCheckpoints) return false;

    return CheckpointChecker(currentCheckpointData, totalCheckpoints);
  }

  private bool CheckpointChecker(CheckpointData[] checkpoints, int totalCheckpoints)
  {
    bool tmpIsCompletedLap = false;

    for (int i = totalCheckpoints; i > 0; i--)
    {
      if (checkpoints[i - 1].checkpointOrder == i)
      {
        tmpIsCompletedLap = true;
      }
      else
      {
        tmpIsCompletedLap = false;
      }
    }

    Debug.Log(tmpIsCompletedLap);

    return tmpIsCompletedLap;
  }

  private Queue<CheckpointData> GetCurrentLapCheckpointQueue(Queue<CheckpointData> fullCheckpointsQueue, string racerId)
  {
    Queue<CheckpointData> currentCheckpointsQueue = new();

    foreach (CheckpointData register in fullCheckpointsQueue)
    {
      if (register.timeStamp > RaceStorage.Instance.GetLastLapByRacerID(racerId).timeStamp)
      {
        currentCheckpointsQueue.Enqueue(register);
      }
    }

    return currentCheckpointsQueue;
  }

  private CheckpointData[] GetCheckpointDataByLap(Queue<LapData> lapsQueue, Queue<CheckpointData> checkpointsQueue, string racerId)
  {
    LapData[] laps = lapsQueue.ToArray();
    CheckpointData[] currentCheckpointData;

    if (laps.Length == 0)
    {
      currentCheckpointData = checkpointsQueue.ToArray();
    }
    else
    {
      Queue<CheckpointData> currentCheckpointsQueue = GetCurrentLapCheckpointQueue(checkpointsQueue, racerId);
      currentCheckpointData = currentCheckpointsQueue.ToArray();
    }

    return currentCheckpointData;
  }
}