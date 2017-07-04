using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UniRx;


public class GameManager : NetworkManager
{
    public string ID { get; set; }
    public string[] MatchingUsers { get { return matching.users; } }
    public Subject<List<ClientInfo>> Startable = new Subject<List<ClientInfo>>();

    MatchingMessage matching = new MatchingMessage();
    ClientMessage clientMessage = new ClientMessage();
    List<ClientInfo> clientInfos = new List<ClientInfo>();

    int i = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("startServer");

        NetworkServer.RegisterHandler(ClientMessageType.MSG_REGISTER, msg =>
        {
            if (matching.playerCount < MatchingMessage.playableCount)
            {
                var message = msg.ReadMessage<ClientMessage>();
                message.team = clientInfos.Count() % 2;
                matching.users[matching.playerCount++] = message.id;
                clientInfos.Add(new ClientInfo(message, msg.conn));
                msg.conn.Send(MatchingMessageType.MSG_MATCHING, matching);
            }
        });

        NetworkServer.RegisterHandler(MatchingMessageType.MSG_MATCHING, msg =>
         {
             if (matching.playerCount < MatchingMessage.playableCount)
             {
                 msg.conn.Send(MatchingMessageType.MSG_MATCHING, matching);
             }
             if (matching.playerCount == MatchingMessage.playableCount)
             {
                 msg.conn.Send(MatchingMessageType.MSG_READY, matching);
                 foreach (var ci in clientInfos)
                 {
                     msg.conn.Send(ClientMessageType.MSG_READY, ci.clientMessage);
                 }
             }
         });

        NetworkServer.RegisterHandler(ClientMessageType.MSG_READY, msg =>
        {
            i++;
            if (i == MatchingMessage.playableCount)
            {
                i = 0;
                Invoke("Invokable", 1.0f);
            }
        });
    }

    void Invokable()
    {
        ServerChangeScene("MusicWar/Scenes/Main"); ;
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        client.RegisterHandler(MatchingMessageType.MSG_MATCHING, msg => {
            matching = msg.ReadMessage<MatchingMessage>();
            client.Send(MatchingMessageType.MSG_MATCHING, matching);
        });

        client.RegisterHandler(MatchingMessageType.MSG_READY, msg => {
            matching = msg.ReadMessage<MatchingMessage>();
            clientInfos.Clear();
        });

        client.RegisterHandler(ClientMessageType.MSG_READY, msg => {
            clientInfos.Add(new ClientInfo(msg.ReadMessage<ClientMessage>(), null));

            if (clientInfos.Count == MatchingMessage.playableCount)
            {
                msg.conn.Send(ClientMessageType.MSG_READY, clientInfos[0].clientMessage);
            }
        });

        client.RegisterHandler(StartMessageType.MSG_START, msg => Startable.OnNext(clientInfos));

        Debug.Log("startClient");
    }

    public void Ready(ClientMessage cm)
    {
        clientMessage = cm;
        clientInfos.Add(new ClientInfo(cm, client.connection));
    }

    public void StartMatching()
    {
        client.Send(ClientMessageType.MSG_REGISTER, clientMessage);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        switch (sceneName)
        {
            case "MusicWar/Scenes/Main":
                StartMessage m = new StartMessage();
                

                foreach (var ci in clientInfos)
                {
                    var playerPrefab = Resources.Load(ci.clientMessage.modelName) as GameObject;
                    var player = Instantiate<GameObject>(playerPrefab);

                    NetworkServer.AddPlayerForConnection(ci.connection, player, 0);
                }

                foreach (var ci in clientInfos)
                {
                    ci.connection.Send(StartMessageType.MSG_START, m);
                }

                matching.playerCount = 0;
                matching.users[0] = "";
                matching.users[1] = "";
                clientInfos.Clear();

                break;

            default:
                break;
        }
    }
}
