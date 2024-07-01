using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCarAI : MonoBehaviour
{
    [SerializeField] private CheckpointsSystem checkpointsSystem;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Transform[] wheels;
    public int CurPoint => curPoint;
    private int curPoint;
    public int CurLap => curLap;
    private int curLap;

    private Checkpoint[] checkpoints;
    private NavMeshAgent agent;
    private bool canMove = false;
    public void StartMovement()
    {
        agent.SetDestination(checkpoints[curPoint].transform.position);
        canMove = true;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        checkpoints = checkpointsSystem.Checkpoints;
    }
    void Update()
    {
        if (!canMove)
            return;

        agent.speed = Mathf.Lerp(agent.speed, maxSpeed, acceleration * Time.deltaTime);
        WheelAnimation();

        if(Vector3.Distance(transform.position, checkpoints[curPoint].transform.position) < 5)
        {
            SetNewPoint(checkpoints[curPoint].IsStart, checkpoints[curPoint].Ind);
        }
    }
    private void WheelAnimation()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate(Vector3.right, 250 * agent.velocity.magnitude * Time.deltaTime);
        }
    }
    public void SetNewPoint(bool isTrackStart, int ind)
    {
        if (ind != curPoint)
            return;

        curPoint++;

        if (curPoint == checkpoints.Length)
            curPoint = 0;

        agent.SetDestination(checkpoints[curPoint].transform.position);

        if (isTrackStart)
        {
            curLap++;
            if (curLap > checkpointsSystem.MaxLaps)
            {
                agent.isStopped = true;
            }
        }
    }
}
