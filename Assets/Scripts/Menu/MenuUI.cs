using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas workshopCanvas;
    [SerializeField] private Canvas chooseCarCanvas;
    [SerializeField] private Canvas tuningCanvas;
    [SerializeField] private Canvas trackCanvas;
    [SerializeField] private Canvas tuningChooseCanvas;
    void Start()
    {
        OpenWorkshop(false);
        OpenTrack(false);
        OpenChooseCar(false);
        OpenTuning(false);
        OpenTuningChoose(false);
    }
    public void OpenMenu(bool isOpenedNow)
    {
        menuCanvas.enabled = isOpenedNow;
    }
    public void OpenWorkshop(bool isOpenedNow)
    {
        workshopCanvas.enabled = isOpenedNow;
    }
    public void OpenChooseCar(bool isOpenedNow)
    {
        chooseCarCanvas.enabled = isOpenedNow;
    }
    public void OpenTuning(bool isOpenedNow)
    {
        tuningCanvas.enabled = isOpenedNow;
    }
    public void OpenTuningChoose(bool isOpenedNow)
    {
        tuningChooseCanvas.enabled = isOpenedNow;
    }
    public void OpenTrack(bool isOpenedNow)
    {
        trackCanvas.enabled = isOpenedNow;
    }

    public void Exit()
    {
        Application.Quit();
    }
}