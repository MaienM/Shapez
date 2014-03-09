using System.Collections.Generic;
using UnityEngine;

class FlockSpawner : MonoBehaviour
{
    public Vector3 spawnRange = new Vector3(10, 10, 10);
    public int spawnCount = 5;
    public List<GameObject> prefabs = new List<GameObject>();
    private KeyCode[] levelKeys = new KeyCode[] {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9
    };

    public void Update()
    {
        int i = 1;
        foreach (KeyCode key in levelKeys)
        {
            if (Input.GetKeyDown(key))
            {
                StartLevel(i);
            }
            i++;
        }
    }

    private void StartLevel(int level)
    {
        int numberOfBirds = level * 10;
        int neededToWin = (int)(numberOfBirds * (0.95f - 0.05f * level));
        int hpOfBird = neededToWin;
        int colorRange = Mathf.FloorToInt(level / 2f + 1);
        int shapeRange = Mathf.CeilToInt(level / 2f);
        int variants = colorRange * shapeRange;

        // Remove all boids.
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag("Boid"))
        {
            DestroyImmediate(boid);
        }

        // Spawn new boids.
        for (int i = 0; i < numberOfBirds / Constants.flockSize; i++)
        {
            BoidColor color = (BoidColor)(i % colorRange);
            BoidShape shape = (BoidShape)((i / colorRange) % shapeRange);
            SpawnFlock(color, shape);
        }
    }

    private void SpawnFlock(BoidColor color, BoidShape shape)
    {
        // Determine which prefab to use.
        GameObject prefab = null;
        foreach (GameObject p in prefabs)
        {
            BoidFlocking bf = p.GetComponent<BoidFlocking>();
            if (bf.color == color && bf.shape == shape)
            {
                prefab = p;
                break;
            }
        }
        if (prefab == null)
        {
            Debug.Log("Could not find prefab for: " + color.ToString() + ", " + shape.ToString());
        }

        // Pick a position to spawn the flock at.
        Vector3 flockPosition = new Vector3(Random.Range(-Constants.spawnRange, Constants.spawnRange),
                                            Random.Range(-Constants.spawnRange, Constants.spawnRange), 
                                            0);
        for (int i = 0; i < Constants.flockSize; i++)
        {
            SpawnBird(prefab, flockPosition);
        }
    }

    private void SpawnBird(GameObject prefab, Vector3 position)
    {
        Vector3 offset = new Vector3(Random.Range(-Constants.flockSpread, Constants.flockSpread),
                                     Random.Range(-Constants.flockSpread, Constants.flockSpread),
                                     Random.Range(-Constants.flockSpread, Constants.flockSpread));
        Instantiate(prefab, position + offset, Quaternion.identity);
    }
}

