using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIDecisionHandler : MonoBehaviour
{
    // Temporary
    [SerializeField] public float OverlapRadius = 5f;
    [SerializeField] public int CheckableAheadDistance = 10;
    [SerializeField] LayerMask Mask;

    // Exposed values
    public List<Obstacle> ObstacleList = new List<Obstacle>();
    public float AngleToNextCheckpointForward = 0;

    private Queue<RaceCheckpoint> _checkpoints;
    private string _playerId;

    private void OnDrawGizmos()
    {
        // Draws OverlapRadius sphere
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, OverlapRadius);
    }

    private void Start()
    {
        _checkpoints = RaceStorage.Instance.GetRaceCheckpoints();
        _playerId = AIIdentifier.GetAIId(gameObject);
    }

    private void Update()
    {
        TrackObstacles();
        AlignNextCheckpointForward(_playerId);
    }

    /// <summary>
    /// Responsible for identify distance and angle of all colliders into Overlap Sphere.
    /// </summary>
    private void TrackObstacles()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, OverlapRadius, transform.forward, CheckableAheadDistance, Mask.value);
        Collider[] hitColliders = hits.Select(x => x.collider).ToArray();
        List<Obstacle> obstacles = new List<Obstacle>();

        foreach (Collider hitCollider in hitColliders)
        {
            // Idenfity the closest point of obstacle collider to the cart
            Vector3 closestPointToCart = hitCollider.ClosestPoint(transform.position);
            float distance = Vector3.Distance(transform.position, closestPointToCart);
            Debug.Log(distance);

            // Idenfity the angle of obstacle related of cart forward
            Vector3 targetDirection = transform.position - closestPointToCart;
            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
            Debug.Log(angle);

            obstacles.Add(new Obstacle(hitCollider, distance, angle, hitCollider.gameObject.layer));
        }

        // Order the obstacles by the closest one
        ObstacleList = obstacles.OrderBy(a => a.Distance).ToList();
    }

    /// <summary>
    /// Verify the checkpoint passed by AI and verify the diference between AI forward and nextcheckpoint forward
    /// </summary>
    /// <param name="playerId">Cart unique id</param>
    private void AlignNextCheckpointForward(string playerId)
    {
        Queue<CheckpointData> racerCheckpoints = RaceStorage.Instance.GetCheckpointsByRacer(playerId);
        
        int indexOfNextCheckpoint = racerCheckpoints.Count;
        Vector3 nextCheckpointForward = _checkpoints.ToArray()[indexOfNextCheckpoint].forward;
        float angle = Vector3.SignedAngle(nextCheckpointForward, transform.forward, Vector3.up);

        AngleToNextCheckpointForward = angle;
    }
}
