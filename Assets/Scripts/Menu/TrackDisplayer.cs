using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TrackDisplayer : MonoBehaviour
{
    [SerializeField] private Image trackImage;
    [SerializeField] private TMP_Text trackName;
    [SerializeField] private TMP_Text trackRecord;

    [SerializeField] private TrackSO[] tracks;
    private int curTrackInd = 0;

    private const string trackKey = "Track";

    private void Start()
    {
        curTrackInd = 0;
        FillDescription();
    }

    private void FillDescription()
    {
        trackImage.sprite = tracks[curTrackInd].TrackPhoto;
        trackName.text = tracks[curTrackInd].Name;
        trackRecord.text = string.Concat("Best:" +
            (PlayerPrefs.GetInt(trackKey + (curTrackInd + 1), tracks[curTrackInd].EnemiesCount) + 1),"/",
            tracks[curTrackInd].EnemiesCount + 1);
    }

    public void NextLevel()
    {
        if (curTrackInd + 1 >= tracks.Length)
            return;

        ChangeLevel(curTrackInd + 1);
    }
    public void PrevLevel()
    {
        if (curTrackInd - 1 < 0)
            return;

        ChangeLevel(curTrackInd - 1);
    }
    private void ChangeLevel(int levelInd)
    {
        curTrackInd = levelInd;
        FillDescription();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(tracks[curTrackInd].SceneInd);
    }
}