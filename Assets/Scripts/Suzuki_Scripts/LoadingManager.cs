using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(nextScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}