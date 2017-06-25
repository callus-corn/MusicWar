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
            var wepon = player.transform.Find("Wepon").gameObject;
            var _camera = player.transform.Find("PlayerCamera").gameObject;

            player.name = manager.matching.users[id.netId.Value - 1];

            wepon.GetComponent<Renderer>().material.color = (id.netId.Value % 2 ==0)  ? Color.red : Color.blue;

            switch (id.netId.Value)
            {
                case 1:
                    player.transform.position = new Vector3(1, 0, 1);
                    break;
                case 2:
                    player.transform.position = new Vector3(-1, 0, 1);
                    break;
                case 3:
                    player.transform.position = new Vector3(-1, 0, -1);
                    break;
                case 4:
                    player.transform.position = new Vector3(1, 0, -1);
                    break;
            }

            player.GetComponent<PlayerInput>().Initialize();
            player.GetComponent<NetworkTransform>().Initialize();
            player.GetComponent<PlayerMover>().Initialize();
            player.GetComponent<PlayerAnimator>().Initialize();
            player.GetComponent<PlayerAttacker>().Initialize();

            wepon.GetComponent<BaseWepon>().Initialize();

            if (player.name != manager.ID)
            {
                _camera.GetComponent<Camera>().enabled = false;
                _camera.GetComponent<AudioListener>().enabled = false;
            }
            _camera.SetActive(true);
            _camera.GetComponent<CameraMover>().Initialize();
        }

    }

}
