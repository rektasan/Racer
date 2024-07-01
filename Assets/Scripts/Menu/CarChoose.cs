using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChoose : MonoBehaviour
{
    [SerializeField] private GameObject[] carModels;

    private const string carIndexKey = "CarIndex";

    private int curCar;
    private int curChoosenCar;
    void Start()
    {
        curChoosenCar = PlayerPrefs.GetInt(carIndexKey, 0);
        curCar = curChoosenCar;
        for (int i = 0; i < carModels.Length; i++)
        {
            if (i != curChoosenCar)
                carModels[i].SetActive(false);
        }
    }
    public void NextCar()
    {
        if (curCar + 1 >= carModels.Length)
            return;

        ChangeCar(curCar + 1);
    }
    public void PrevCar()
    {
        if (curCar - 1 < 0)
            return;

        ChangeCar(curCar - 1);
    }
    private void ChangeCar(int carInd)
    {
        carModels[curCar].SetActive(false);
        curCar = carInd;
        carModels[curCar].SetActive(true);
    }
    public void ChooseCar()
    {
        curChoosenCar = curCar;
        PlayerPrefs.SetInt(carIndexKey, curCar);
        PlayerPrefs.Save();
    }
    public void CloseWindow()
    {
        carModels[curCar].SetActive(false);
        curCar = curChoosenCar;
        carModels[curCar].SetActive(true);
    }
}
