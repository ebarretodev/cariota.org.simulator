using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HoleController : MonoBehaviour
{

    [SerializeField] GameConnectionsController con;
    public GameObject ScreenToShow;
    public Text message;
    public int HoleNumber = 1;
    public int value = 5;
    private IEnumerator coroutine;
    private double lastTime = 0f;
    private double currentTime = 0f;
    public double TimeToReport = 120f;
    private double SpendTime;
    private double ReportTime;

    public IEnumerator StartSendValues()
    {
        string tokenFromCityHall = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYxYmRlN2Q5MmYxMWIzNjg4Njg4OTI3NiIsInVzZXJuYW1lIjoiQ2l0eUhhbGwiLCJpYXQiOjE2Mzk4MzU2MDl9.RX9NMwNBRwDn2WsfRsIvZUqbfFGtMIX1ThX3eZZauNU";
        string address = con.address;
        string message = $"Send {value} Miota to user for report hole #{HoleNumber}";
        coroutine = con.SendIota(address, value, message, tokenFromCityHall);
        Debug.Log($"Send {value} Miota");
        yield return StartCoroutine(coroutine);
    }

    IEnumerator ControlMessages()
    {
        ScreenToShow.SetActive(true);
        message.text = $"Hole #{HoleNumber} was detected on the street. Reporting to the municipality.";
        yield return new WaitForSeconds(5);
        ScreenToShow.SetActive(true);
        message.text = "The CityHall will send 5Mi for the report!";
        yield return StartCoroutine(StartSendValues());
        ScreenToShow.SetActive(true);
        message.text = "5Mi received from CityHall.";
        yield return new WaitForSeconds(5);
        ScreenToShow.SetActive(false);
    }

    IEnumerator WaitTime()
    {
        ScreenToShow.SetActive(true);
        double restTime = (ReportTime - SpendTime)/1000f;
        message.text = $"Wait more {(int)restTime}s to report again!";
        yield return new WaitForSeconds(5);
        ScreenToShow.SetActive(false);
    }

    void Start()
    {
        ScreenToShow.SetActive(false);
    }

    private double getTime()
    {
        double dateReturn =
            Math.Round((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        // note that (..date..).TotalMilliseconds returns a number such as
        // 1606465207140.45 where
        // 1606465207140 is ms and the ".45" is a fraction of a ms
        return dateReturn;
    }

    void OnTriggerEnter(Collider player)
    {
        currentTime = getTime();
        SpendTime = currentTime - lastTime;
        ReportTime = TimeToReport * 1000f;
        if (SpendTime > ReportTime)
        {
            StartCoroutine(ControlMessages());
            Debug.Log("Send Iotas");
            lastTime = currentTime;
        }
        else
        {
            StartCoroutine(WaitTime());
        }

    }


}