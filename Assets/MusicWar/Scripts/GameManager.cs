using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


public class GameManager : NetworkManager
{
    public string ID { get; set; }
    public MatchingMessage matching = new MatchingMessage();
    public List<ClientMessage> clientMessages = new List<ClientMessage>();
    public List<NetworkConnection> connections = new List<NetworkConnection>();

    int i = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("startServer");

        NetworkServer.RegisterHandler(ClientMessageType.MSG_REGISTER, msg =>
        {
            var message = msg.ReadMessage<ClientMessage>();
            matching.users[matching.playerCount++] = message.id;
            clientMessages.Add(message);
            connections.Add(msg.conn);
            msg.conn.Send(MatchingMessageType.MSG_MATCHING, matching);
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
                 foreach (var cl in clientMessages)
                 {
                     msg.conn.Send(ClientMessageType.MSG_READY,cl);
                 }
             }
         });

        NetworkServer.RegisterHandler(ClientMessageType.MSG_READY, msg =>
        {
            i++;
            if (i == MatchingMessage.playableCount)
            {
                ServerChangeScene("MusicWar/Scenes/Main");
            }
        });
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        clientMessages.Add(new ClientMessage());
        clientMessages[0].id = ID;
        Debug.Log("startClient");
    }

    public void Ready()
    {
        client.RegisterHandler(MatchingMessageType.MSG_MATCHING, msg => {
            matching = msg.ReadMessage<MatchingMessage>();
            client.Send(MatchingMessageType.MSG_MATCHING, matching);
        });
        client.RegisterHandler(MatchingMessageType.MSG_READY, msg => {
            matching = msg.ReadMessage<MatchingMessage>();
            clientMessages.Clear();
        });
        client.RegisterHandler(ClientMessageType.MSG_READY, msg => {
            clientMessages.Add(msg.ReadMessage<ClientMessage>());
            
            if(clientMessages.Count == MatchingMessage.playableCount)
            {
                msg.conn.Send(ClientMessageType.MSG_READY,clientMessages[0]);
            }
        });

        client.RegisterHandler(StartMessageType.MSG_START, msg =>{
            GameObject.Find("GameController").GetComponent<GameController>().Initialize();
        });


        client.Send(ClientMessageType.MSG_REGISTER, clientMessages[0]);        
    }

        public override void OnServerSceneChanged(string sceneName)
        {
            base.OnServerSceneChanged(sceneName);

            for (int j = 0 ; j < connections.Count ; j++)
            {
                var prefab = Resources.Load("Prefabs/Ethan") as GameObject;
                var player = Instantiate<GameObject>(prefab);
                player.name = clientMessages[j].id;

                NetworkServer.AddPlayerForConnection(connections[j], player,0);
            }

            var m = new StartMessage();
            foreach (var co in connections)
            {
                co.Send(StartMessageType.MSG_START,m);
            }
        }
}
