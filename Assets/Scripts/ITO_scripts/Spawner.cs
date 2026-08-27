using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ItemPrefabs;
    [SerializeField] private Transform LeftTop;
    [SerializeField] private Transform RightBottom;

    private float minX, maxX, minY, maxY;

    private void Start()
    {
        minX = LeftTop.position.x;
        maxX = RightBottom.position.x;
        minY = RightBottom.position.y;
        maxY = LeftTop.position.y;
        
        StartCoroutine(SpawnItem());
    }
    private IEnumerator SpawnItem()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            GameObject enemy = ItemPrefabs[Random.Range(0, ItemPrefabs.Length)];
            Instantiate(enemy, position, Quaternion.identity, transform);
            yield return new WaitForSeconds(10f);
        }
    }
}
