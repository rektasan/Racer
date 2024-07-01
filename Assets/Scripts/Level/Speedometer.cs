using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private CarMovement car;

    [SerializeField] private float minSpdPointerAngle;
    [SerializeField] private float maxSpdPointerAngle;

    [SerializeField] private RectTransform pointerHolder;
    [SerializeField] private TMP_Text speedText;

    void Update()
    {
        speedText.text = ((int)car.CurSpeed).ToString();
        pointerHolder.localEulerAngles = new Vector3(0, 0,
            Mathf.Lerp(minSpdPointerAngle, maxSpdPointerAngle, car.CurSpeed / car.MaxSpeed));
    }
}
