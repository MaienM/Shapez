using UnityEngine;

class FlockSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 spawnRange = new Vector3(10, 10, 10);
    public int spawnCount = 5;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            for (var i = 0; i < spawnCount; i++)
            {
                Vector3 boidPosition = position + new Vector3(
                    Random.value * spawnRange.x,
                    Random.value * spawnRange.y,
                    Random.value * spawnRange.z
                );

                GameObject boid = Instantiate(prefab, boidPosition, Quaternion.identity) as GameObject;
            }
        }
    }
}

