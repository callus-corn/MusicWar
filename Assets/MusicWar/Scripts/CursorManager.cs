using UnityEngine;
using UniRx;

public class CursorManager : MonoBehaviour {

    public bool hoge = true;

    private void Start()
    {
        
    }

    void Update()
    {
        if (GameObject.Find("Ethan(Clone)") && hoge)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hoge = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            hoge = true;
        }
    }
}
