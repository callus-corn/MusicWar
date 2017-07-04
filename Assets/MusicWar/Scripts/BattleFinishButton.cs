using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFinishButton : MonoBehaviour
{
    public void OnClicked()
    {
        SceneManager.LoadScene("MusicWar/Scenes/Setting");
    }
}
