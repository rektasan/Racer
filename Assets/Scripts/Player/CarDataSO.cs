using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarData", menuName = "Race/New Car")]
public class CarDataSO : ScriptableObject
{
    public float MaxSpeed;
    public float MaxAcceleration;
    public float BrakeAcceleration;
    public float TurnSensitivity;
}