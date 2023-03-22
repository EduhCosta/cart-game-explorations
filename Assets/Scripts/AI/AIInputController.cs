using System;
using UnityEngine;

public class AIInputController : MonoBehaviour
{
    const int TURN_LEFT = -1;
    const int TURN_RIGHT = 1;
    const int NEUTRAL = 0;

    private string _aiId;
    private float _forwardAngleToNextCheckpoint;

    public float Direction = 0;
    public float Accelerate = 0;

    private CheckpointData[] _aICheckpointsDone;
    private int currentCheckpoint = 1;

    void OnEnable()
    {
        _aiId = AIIdentifier.GetAIId(gameObject);
    }

    void Update()
    {
        _aICheckpointsDone = RaceStorage.Instance.GetCheckpointsByRacer(_aiId).ToArray();
        _forwardAngleToNextCheckpoint = GetComponent<AIDecisionHandler>().AngleToNextCheckpointForward;
        
        Accelerate = GoToNextPoint();
        Direction = KeepDirectionToNextCheckpoint();
    }

    private int GoToNextPoint()
    {
        if (Array.Exists(_aICheckpointsDone, c => c.checkpointOrder == currentCheckpoint))
        {
            currentCheckpoint++;
        } 
        else
        {
            return  1;
        }

        return 0;
    }

    public int KeepDirectionToNextCheckpoint()
    {
        if (_forwardAngleToNextCheckpoint > 0 && _forwardAngleToNextCheckpoint <= 190)
        {
            return TURN_LEFT;
        }
        else if (_forwardAngleToNextCheckpoint < 0 && _forwardAngleToNextCheckpoint >= -180)
        {
            return TURN_RIGHT;
        }

        return NEUTRAL;
    }
}
