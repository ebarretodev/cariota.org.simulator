using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    [SerializeField] BarraCircular barraCircular;
    [SerializeField] Blink icon;
    [SerializeField] SimpleCarController car;
    [SerializeField] ChargeStationController charge;

    [SerializeField] public float tempoMaximo;
    public float tempoAtual;
    private bool consuming;
    private bool charging;
    private float m_input_vertical;
    int chargingCount = 0;

    private void GetInputs(){
        m_input_vertical = Input.GetAxis("Vertical");
    }
    public void DecrementaEnergia()
    {
        consuming = true;
    }

    public void PausaDecrementaEnergia()
    {
        consuming = false;
    }

    public void IncrementaEnergia()
    {
        charging = true;
        StartCoroutine(charge.StartSendValues());
    }

    public void PausaIncrementaEnergia()
    {
        charging = false;
    }

    private void Consuming(){
        if(consuming)
        {
            if (tempoAtual <= 0)
            {
                consuming = false;
                icon.StartBlink(0.5f);
                car.SetSpeed(5);
                return;
            }
            tempoAtual -= Time.deltaTime;

            barraCircular.AtualizarBarra(tempoAtual);


        }
    }

    private void Charging(){

         if(charging){
            if (chargingCount == 0 ){
                icon.StopBlinking();
            }
            chargingCount ++;
            car.SetSpeed(1);
            icon.StartBlink(1f);
            tempoAtual += Time.deltaTime*2;
            barraCircular.AtualizarBarra(tempoAtual);
            if( tempoAtual >= tempoMaximo){
                chargingCount = 0;
                icon.StopBlinking();
                car.ReleaseForceCarBrake();
                charging = false;
                tempoAtual = tempoMaximo;
            }
        }
    }

    private void Start()
    {
        tempoAtual = tempoMaximo;
        barraCircular.DefinirValorMaximo(tempoMaximo);
    }
    private void FixedUpdate(){
        GetInputs();
        Consuming();
        Charging();
        if(m_input_vertical != 0){
            DecrementaEnergia();
        }
        if(m_input_vertical == 0){
            PausaDecrementaEnergia();
        }
    }

}