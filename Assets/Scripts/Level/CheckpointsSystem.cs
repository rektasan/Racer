using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointsSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text lapsText;
    [SerializeField] private TMP_Text finishText;

    [SerializeField] private EnemyCarAI[] enemies;
    [SerializeField] private CarMovement player;
    public Checkpoint[] Checkpoints => checkpoints;
    [SerializeField] private Checkpoint[] checkpoints;
    public int MaxLaps => maxLaps;
    [SerializeField] private int maxLaps;

    private int curPoint;
    private int curLap;

    private int curPos;

    private const string trackKey = "Track";
    void Start()
    {
        curLap = 0;
        curPoint = 0;
        lapsText.text = $"Laps: {curLap}/{maxLaps}\nPosition: {curPos + 1}/{enemies.Length + 1}";
        SetupCheckpoints();
    }
    private void SetupCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].Setup(this, i);
        }
    }
    private void Update()
    {
        if (curLap > maxLaps)
            return;

        ComparePositions();
    }
    private void ComparePositions()
    {
        curPos = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (curLap < enemies[i].CurLap)
            {
                curPos++;
            }
            else if (curLap == enemies[i].CurLap)
            {
                int playerPoint = curPoint == 0 ? checkpoints.Length + 1 : curPoint;
                int carPoint = enemies[i].CurPoint == 0 ? checkpoints.Length + 1 : enemies[i].CurPoint;
                if (playerPoint < carPoint)
                {
                    curPos++;
                }
                else if (curPoint == enemies[i].CurPoint)
                {
                    if (Vector3.Distance(player.transform.position, checkpoints[curPoint].transform.position)
                    > Vector3.Distance(enemies[i].transform.position, checkpoints[enemies[i].CurPoint].transform.position))
                    {
                        curPos++;
                    }
                }
            }
        }

        lapsText.text = $"Laps: {curLap}/{maxLaps}\nPosition: {curPos + 1}/{enemies.Length + 1}";
    }
    public void ChangePlayerPoint(bool isTrackStart, int ind)
    {
        if (ind != curPoint)
            return;

        curPoint++;

        if (curPoint == checkpoints.Length)
            curPoint = 0;

        if (isTrackStart)
        {
            curLap++;
            lapsText.text = $"Laps: {curLap}/{maxLaps}\nPosition: {curPos + 1}/{enemies.Length + 1}";

            if (curLap > maxLaps)
            {
                player.BlockMovement(true);

                if (curPos == 0)
                {
                    finishText.text = "You are first!";
                }
                else
                {
                    finishText.text = $"Your position: {curPos + 1}";
                }

                if(curPos < PlayerPrefs.GetInt(trackKey + SceneManager.GetActiveScene().buildIndex, enemies.Length))
                {
                    PlayerPrefs.SetInt(trackKey + SceneManager.GetActiveScene().buildIndex, curPos);
                    PlayerPrefs.Save();
                }

                lapsText.text = string.Empty;
            }
        }
    }
}
