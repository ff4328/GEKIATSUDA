using UnityEngine;

public class ResultScene : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < ResultSceneData.ranking.Length; i++)
        {
            Debug.Log($"{i + 1}位 : {ResultSceneData.ranking[i]}");
        }
    }
}
