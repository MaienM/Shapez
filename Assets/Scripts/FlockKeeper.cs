using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FlockKeeper : MonoBehaviour
{
    public Vector3 flockCenter;
    public Vector3 flockVelocity;
    public List<GameObject> boids = new List<GameObject>();

    void Start()
    {
        StartCoroutine("FlockUpdater");
    }

    IEnumerator FlockUpdater()
    {
        while (true)
        {
            BoidFlocking bf = GetComponent<BoidFlocking>();

            foreach (GameObject boid in GameObject.FindGameObjectsWithTag(tag))
            {
                if ((transform.position - boid.transform.position).magnitude <= bf.range)
                {
                    boids.Add(boid);
                }
            }

            int flockSize = boids.Count;
            Vector3 theCenter = Vector3.zero;
            Vector3 theVelocity = Vector3.zero;

            foreach (GameObject boid in boids)
            {
                theCenter = theCenter + boid.transform.localPosition;
                theVelocity = theVelocity + boid.rigidbody.velocity;
            }

            flockCenter = theCenter / flockSize;
            flockVelocity = theVelocity / flockSize;

            yield return new WaitForSeconds(1);
        }
    }
}
