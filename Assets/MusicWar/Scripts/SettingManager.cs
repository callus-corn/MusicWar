using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SettingManager : MonoBehaviour
{
    ClientMessage info = new ClientMessage();
    GameManager manager;
    GameObject canvas;
    GameObject _wepon;
    GameObject _player;

    private void Start()
    {
        var prefab = Resources.Load("Prefabs/Ethan") as GameObject;
        var player = GameObject.Instantiate(prefab);
        var weponPrefab = Resources.Load("Prefabs/Wepon") as GameObject;
        var wepon = Instantiate<GameObject>(weponPrefab);
        _player = player;
        _wepon = wepon;
        canvas = GameObject.Find("Canvas");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player.name = manager.ID;
        wepon.transform.parent = player.transform;
        info.id = manager.ID;
        info.modelName = "Prefabs/Ethan";
        info.weponName = "Prefabs/Wepon";
    }

    public void Setting()
    {
        var wepons = canvas.transform.Find("Wepons").gameObject;
        var models = canvas.transform.Find("Models").gameObject;

        wepons.SetActive(!wepons.activeSelf);
        models.SetActive(!models.activeSelf);
    }

    public void OnTauchWeponButtonOne()
    {
        info.weponName = "Prefabs/Wepon";
        ChangeWepon();
    }

    public void OnTauchWeponButtonTwo()
    {
        info.weponName = "Prefabs/Wepon(45)";
        ChangeWepon();
    }

    public void OnTauchWeponButtonThree()
    {
        info.weponName = "Prefabs/Wepon(90)";
        ChangeWepon();
    }

    public void OnTauchModelButtonOne()
    {
        info.modelName = "Prefabs/Ethan";
        ChangePlayer();
    }

    public void OnTauchModelButtonTwo()
    {
        info.modelName = "Prefabs/Ethan(Red)";
        ChangePlayer();
    }

    public void OnTauchModelButtonThree()
    {
        info.modelName = "Prefabs/Ethan(Blue)";
        ChangePlayer();
    }

    private void ChangeWepon()
    {
        Destroy(_wepon);
        var weponPrefab = Resources.Load(info.weponName) as GameObject;
        var wepon = Instantiate<GameObject>(weponPrefab);
        _wepon = wepon;
        _wepon.transform.parent = _player.transform;
    }

    private void ChangePlayer()
    {
        _wepon.transform.parent = null;
        Destroy(_player);
        var prefab = Resources.Load(info.modelName) as GameObject;
        var player = GameObject.Instantiate(prefab);
        player.name = manager.ID;
        _player = player;
        _wepon.transform.parent = _player.transform;
    }

    public void StartMatching()
    {
        manager.Ready(info);
        SceneManager.LoadScene("Matching");
    }

}
