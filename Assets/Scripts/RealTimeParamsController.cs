using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PlaygroundPinappleStudios
{
    public class RealTimeParamsController : MonoBehaviour
    {
        [SerializeField] public SphereCartController Cart;
        [SerializeField] public TMP_InputField AccelerationField;
        [SerializeField] public TMP_InputField SteerField;
        [SerializeField] public TMP_InputField GravityField;

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
        }
    }
}