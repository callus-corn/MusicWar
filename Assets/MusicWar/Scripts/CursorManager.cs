using UnityEngine;

public class CursorManager : MonoBehaviour {

    bool hoge = true;

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
