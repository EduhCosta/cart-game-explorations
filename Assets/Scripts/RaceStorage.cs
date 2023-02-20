using System.Collections.Generic;
using UnityEngine;

// Handle racer data
public class RaceStorage : MonoBehaviour
{
  public static RaceStorage Instance { get; private set; }

  private float _currentTime = 0f;
  private bool _isStartRacer = false;
  private int _totalCheckpoints = 0;
  private int _totalLaps = 0;
  private Queue<LapData> QueueOfLaps = new();
  private Queue<CheckpointData> QueueOfCheckpoints = new();

  // Same as contructors in pure C#
  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }

    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    // Test
    StartGame();
  }

  private void Update()
  {
    if (_isStartRacer)
    {
      UpdateTimer();
    }
  }

  /// <summary>
  /// Called when race starts, reponsible to set the initial state of game
  /// </summary>
  void StartGame()
  {
    _isStartRacer = true;
  }

  /// <summary>
  /// Update the general timer
  /// </summary>
  void UpdateTimer()
  {
    _currentTime += Time.deltaTime;
  }

  /// <summary>
  /// Called when race ends, reponsible to set the initial state of game
  /// </summary>
  public void EndGame()
  {
    _currentTime = 0f;
    _isStartRacer = false;
    _totalCheckpoints = 0;
    _totalLaps = 0;
  }

  /// <summary>
  /// Set Player data on Storage
  /// <param name="id">Id of cart.</param>
  /// <param name="name">Name of cart.</param>
  /// <param name="checkpoint">Which checkpoit borns this calls.</param>
  /// </summary>
  public void RegisterCheckpointByPlayer(string id, string name, int checkpoint)
  {
    QueueOfCheckpoints.Enqueue(
      new CheckpointData(id, name, checkpoint, _currentTime)
    );
  }

  /// <summary>
  /// Define the total number of checkpoints
  /// </summary>
  public void SetTotalRacerCheckpoints(int totalCheckpoints)
  {
    _totalCheckpoints = totalCheckpoints;
  }

  /// <summary>
  /// Return the total number of checkpoints
  /// </summary>
  public int GetTotalCheckpoints()
  {
    return _totalCheckpoints;
  }

  /// <summary>
  /// Define the total number of laps
  /// </summary>
  public void SetTotalRaceLap(int totalLaps)
  {
    _totalLaps = totalLaps;
  }

  /// <summary>
  /// Return a checkpoint queue based on racer Id
  /// </summary>
  public Queue<CheckpointData> GetCheckpointsByRacer(string racerId)
  {
    Queue<CheckpointData> racerQueue = new();

    foreach (CheckpointData register in QueueOfCheckpoints)
    {
      if (register.playerId.Equals(racerId))
      {
        racerQueue.Enqueue(register);
      }
    }

    return racerQueue;
  }

  /// <summary>
  /// Set end time of the last lap
  /// </summary>
  public void SetLastLapTimeStamp(string playerId, int lap)
  {
    QueueOfLaps.Enqueue(new LapData(playerId, lap, _currentTime));
  }

  /// <summary>
  /// Returns a queue of laps by racer id
  /// </summary>
  public Queue<LapData> GetLapsByRacerID(string racerId)
  {
    Queue<LapData> lapQueue = new();

    foreach (LapData register in QueueOfLaps)
    {
      if (register.playerId.Equals(racerId))
      {
        lapQueue.Enqueue(register);
      }
    }

    return lapQueue;
  }

  /// <summary>
  /// Returns end time of the last lap
  /// </summary>
  public LapData GetLastLapByRacerID(string racerId)
  {
    return GetLapsByRacerID(racerId).Peek();
  }

}