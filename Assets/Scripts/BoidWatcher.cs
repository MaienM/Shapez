using UnityEngine;
using System.Collections;

public class BoidWatcher : MonoBehaviour
{
    public float positionEase = 0.99f;
    public float sizeEase = 0.99f;
    public float borderPct = 0.2f;
    public int minSize = 20;
    public int maxSize = 100;
	
	void Update() 
    {
        float minX = float.MaxValue,
              maxX = float.MinValue,
              minY = float.MaxValue,
              maxY = float.MinValue;
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag("Boid"))
        {
            if (boid.transform.position.x < minX)
            {
                minX = boid.transform.position.x;
            }
            else if (boid.transform.position.x > maxX)
            {
                maxX = boid.transform.position.x;
            }

            if (boid.transform.position.y < minY)
            {
                minY = boid.transform.position.y;
            }
            else if (boid.transform.position.y > maxY)
            {
                maxY = boid.transform.position.y;
            }
        }

        Vector3 oldPos = Camera.main.transform.position;
        Vector3 newPos = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, -200);
        Camera.main.transform.position = oldPos * positionEase + newPos * (1 - positionEase);

        float oldSize = Camera.main.orthographicSize;
        float newSize = Mathf.Max(maxX - minX, maxY - minY, 0) * (0.5f + borderPct / 2);
        Camera.main.orthographicSize = Mathf.Clamp(oldSize * sizeEase + newSize * (1 - sizeEase), minSize, maxSize);
	}
}
