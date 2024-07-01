using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerModelCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] carModels;
    [SerializeField] private CarMovement carMovement;
    [SerializeField] private GameObject[] spoilers;

    private const string carIndexKey = "CarIndex";
    private const string tuningKey = "Tuning1";
    void Start()
    {
        int curCar = PlayerPrefs.GetInt(carIndexKey, 0);
        for (int i = 0; i < carModels.Length; i++)
        {
            if (i != curCar)
                carModels[i].SetActive(false);
            else
                carModels[i].SetActive(true);
        }
        carMovement.SetWheels(carModels[curCar].GetComponent<PlayerWheelsData>().Wheels);

        PlayerModelParameters param = carModels[curCar].GetComponent<PlayerModelParameters>();
        carMovement.SetParameters(param.Parameters, param.Lights, param.LightsUseColor, param.LightsDefColor);

        for (int i = 0; i < spoilers.Length; i++)
        {
            if (spoilers[i].name == "Back" + PlayerPrefs.GetInt(tuningKey, 0))
            spoilers[i].SetActive(true);
            else
                spoilers[i].SetActive(false);
        }
    }
}