using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class GameConnectionsController : MonoBehaviour {
    [DllImport("__Internal")] private static extern void initialized ();
    public string username = "ebarreto";
    public int balance = 100 ;
    private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYxYmQ2OTQ1MzZkZDBjMzdmMDdkNTBjZiIsInVzZXJuYW1lIjoiZWJhcnJldG8iLCJlbWFpbCI6ImVsaWFiZWxAdGVzdGUuY29tIiwiaWF0IjoxNjM5ODAzMjA1fQ.lO2yG-syRln_zxVCp-wWsFFAvHqj-UMDLTWmJ_e-9gk";
    //ReactVariables
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

    //RestAPI requests
    //private const string API_SITE = "http://localhost:5000";
    private const string API_SITE = "https://api.cariota.org";

    private UnityWebRequest pedido;
    private UnityWebRequest send;

    [SerializeField] BalanceData balanceData;
    private string jsonResposta;

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

    public IEnumerator SendIota(string _address, int _value, string _message){
        WWWForm form = new WWWForm();
        form.AddField("address", _address);
        form.AddField("amount", _value * 1000000);
        form.AddField("message", _message);
        send = UnityWebRequest.Post($"{API_SITE}/iota/sendValue/", form);
        send.SetRequestHeader("Authorization", $"Bearer {token}");
        yield return send.SendWebRequest();
        jsonResposta = send.downloadHandler.text;
        Debug.Log(jsonResposta);
    }

}