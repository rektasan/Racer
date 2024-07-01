using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuningChoose : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private CarColor[] carColors;

    [SerializeField] private Color[] wheelColors;
    [SerializeField] private CarColor[] carWheelColors;

    [SerializeField] private GameObject[] spoilers;

    [SerializeField] private float[] engineSpeedBoost;
    private enum TuningType
    {
        Color,
        Spoiler,
        Wheels,
        Engine
    }

    private TuningType tuningType;
    private int[] curChosenTuning = new int[4];
    private int[] curTuning = new int[4];

    private const string tuningKey = "Tuning";
    private const string tuningColorKey = "TuningColorHex";
    private const string tuningWheelsColorKey = "TuningWheelColorHex";
    private const string tuningEngineKey = "TuningEngine";
    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            curTuning[i] = PlayerPrefs.GetInt(tuningKey + i, 0);
        }

        for (int i = 0; i < 4; i++)
        {
            curChosenTuning[i] = curTuning[i];
        }

        for (int i = 0; i < carWheelColors.Length; i++)
        {
            carWheelColors[i].SetNewColor(wheelColors[curTuning[(int)TuningType.Wheels]]);
        }

        for (int i = 0; i < spoilers.Length; i++)
        {
            if (spoilers[i].name == "Back" + curTuning[(int)TuningType.Spoiler])
                spoilers[i].SetActive(true);
            else
                spoilers[i].SetActive(false);
        }

        for (int i = 0; i < carColors.Length; i++)
        {
            carColors[i].SetNewColor(colors[curTuning[(int)TuningType.Color]]);
        }
    }
    public void SetTuningCategory(int ind)
    {
        tuningType = (TuningType)ind;
    }
    public void NextTuning()
    {
        if (tuningType == TuningType.Color && curChosenTuning[(int)tuningType] + 1 >= colors.Length)
            return;

        if (tuningType == TuningType.Spoiler && curChosenTuning[(int)tuningType] + 1 >= 4)
            return;

        if (tuningType == TuningType.Wheels && curChosenTuning[(int)tuningType] + 1 >= wheelColors.Length)
            return;

        if (tuningType == TuningType.Engine && curChosenTuning[(int)tuningType] + 1 >= engineSpeedBoost.Length)
            return;

        ChangeTuning(curChosenTuning[(int)tuningType] + 1);
    }
    public void PrevTuning()
    {
        if (curChosenTuning[(int)tuningType] - 1 < 0)
            return;

        ChangeTuning(curChosenTuning[(int)tuningType] - 1);
    }
    private void ChangeTuning(int tunInd)
    {
        if (tuningType == TuningType.Color)
        {
            for (int i = 0; i < carColors.Length; i++)
            {
                carColors[i].SetNewColor(colors[tunInd]);
            }
        }
        else if (tuningType == TuningType.Spoiler)
        {
            for (int i = 0; i < spoilers.Length; i++)
            {
                if (spoilers[i].name == "Back" + tunInd)
                    spoilers[i].SetActive(true);
                else
                    spoilers[i].SetActive(false);
            }
        }
        else if (tuningType == TuningType.Wheels)
        {
            for (int i = 0; i < carWheelColors.Length; i++)
            {
                carWheelColors[i].SetNewColor(wheelColors[tunInd]);
            }
        }
        curChosenTuning[(int)tuningType] = tunInd;
    }
    public void ChooseTuning()
    {
        curTuning = curChosenTuning;
        if (tuningType == TuningType.Color)
        {
            PlayerPrefs.SetInt(tuningKey + (int)tuningType, curTuning[(int)TuningType.Color]);
            PlayerPrefs.SetString(tuningColorKey, ColorToHex(colors[curTuning[(int)TuningType.Color]]));
            PlayerPrefs.Save();
        }
        else if (tuningType == TuningType.Spoiler)
        {
            PlayerPrefs.SetInt(tuningKey + (int)tuningType, curTuning[(int)TuningType.Spoiler]);
            PlayerPrefs.Save();
        }
        else if (tuningType == TuningType.Wheels)
        {
            PlayerPrefs.SetInt(tuningKey + (int)tuningType, curTuning[(int)TuningType.Wheels]);
            PlayerPrefs.SetString(tuningWheelsColorKey, ColorToHex(wheelColors[curTuning[(int)TuningType.Wheels]]));
            PlayerPrefs.Save();
        }
        else if (tuningType == TuningType.Engine)
        {
            PlayerPrefs.SetInt(tuningKey + (int)tuningType, curTuning[(int)TuningType.Engine]);
            PlayerPrefs.SetFloat(tuningEngineKey, engineSpeedBoost[curTuning[(int)TuningType.Engine]]);
            PlayerPrefs.Save();
        }
    }
    private string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }
    public void CloseWindow()
    {
        curChosenTuning = curTuning;
        if (tuningType == TuningType.Color)
        {
            for (int i = 0; i < carColors.Length; i++)
            {
                carColors[i].SetNewColor(colors[curTuning[(int)TuningType.Color]]);
            }
        }
        if (tuningType == TuningType.Spoiler)
        {
            for (int i = 0; i < spoilers.Length; i++)
            {
                if (spoilers[i].name == "Back" + curTuning[(int)TuningType.Spoiler])
                    spoilers[i].SetActive(true);
                else
                    spoilers[i].SetActive(false);
            }
        }
        if (tuningType == TuningType.Wheels)
        {
            for (int i = 0; i < carWheelColors.Length; i++)
            {
                carWheelColors[i].SetNewColor(wheelColors[curTuning[(int)TuningType.Wheels]]);
            }
        }
    }
}