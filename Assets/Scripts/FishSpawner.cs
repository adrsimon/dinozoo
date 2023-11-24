using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject littleFish;
    public GameObject midFish;
    public GameObject bigFish;
    public int maxFish = 20;
    public float spawnRadius = 6f;
    public float spawnHeight = -1f;
    private int fishCount;

    void Start()
    {
        for (int i = 0; i < maxFish; i++)
        {
            SpawnFish();
        }
    }

    void Update()
    {
        if (fishCount < maxFish)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = spawnHeight;
        GameObject fishPrefab = SelectFishPrefab();
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        fishCount++;
    }

    GameObject SelectFishPrefab()
    {
        int randomValue = Random.Range(0, 6);
        if (randomValue < 3)
        {
            return littleFish;
        }
        else if (randomValue < 5)
        {
            return midFish;
        }
        else
        {
            return bigFish;
        }
    }

    public void DestroyFish(GameObject fish)
    {
        Destroy(fish);
        fishCount--;
    }
}