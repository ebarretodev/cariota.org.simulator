using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class GameConnectionsController : MonoBehaviour {
    [DllImport("__Internal")] private static extern void initialized ();
    public string username = "ebarreto";
    public int balance = 100 ;
    private int testeRes;
    private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYxYmQ2OTQ1MzZkZDBjMzdmMDdkNTBjZiIsInVzZXJuYW1lIjoiZWJhcnJldG8iLCJlbWFpbCI6ImVsaWFiZWxAdGVzdGUuY29tIiwiaWF0IjoxNjM5ODAzMjA1fQ.lO2yG-syRln_zxVCp-wWsFFAvHqj-UMDLTWmJ_e-9gk";
    //ReactVariables
    public string address = "atoi1qrqg52x0kasvakw2m6aruqfvafs6lzwjm6tummet3rgj9yrs7kkuuzjrqnk";
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

    //RestAPI requests
    //private const string API_SITE = "http://localhost:5000";
    private const string API_SITE = "https://api.cariota.org";

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

        StartCoroutine(GetBalance());
    }
    private IEnumerator GetBalance(){
        pedido = UnityWebRequest.Get($"{API_SITE}/iota/balance/");
        pedido.SetRequestHeader("Authorization", $"Bearer {token}");
        yield return pedido.SendWebRequest();
        jsonResposta = pedido.downloadHandler.text;
        balanceData = JsonUtility.FromJson<BalanceData>(jsonResposta);
        balance = balanceData.balance / 1000000;

        StartCoroutine(GetBalance());
    }

    void FixedUpdate()
    {
    }

    public IEnumerator SendIota(string _address, int _value, string _message, string _token = null){
        ConsoleField.text = _message;
        WWWForm form = new WWWForm();
        form.AddField("address", _address);
        form.AddField("amount", _value * 1000000);
        form.AddField("message", _message);
        send = UnityWebRequest.Post($"{API_SITE}/iota/sendValue/", form);
        if (_token == null){
            send.SetRequestHeader("Authorization", $"Bearer {token}");
        }
        else{
            send.SetRequestHeader("Authorization", $"Bearer {_token}");
        }
        yield return send.SendWebRequest();
        jsonResposta = send.downloadHandler.text;
        Debug.Log(jsonResposta);
        testeRes = jsonResposta.Length;
        if(testeRes == 66){
            ConsoleField.text = "Message confirmed on Tangle";
        } else {
            ConsoleField.text = "Message rejected by server, contact the Admin.";
        }
        ConsoleField.text = "Message confirmed on Tangle";
        yield return new WaitForSeconds(5);
        ConsoleField.text = "";
    }

}