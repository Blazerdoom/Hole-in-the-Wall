using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnWall", 1f, 2f);
    }

    private void SpawnWall()
    {
        Vector3 spot = spawnPoint.position;
        Instantiate(wallPrefab, spot, wallPrefab.transform.rotation);
    }
}
