using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public GameObject ScreenToShow;
    public GameObject ScreenToInfo;
    [SerializeField] SimpleCarController car;

    [SerializeField] GameConnectionsController con;
    [SerializeField] ViewsController views;

    private IEnumerator coroutine;
    public int CoffeeValue;

    private int taxValue;
    public int percentageTaxValue = 50;
    public Text message;
    private IEnumerator coroutinePaymentFee;
    private string tokenShop = "wDnUE21Vz6Vn7zm5ERbvcybqzi33";
    private string addressShop = "atoi1qr02tzfuemp3d3fljlcq7rjuk5u7r5f6d3uh8q0llyuj8hvytkjgqnxlkwn";
    private string addressCityHall = "atoi1qpqv4yvfcdf53xx4mmufyp6h4ruxtny7hudaf7hw8ak4djq5f2tf7y3pkue";
    void Start()
    {
        ScreenToShow.SetActive(false);
    }

    public void CloseScreen()
    {
        ScreenToShow.SetActive(false);
    }

    public void OrderCoffee (){
        StartCoroutine(ControlMessages());
    }

    IEnumerator ControlMessages()
    {
        ScreenToShow.SetActive(false);
        ScreenToInfo.SetActive(true);
        message.text = $"Sending order to shop";
        yield return StartCoroutine(StartSendValues());
        ScreenToInfo.SetActive(true);
        message.text = $"Order received. Preparing your coffee.";
        yield return new WaitForSeconds(5);
        ScreenToInfo.SetActive(true);
        message.text = "Coffee already delivered. Thanks :) Come back Soon!!";
        car.ReleaseForceCarBrake();
        yield return new WaitForSeconds(5);
        if(views.progress == 3){
            views.FireScreen4();
        }
        ScreenToInfo.SetActive(false);

    }

    public IEnumerator StartSendValues()
    {
        string address = addressShop;
        string message = $"Send {CoffeeValue} Miota to Shop for 1 coffee";
        coroutine = con.SendIota(address, CoffeeValue, message);
        Debug.Log($"Send {CoffeeValue} Miota");
        yield return StartCoroutine(coroutine);
        StartTaxPayment();
    }

    private void StartTaxPayment()
    {
        taxValue = Mathf.RoundToInt(CoffeeValue * percentageTaxValue / 100);
        string message = $"Shop pays tax(es) {taxValue}Mi to CityHall";
        coroutinePaymentFee = con.SendIota(addressCityHall, taxValue, message, tokenShop);
        StartCoroutine(coroutinePaymentFee);
    }

    void OnTriggerEnter(Collider player)
    {
        ScreenToShow.SetActive(true);
        car.ForceCarBrake();
    }
}
