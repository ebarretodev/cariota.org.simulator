using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class GameConnectionsController : MonoBehaviour {
    [DllImport("__Internal")] private static extern void initialized ();
    public string username = "eliabel";
    public int balance = 10 ;
    private int testeRes;
    public string token = "CtJigYE1jBdkwT5tAi5gETAsnqv1";
    //ReactVariables
    public string address = "atoi1qqnw8k5nhz5une6r40wue922zys2vp2x3x6tp80gj4chmgvxje376xepwvr";
    public void GetUsername(string _username){
        if(username == _username ){
            return;
        }
        username = _username;
        Debug.Log($"User Selected: {username}");
    }

    public void GetToken(string _token){
        if(token == _token ){
            return;
        }
        token = _token;
    }

    public void GetAddress(string _address){
        if(address == _address ){
            return;
        }
        address = _address;
    }
    public void GetBalance(int _balance){
        if(balance == _balance ){
            return;
        }
        balance = _balance;
    }

    // //RestAPI requests
    // private const string API_SITE = "http://localhost:5001/cariota-b56d7/us-central1/main/api/v1";
    private const string API_SITE = "https://cariota-b56d7.web.app/api/v1";

    private UnityWebRequest pedido;
    private UnityWebRequest send;

    [SerializeField] BalanceData balanceData;
    private string jsonResposta;

    public Text ConsoleField;

    private void Start()
    {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            initialized();
        #endif

        // StartCoroutine(GetBalance());
    }
    // private IEnumerator GetBalance(){
    //     WWWForm form = new WWWForm();
    //     form.AddField("token", token);
    //     pedido = UnityWebRequest.Post($"{API_SITE}/iota/balance/", form);
    //     yield return pedido.SendWebRequest();
    //     jsonResposta = pedido.downloadHandler.text;
    //     balanceData = JsonUtility.FromJson<BalanceData>(jsonResposta);
    //     balance = balanceData.balance / 1000000;

    //     StartCoroutine(GetBalance());
    // }

    void FixedUpdate()
    {
    }

    public IEnumerator SendIota(string _address, int _value, string _message, string _token = null){
        ConsoleField.text = _message;
        if(_token == null){
            _token = token;
        }
        WWWForm form = new WWWForm();
        form.AddField("address", _address);
        form.AddField("amount", _value * 1000000);
        form.AddField("message", _message);
        form.AddField("token", _token);
        send = UnityWebRequest.Post($"{API_SITE}/iota/sendValue/", form);
        yield return send.SendWebRequest();
        if(send.result != UnityWebRequest.Result.Success){
            ConsoleField.text = "Message rejected by server, contact the Admin.";
        } else {
            ConsoleField.text = "Message confirmed on Tangle";
        }
        yield return new WaitForSeconds(5);
        ConsoleField.text = "";
    }

}