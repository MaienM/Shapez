using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidFlocking : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float pullForce = 1;
    public float randomness = 1;
    public float flockRange = 10;
    public int flockSize = 15;
    public float updateInterval = 1;

    private Vector3 flockCenter;
    private Vector3 flockVelocity;
    private List<GameObject> boids = new List<GameObject>();

    void Start()
    {
        StartCoroutine("FlockUpdater");
    }

    IEnumerator FlockUpdater()
    {
        while (true)
        {
            UpdateFlock();
            UpdateVelocity();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void UpdateFlock()
    {
        // Get all boids.
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag(tag))
        {
            float distance = (transform.position - boid.transform.position).magnitude;
            if (boid.rigidbody != null && distance <= flockRange)
            {
                boids.Add(boid);
            }
        }

        flockCenter = Vector3.zero;
        flockVelocity = Vector3.zero;

        foreach (GameObject boid in boids)
        {
            flockCenter += boid.transform.localPosition;
            flockVelocity += boid.rigidbody.velocity;
        }

        flockCenter /= boids.Count;
        flockVelocity /= boids.Count;
    }

    private void UpdateVelocity()
    {
        rigidbody.velocity += CalcVelocity() * Time.deltaTime * pullForce;

        // Enforce minimum and maximum speeds for the boids
        float speed = rigidbody.velocity.magnitude;
        if (speed > maxVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }
        else if (speed < minVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
        }
    }

    private Vector3 CalcVelocity()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();

        Vector3 center = flockCenter - transform.localPosition;
        Vector3 velocity = flockVelocity - rigidbody.velocity;

        return (center + velocity + randomize * randomness);
    }
}
