using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrack", menuName = "Race/New Track")]
public class TrackSO : ScriptableObject
{
    public string Name;
    public Sprite TrackPhoto;
    public int EnemiesCount;
    public int SceneInd;
}