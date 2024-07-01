using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColor : MonoBehaviour
{
    [SerializeField] private Color carColor;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private bool needToLoadColor;
    [SerializeField] private string tuningColorKey = "TuningColorHex";

    void Start()
    {
        meshRenderer.materials[0].color = carColor;

        if (needToLoadColor)
            SetNewColor(HexToColor(PlayerPrefs.GetString(tuningColorKey, "FFFFFF")));
    }

    private Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    public void SetNewColor(Color newColor)
    {
        meshRenderer.materials[0].color = newColor;
    }
}