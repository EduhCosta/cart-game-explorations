using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RealTimeParamsController : MonoBehaviour
{
    [SerializeField] public SphereCartController Cart;
    [SerializeField] public TMP_InputField AccelerationField;
    [SerializeField] public TMP_InputField SteerField;
    [SerializeField] public TMP_InputField GravityField;
    [SerializeField] public TMP_Text LapValue;
    [SerializeField] public GameObject FlowValue;
    [SerializeField] public CartGameSettings CartSettings;

    void Start()
    {
        AccelerationField.text = string.Format("{0:N2}", Cart.Acceleration);
        SteerField.text = string.Format("{0:N2}", Cart.Steering);
        GravityField.text = string.Format("{0:N2}", Cart.Gravity);
    }

    void FixedUpdate()
    {
        Cart.Acceleration = float.Parse(AccelerationField.text);
        Cart.Steering = float.Parse(SteerField.text);
        Cart.Gravity = float.Parse(GravityField.text);
        Cart.Gravity = float.Parse(GravityField.text);
        int currentLap = RaceStorage.Instance.GetCurrentLap();
        int totalLaps = RaceStorage.Instance.GetTotalRaceLap();
        LapValue.text = $"{currentLap}/{totalLaps}";
        FlowValue.SetActive(RaceStorage.Instance.GetIsWorngFlow());
    }
}
