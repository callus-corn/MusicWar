using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public void Initialize()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach (var player in players)
        {
            var id = player.GetComponent<NetworkIdentity>();
            player.name = manager.matching.users[id.netId.Value - 1];
            player.GetComponent<PlayerInput>().Initialize();
            player.GetComponent<NetworkTransform>().Initialize();
            player.GetComponent<PlayerMover>().Initialize();
        }

        var _camera = GameObject.Find(manager.ID).transform.Find("PlayerCamera").gameObject;
        _camera.SetActive(true);
        _camera.GetComponent<CameraMover>().Initialize();


    }

}
