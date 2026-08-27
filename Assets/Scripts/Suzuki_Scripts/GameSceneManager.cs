using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GoTitle() { SceneManager.LoadScene("Scene_Title"); }
    public void GoMaching() { SceneManager.LoadScene("所のなかのシーン"); }
    public void GoBuild() { SceneManager.LoadScene("Scene_Build"); }
    public void GoNormal() { SceneManager.LoadScene("Scene_Normal"); }
    public void GoIsland() { SceneManager.LoadScene("Scene_Island"); }
    public void GoCave() { SceneManager.LoadScene("Scene_Cave"); }
    public void GoResult() { SceneManager.LoadScene("Scene_Result"); }


}
