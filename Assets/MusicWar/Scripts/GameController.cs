using UnityEngine;

public class GameController : MonoBehaviour
{
	void Start ()
    {
        GameObject.Find("Canvas/Timer").GetComponent<TimerUI>().Initialize();
    }
}
