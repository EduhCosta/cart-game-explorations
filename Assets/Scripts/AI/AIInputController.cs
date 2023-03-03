using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIInputController : MonoBehaviour
{
    const int TURN_LEFT = -1;
    const int TURN_RIGHT = 1;
    const int NEUTRAL = 0;

    private string _aiId;
    private Obstacle[] _obstacles = new Obstacle[0];
    private float _distanceToCollision;
    private float _forwardAngleToNextCheckpoint;

    public float Direction = 0;
    public float Accelerate = 0;

    void Start()
    {
        _distanceToCollision = GetComponent<AIDecisionHandler>().CheckableAheadDistance;
        _aiId = AIIdentifier.GetAIId(gameObject);
    }

    void Update()
    {
        _obstacles = GetComponent<AIDecisionHandler>().ObstacleList.ToArray();
        _forwardAngleToNextCheckpoint = GetComponent<AIDecisionHandler>().AngleToNextCheckpointForward;

        if (hasObstacles())
        {
            TakeDirectionByObstacle();
        }
        else
        {
            KeepDirectionToForward();
        }

        KeepAccelerating(_aiId);
    }

    // If (angle > 0 && angle <= 180) - Go to left
    // If (angle < 0 && angle >= -180) - Go to right
    public void KeepDirectionToForward()
    {
        if (_forwardAngleToNextCheckpoint > 0 && _forwardAngleToNextCheckpoint <= 190)
        {
            Direction = TURN_LEFT;
        }
        else if (_forwardAngleToNextCheckpoint < 0 && _forwardAngleToNextCheckpoint >= -180)
        {
            Direction = TURN_RIGHT;
        }
        else
        {
            Direction = NEUTRAL;
        }
    }

    private bool hasObstacles()
    {
        return _obstacles != null && _obstacles.Length > 0;
    }

    /// <summary>
    /// According of the closest obstacle take right direction to avoid it
    /// </summary>
    public void TakeDirectionByObstacle()
    {
        Obstacle closestObstacle = _obstacles[0];
        float angle = closestObstacle.AngleToHit;

        // Colliders hit the sphere
        if (closestObstacle.Distance < _distanceToCollision)
        {
            // If angle is equal to 180, select randomly a side SphereCast to select the side??
            if (angle == 180)
            {
                Direction = TURN_RIGHT;
            }
            // If angle is positive turn left
             if (angle > 0)
            {
                Direction = TURN_LEFT;
            }
            // If angle is legative turn right
            else if (angle < 0)
            {
                Direction = TURN_RIGHT;
            }
        }
    }

    /// <summary>
    /// While not pass for the last checkpoint the AI keep acelerating
    /// </summary>
    /// <param name="aiId"></param>
    private void KeepAccelerating(string aiId)
    {
        int totalCheckpoins = RaceStorage.Instance.GetTotalCheckpoints();
        Queue<CheckpointData> checkpointsDone = RaceStorage.Instance.GetCheckpointsByRacer(aiId);
        // While not pass all checkpoints keep acelerating
        if (checkpointsDone.ToArray().Length < totalCheckpoins)
        {
            Accelerate = 1;
            return;
        }

        Accelerate = 0;
    }


}
