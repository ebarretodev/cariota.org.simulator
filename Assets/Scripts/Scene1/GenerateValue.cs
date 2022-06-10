using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateValue : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textInput;
    public string unit;
    public int timeToUpdate = 5;
    public int upperLimit = 100;
    public int lowerLimit = 0;


    void Start()
    {
        StartCoroutine(GenerateNewValue());
    }

    IEnumerator GenerateNewValue(){

        textInput.text = $"{Mathf.RoundToInt(Random.Range(lowerLimit , upperLimit ))}{unit}";
        yield return new WaitForSeconds(timeToUpdate);
        StartCoroutine(GenerateNewValue());

    }

}
