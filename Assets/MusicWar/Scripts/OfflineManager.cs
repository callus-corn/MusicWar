using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

public class OfflineManager : MonoBehaviour
{
    string _IP;
    string _ID;
    string _PASS;
    int _port = 3000;

    NetworkClient _client;
    GameManager _manager;

    IObservable<WWW> startable;

    public void StartConnect()
    {
        _IP = GameObject.Find("Canvas/IP").GetComponent<InputField>().text;
        _ID = GameObject.Find("Canvas/ID").GetComponent<InputField>().text;
        _PASS = GameObject.Find("Canvas/PASS").GetComponent<InputField>().text;
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _manager.ID = _ID;
        _manager.networkAddress = _IP;
        _manager.networkPort = _port;

        if (_ID == "server")
        {
            _manager.StartServer();
        }
        else if (_PASS != "")
        {
            string hashPassword = CalcSha256(_PASS);
            WWWForm form = new WWWForm();

            form.AddField("userId", _ID);
            form.AddField("password", hashPassword);
            startable = ObservableWWW.PostWWW("http://" + _IP + ":" + _port + "/login", form);

            startable
                .Where(www => www.responseHeaders["STATUS"].Contains("200"))
                .Subscribe(www =>
                {
                    Debug.Log(www.text);
                    _manager.StartClient();
                    SceneManager.LoadScene("Setting");
                });
        }
        else
        {
            _manager.StartClient();
            SceneManager.LoadScene("Setting");
        }
    }

    private string CalcSha256(string planePassword)
    {
        SHA256 sha = new SHA256CryptoServiceProvider();
        byte[] planeBytes = new UTF8Encoding().GetBytes(planePassword);
        byte[] hashBytes = sha.ComputeHash(planeBytes);
        string hashPassword = "";
        foreach (byte b in hashBytes)
        {
            hashPassword += string.Format("{0:x2}", b);
        }
        Debug.Log("Calculate Hash: " + hashPassword);
        return hashPassword;
    }
}
