using UnityEngine;

class FlockSpawner : MonoBehaviour
{
    public GameObject prefab;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}

