using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SettingManager : MonoBehaviour
{

    private void Start()
    {
        var prefab = Resources.Load("Prefabs/Ethan") as GameObject;
        var player = GameObject.Instantiate(prefab);
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.transform.Rotate(0,180,0);
        player.name = manager.ID;
        manager.clientMessages[0].modelNo = 0;
        manager.clientMessages[0].weponNo = 0;
    }

    public void StartMatching()
    {
        SceneManager.LoadScene("Matching");
    }

}
