using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkManager
{
    int test=0;

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        test++;
        if (test > 1) StartBattle();
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        ClientScene.AddPlayer(0);
    }

    private void StartBattle()
    {
        ServerChangeScene("MusicWar/Scenes/Main");
    }
}
