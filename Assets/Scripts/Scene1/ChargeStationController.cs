using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeStationController : MonoBehaviour {
    public GameObject ScreenToShow;
    [SerializeField] SimpleCarController car;
    [SerializeField] GameConnectionsController con;
    [SerializeField] EnergyController energyBar;
    public Text ScreenTextField;
    private int value;
    public float purchase = 5f ;
    private float timeToRecharge;
    private IEnumerator coroutine;
    bool keepSending = false;
    void Start (){
        ScreenToShow.SetActive(false);
    }

    public IEnumerator StartSendValues(){
        string address = "atoi1qztjwahghcyzjv2mtwga8j34svf8e3ypmrtfhtnjzn4jrmw5s2x325gpy4m";
        string message = $"Send {value} Miota to ChargeStation for {Mathf.RoundToInt(timeToRecharge)} seconds of recharge";
        coroutine = con.SendIota(address, value, message);
        Debug.Log($"Send {value} Miota");
        yield return StartCoroutine(coroutine);
    }


   public void CloseScreen (){
        ScreenToShow.SetActive(false);
    }

    void OnTriggerEnter(Collider player){
        timeToRecharge = energyBar.tempoMaximo - energyBar.tempoAtual;
        value = Mathf.RoundToInt((timeToRecharge / purchase) + 0.5f);
        ScreenTextField.text = $"Would you like to full charge your vehicle? ({value}Mi for {Mathf.RoundToInt(timeToRecharge)}s of driving)";
        ScreenToShow.SetActive(true);
        car.ForceCarBrake();
    }
}