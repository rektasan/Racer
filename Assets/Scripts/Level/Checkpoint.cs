using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool IsStart => isTrackStart;
    [SerializeField] private bool isTrackStart;

    public int Ind => ind;
    private int ind;

    private CheckpointsSystem checkpointsSystem;

    public void Setup(CheckpointsSystem checkpointsSyst, int curInd)
    {
        checkpointsSystem = checkpointsSyst;
        ind = curInd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CarMovement>())
        {
            checkpointsSystem.ChangePlayerPoint(isTrackStart, ind);
        }
    }
}