using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeStationController : MonoBehaviour {
    public GameObject ScreenToShow;
    [SerializeField] SimpleCarController car;
    [SerializeField] GameConnectionsController con;
    [SerializeField] EnergyController energyBar;
    [SerializeField] ViewsController views;
    public Text ScreenTextField;
    private int value;
    public float purchase = 5f ;
    private float timeToRecharge;
    private IEnumerator coroutine;
    private int taxValue;
    public int percentageTaxValue = 50;
    private IEnumerator coroutinePaymentFee;
    private string tokenChargingStation = "upY1cfomSbepxuPj2bd1DIK4klm1";

    private string addressChargingStation = "atoi1qztjwahghcyzjv2mtwga8j34svf8e3ypmrtfhtnjzn4jrmw5s2x325gpy4m";
    private string addressCityHall = "atoi1qpqv4yvfcdf53xx4mmufyp6h4ruxtny7hudaf7hw8ak4djq5f2tf7y3pkue";
    bool keepSending = false;
    void Start (){
        ScreenToShow.SetActive(false);
    }

    public IEnumerator StartSendValues(){
        string address = addressChargingStation;
        string message = $"Send {value} Miota to ChargeStation for {Mathf.RoundToInt(timeToRecharge)} seconds of recharge";
        coroutine = con.SendIota(address, value, message);
        Debug.Log($"Send {value} Miota");
        yield return StartCoroutine(coroutine);
        if(views.progress == 7){
            views.FireScreen8();
        }
        StartTaxPayment();
    }

    private void StartTaxPayment(){
        taxValue = Mathf.RoundToInt(value * percentageTaxValue / 100);
        string message = $"ChargeStation pays tax(es) {taxValue}Mi to CityHall";
        coroutinePaymentFee = con.SendIota(addressCityHall, taxValue, message, tokenChargingStation);
        StartCoroutine(coroutinePaymentFee);
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