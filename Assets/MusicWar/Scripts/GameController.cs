using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using System;

public class GameController : MonoBehaviour
{
    List<ClientInfo> clients;
    GameObject ui;
    GameObject[] players;
    GameManager manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        manager.Startable
            .First()
            .Subscribe(infos => 
            {
                clients = infos;
                Initialize();
            });
    }

    public void Initialize()
    {
        Debug.Log("Initialize");
        players = GameObject.FindGameObjectsWithTag("Player");
        ui = GameObject.Find("UI");

        int i = -1;
        foreach (var player in players)
        {
            i++;
            var weponPrefab = Resources.Load(clients[i].clientMessage.weponName) as GameObject;
            var wepon = Instantiate<GameObject>(weponPrefab);
            var _camera = player.transform.Find("PlayerCamera").gameObject;

            player.name = clients[i].clientMessage.id;
            wepon.name = "Wepon";
            wepon.transform.parent = player.transform;
            if (manager.ID != clients[i].clientMessage.id)
            {
                _camera.GetComponent<Camera>().enabled = false;
                _camera.GetComponent<AudioListener>().enabled = false;
            }

            switch (clients[i].clientMessage.team)
            {
                case 0:
                    wepon.GetComponent<Renderer>().material.color = Color.red;
                    switch (i % 4)
                    {
                        case 0:
                            player.transform.position = new Vector3(1, 0, 0);
                            break;
                        case 1:
                            player.transform.position = new Vector3(2, 0, 1);
                            break;
                        case 2:
                            player.transform.position = new Vector3(2, 0, -1);
                            break;
                        case 3:
                            player.transform.position = new Vector3(2, 0, 0);
                            break;
                    }
                    break;
                case 1:
                    wepon.GetComponent<Renderer>().material.color = Color.blue;
                    switch (i % 4)
                    {
                        case 0:
                            player.transform.position = new Vector3(-1, 0, 0);
                            break;
                        case 1:
                            player.transform.position = new Vector3(-2, 0, 1);
                            break;
                        case 2:
                            player.transform.position = new Vector3(-2, 0, -1);
                            break;
                        case 3:
                            player.transform.position = new Vector3(-2, 0, 0);
                            break;
                    }
                    break;
            }

            player.GetComponent<PlayerInitializer>().startPosition = player.transform.position;
            player.GetComponent<PlayerInitializer>().startRotetion = player.transform.rotation;

            player.GetComponent<IInitializable>().Initialize();
            wepon.GetComponent<IInitializable>().Initialize();
            _camera.GetComponent<IInitializable>().Initialize();
        }

        ui.GetComponent<IInitializable>().Initialize();

        this.GetComponent<IInitializable>().Initialize();

        this.GetComponent<Timer>().Time
            .Where(time => time <= 0)
            .Subscribe(_ => Final());

    }

    void Final()
    {
        ui.GetComponent<IFinalizable>().Final();
/*
        manager.SendResult(this.GetComponent<Score>().PlayerScore.Value)
            .Subscribe(result => ui.GetComponent<Result>().ApplyResult(result));
            */
    }    

}
