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

    public void GoTitle() {
        LoadingManager.nextScene = "Scene_Title" ; 
        SceneManager.LoadScene("Scene_Loading");
    }
    public void GoMaching() {
        LoadingManager.nextScene = "Scene_Maching";
        SceneManager.LoadScene("Scene_Loading");
    }
    public void GoBuild() {
        LoadingManager.nextScene = "Scene_Build";
        SceneManager.LoadScene("Scene_Loading");
    }
    public void GoNormal() {
        LoadingManager.nextScene = "Scene_Normal";
        SceneManager.LoadScene("Scene_Loading");
    }
    public void GoIsland() {
        LoadingManager.nextScene = "Scene_Island";
        SceneManager.LoadScene("Scene_Loading");
    }
    public void GoCave() {
        LoadingManager.nextScene = "Scene_Cave";
        SceneManager.LoadScene("Scene_Loading");
    }
    public void GoResult() {
        LoadingManager.nextScene = "Scene_Result";
        SceneManager.LoadScene("Scene_Loading");
    }


}
