using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoidColor 
{
    PURPLE,
    GREEN,
    ORANGE,
    PINK,
    BLUE,
}

public enum BoidShape 
{
    TRIANGLE,
    SQUARE,
    RECTANGLE,
    CIRCLE,
    PENTAGON,
}

public class BoidFlocking : MonoBehaviour
{
    public BoidColor color;
    public BoidShape shape;

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
            yield return new WaitForSeconds(Constants.updateInterval);
        }
    }

    private void UpdateFlock()
    {
        // Get all boids.
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag(tag))
        {
            float distance = (transform.position - boid.transform.position).magnitude;
            if (boid.rigidbody != null && distance <= Constants.flockRange)
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
        rigidbody.velocity += CalcVelocity() * Time.deltaTime * Constants.pullForce;

        // Enforce minimum and maximum speeds for the boids
        float speed = rigidbody.velocity.magnitude;
        if (speed > Constants.maxVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * Constants.maxVelocity;
        }
        else if (speed < Constants.minVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * Constants.minVelocity;
        }
    }

    private Vector3 CalcVelocity()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();

        Vector3 center = flockCenter - transform.localPosition;
        Vector3 velocity = flockVelocity - rigidbody.velocity;

        return (center + velocity + randomize * Constants.randomness);
    }
}
