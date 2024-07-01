using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WheelType
{
    Front,
    Rear
}
[Serializable]
public class Wheel
{
    public GameObject wheelModel;
    public WheelCollider wheelCollider;
    public WheelType type;
}
public class PlayerWheelsData : MonoBehaviour
{
    public Wheel[] Wheels => wheels;
    [SerializeField] private Wheel[] wheels;
}