using UnityEngine;
using UnityEngine.UI;

public class StartUpScreenSimulator : MonoBehaviour{
    [SerializeField] GameConnectionsController con;

    private void FixedUpdate(){
        Transform text = transform.Find($"Username");
        Text UsernameText = text.GetComponent<Text>();
        UsernameText.text = $"{con.username}";
    }
}