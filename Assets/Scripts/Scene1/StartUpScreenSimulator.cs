using UnityEngine;
using UnityEngine.UI;

public class StartUpScreenSimulator : MonoBehaviour{
    [SerializeField] GameConnectionsController con;

    private void Update(){
        Transform text = transform.Find($"Username");
        Text UsernameText = text.GetComponent<Text>();
        UsernameText.text = $"{con.username}";
    }
}