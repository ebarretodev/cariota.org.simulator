using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewControler : MonoBehaviour {
    [SerializeField] GameConnectionsController con;
    public Text username;
    public Text balance;

    void FixedUpdate(){
        username.text = con.username;
        balance.text = $"{con.balance} Miota";
    }
}