using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineManager : MonoBehaviour
{
    string _IP;
    string _ID;
    int _port = 12345;

    NetworkClient _client;
    GameManager _manager;

    public void StartConnect()
    {
        _IP = GameObject.Find("Canvas/IP").GetComponent<InputField>().text;
        _ID = GameObject.Find("Canvas/ID").GetComponent<InputField>().text;
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _manager.ID = _ID;
        _manager.networkAddress = _IP;
        _manager.networkPort = _port;

        if (_ID == "server")
        {
            _manager.StartServer();
        }
        else
        {
            _manager.StartClient();
            SceneManager.LoadScene("Setting");
        }
    }
}
