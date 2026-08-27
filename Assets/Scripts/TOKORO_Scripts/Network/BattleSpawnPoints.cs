using UnityEngine;

public class BattleSpawnPoints : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    public Transform GetSpawnPoint(int playerNumber)
    {
        int index = playerNumber - 1;

        if (index < 0 || index >= spawnPoints.Length)
            return null;

        return spawnPoints[index];
    }
}