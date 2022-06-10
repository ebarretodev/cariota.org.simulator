using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateEmissionCO2 : MonoBehaviour
{
    [SerializeField] SimpleCarController car;
    public float emissionPerKm;
    public Text textInput;
    private float emissionValue;

    void Update()
    {
        emissionValue = car.totalDistance * emissionPerKm / 1000f;
        textInput.text = $"{Mathf.RoundToInt(emissionValue)}g/km";
    }
}
