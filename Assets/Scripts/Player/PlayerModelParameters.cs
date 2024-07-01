using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelParameters : MonoBehaviour
{
    public CarDataSO Parameters => parameters;
    [SerializeField] private CarDataSO parameters;
    public MeshRenderer Lights => lights;
    [SerializeField] private MeshRenderer lights;
    public Color LightsUseColor => lightsUseColor;
    [SerializeField] private Color lightsUseColor;
    public Color LightsDefColor => lightsDefColor;
    [SerializeField] private Color lightsDefColor;
}