using UnityEngine;

public class UIFinalizer : MonoBehaviour,IFinalizable
{
    public void Final()
    {
        this.transform.Find("Score").gameObject.SetActive(false);
        this.transform.Find("HP").gameObject.SetActive(false);
        this.transform.Find("Magazine").gameObject.SetActive(false);
        this.transform.Find("MAP").gameObject.SetActive(false);
        this.transform.Find("Timer").gameObject.SetActive(false);
        this.transform.Find("AIM").gameObject.SetActive(false);

        this.transform.Find("Result").gameObject.SetActive(true);
        this.transform.Find("Button").gameObject.SetActive(true);
    }
}
