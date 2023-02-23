using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckpointController : MonoBehaviour
{

    private void Start()
    {
        // Set on storage the total number of checkpoints
        RaceStorage.Instance.SetTotalRacerCheckpoints(transform.childCount);
    }
    private void FixedUpdate()
    {
        IdentifyWrongFlowByAllCheckpoints();
    }

    private void IdentifyWrongFlowByAllCheckpoints()
    {
        TrackFlowHandler[] flows = gameObject.GetComponentsInChildren<TrackFlowHandler>();
        bool tmpIsWrongFlow = false;

        foreach (TrackFlowHandler flow in flows)
        {
            tmpIsWrongFlow = tmpIsWrongFlow || flow.GetIsWrongFlow();
        }

        RaceStorage.Instance.SetIsWorngFlow(tmpIsWrongFlow);
    }
}
