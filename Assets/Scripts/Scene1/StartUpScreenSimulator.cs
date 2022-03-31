using UnityEngine;
using UnityEngine.UI;

public class StartUpScreenSimulator : MonoBehaviour{
    [SerializeField] GameConnectionsController con;
    public Text username;

    public GameObject ScreenToShow;
    void Start(){

        ScreenToShow.SetActive(true);
    }

    public void CloseScreen(){
        ScreenToShow.SetActive(false);
    }

    private void FixedUpdate(){
        username.text = $"Hi {con.username},";
    }
}