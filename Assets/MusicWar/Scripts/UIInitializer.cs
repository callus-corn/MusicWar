using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitializer :MonoBehaviour, IInitializable
{
    public void Initialize()
    {
        this.transform.Find("HP").GetComponent<HPUI>().Initialize();
        this.transform.Find("Magazine").GetComponent<MagazineUI>().Initialize();
        this.transform.Find("Score").GetComponent<ScoreUI>().Initialize();
        this.transform.Find("Timer").GetComponent<TimerUI>().Initialize();
    }
}
