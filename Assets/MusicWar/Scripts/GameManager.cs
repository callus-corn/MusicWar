using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkManager
{

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);

        Debug.Log(ClientScene.localPlayers[0]);

        var hpUI = GameObject.Find("Canvas/HP").GetComponent<HPUI>();
        var magazineUI = GameObject.Find("Canvas/Magazine").GetComponent<MagazineUI>();
        var timerUI = GameObject.Find("Canvas/Timer").GetComponent<TimerUI>();
        var scoreUI = GameObject.Find("Canvas/Score").GetComponent<ScoreUI>();

        hpUI.Initialize();
        magazineUI.Initialize();
        timerUI.Initialize();
        scoreUI.Initialize();
    }
}
