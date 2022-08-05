using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class ViewsController : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void finishQuest();
    [SerializeField] GameObject[] Screens;

    [SerializeField] GameObject Decals;
    [SerializeField] GameObject CoffeeShop;
    [SerializeField] GameObject ChargingStation;
    [SerializeField] GameObject ReportData;
    [SerializeField] Text QuestText;

    public int progress;

    void Start() {
        Screens[0].SetActive(true);

        progress = 0;
        Decals.SetActive(false);
        CoffeeShop.SetActive(false);
        ChargingStation.SetActive(false);
        ReportData.SetActive(false);
        Time.timeScale = 0;

        Transform btn0 = transform.Find($"Views/Screen0/ContinueButton");
        Button ContinueButton0 = btn0.GetComponent<Button>();
        ContinueButton0.onClick.AddListener(TaskOnClickButton0);

        Transform btn1 = transform.Find($"Views/Screen1/ContinueButton");
        Button ContinueButton1 = btn1.GetComponent<Button>();
        ContinueButton1.onClick.AddListener(TaskOnClickButton1);

        Transform btn2 = transform.Find($"Views/Screen2/ContinueButton");
        Button ContinueButton2 = btn2.GetComponent<Button>();
        ContinueButton2.onClick.AddListener(TaskOnClickButton2);

        Transform btn3 = transform.Find($"Views/Screen3/ContinueButton");
        Button ContinueButton3 = btn3.GetComponent<Button>();
        ContinueButton3.onClick.AddListener(TaskOnClickButton3);

        Transform btn4 = transform.Find($"Views/Screen4/ContinueButton");
        Button ContinueButton4 = btn4.GetComponent<Button>();
        ContinueButton4.onClick.AddListener(TaskOnClickButton4);

        Transform btn5 = transform.Find($"Views/Screen5/ContinueButton");
        Button ContinueButton5 = btn5.GetComponent<Button>();
        ContinueButton5.onClick.AddListener(TaskOnClickButton5);

        Transform btn6 = transform.Find($"Views/Screen6/ContinueButton");
        Button ContinueButton6 = btn6.GetComponent<Button>();
        ContinueButton6.onClick.AddListener(TaskOnClickButton6);

        Transform btn7 = transform.Find($"Views/Screen7/ContinueButton");
        Button ContinueButton7 = btn7.GetComponent<Button>();
        ContinueButton7.onClick.AddListener(TaskOnClickButton7);

        Transform btn8 = transform.Find($"Views/Screen8/ContinueButton");
        Button ContinueButton8 = btn8.GetComponent<Button>();
        ContinueButton8.onClick.AddListener(TaskOnClickButton8);

        Transform btn9 = transform.Find($"Views/Screen9/ContinueButton");
        Button ContinueButton9 = btn9.GetComponent<Button>();
        ContinueButton9.onClick.AddListener(TaskOnClickButton9);

    }


    void TaskOnClickButton0()
    {
        Screens[0].SetActive(false);
        progress = 1;
        Screens[1].SetActive(true);

    }

    void TaskOnClickButton1()
    {
        Screens[1].SetActive(false);
        Time.timeScale = 1;
        Decals.SetActive(true);
        QuestText.text = "Find one hole on the street to report";
    }

    public void FireScreen2 (){
        if(progress == 1){
            Time.timeScale = 0;
            progress = 2;
            Screens[2].SetActive(true);
        }
    }

    void TaskOnClickButton2()
    {
        Screens[2].SetActive(false);
        progress = 3;
        Screens[3].SetActive(true);
    }

    void TaskOnClickButton3(){
        Screens[3].SetActive(false);
        Time.timeScale = 1;
        CoffeeShop.SetActive(true);
        QuestText.text = "Find a shop to buy a coffee";
    }
    public void FireScreen4()
    {
        Time.timeScale = 0;
        progress = 4;
        Screens[4].SetActive(true);
    }

    void TaskOnClickButton4(){
        Screens[4].SetActive(false);
        progress = 5;
        Screens[5].SetActive(true);
    }
    void TaskOnClickButton5(){
        Screens[5].SetActive(false);
        Time.timeScale = 1;
        ReportData.SetActive(true);
        QuestText.text = "Locate a button on the screen to share your data";
    }
    public void FireScreen6()
    {
        Time.timeScale = 0;
        progress = 6;
        Screens[6].SetActive(true);
    }

    void TaskOnClickButton6(){
        Screens[6].SetActive(false);
        progress = 7;
        Screens[7].SetActive(true);
    }
    void TaskOnClickButton7(){
        Screens[7].SetActive(false);
        Time.timeScale = 1;
        ChargingStation.SetActive(true);
        QuestText.text = "Recharge your car on Charging Station";
    }
    public void FireScreen8()
    {
        Time.timeScale = 0;
        progress = 8;
        Screens[8].SetActive(true);
    }
    void TaskOnClickButton8(){
        Screens[8].SetActive(false);
        progress = 9;
        Screens[9].SetActive(true);
    }
    void TaskOnClickButton9(){
        Screens[9].SetActive(false);
        Time.timeScale = 1;
        QuestText.text = "";
        finishQuest();
    }

}
