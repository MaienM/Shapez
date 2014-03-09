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

    public List<GameObject> boids = new List<GameObject>();

    void Start()
    {
        StartCoroutine("FlockUpdater");
        StartCoroutine("VelocityUpdater");
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         0);
    }

    IEnumerator FlockUpdater()
    {
        while (true)
        {
            UpdateFlock();
            yield return new WaitForSeconds(Constants.flockUpdateInterval);
        }
    }

    IEnumerator VelocityUpdater()
    {
        while (true)
        {
            UpdateVelocity();
            yield return new WaitForSeconds(Constants.velocityUpdateInterval);
        }
    }

    private void UpdateFlock()
    {
        // Get all boids.
        boids.Clear();
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag(tag))
        {
            // In range, not ourselves and not in an invalid state.
            float distance = (transform.position - boid.transform.position).magnitude;
            if (boid.rigidbody != null && boid != this && distance <= Constants.flockRange)
            {
                // Add to list depending on color/shape match.
                BoidFlocking bf = boid.GetComponent<BoidFlocking>();
                if (shape == bf.shape)
                {
                    boids.Add(boid);
                }
                if (color == bf.color)
                {
                    boids.Add(boid);
                }

                if (boids.Count >= Constants.localFlockSize)
                {
                    break;
                }
            }
        }
    }

    private void UpdateVelocity()
    {
        rigidbody.velocity += (CalcToCenter() + CalcSeparation() + CalcMatchVelocity()) * Time.deltaTime;

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

    private Vector3 CalcToCenter()
    {
        Vector3 flockCenter = Vector3.zero;
        if (boids.Count == 0)
        {
            return flockCenter;
        }
        foreach (GameObject boid in boids)
        {
            flockCenter += boid.transform.position;
        }
        flockCenter /= boids.Count;
        flockCenter -= transform.position;
        return flockCenter;
    }

    private Vector3 CalcSeparation()
    {
        Vector3 separation = Vector3.zero;
        foreach (GameObject boid in boids)
        {
            Vector3 diff = boid.transform.position - transform.position;
            if (diff.magnitude < 5)
            {
                separation -= diff;
            }
        }
        return separation;
    }

    private Vector3 CalcMatchVelocity()
    {
        Vector3 flockVelocity = Vector3.zero;
        if (boids.Count == 0)
        {
            return flockVelocity;
        }
        foreach (GameObject boid in boids)
        {
            flockVelocity += boid.rigidbody.velocity;
        }
        flockVelocity /= boids.Count;
        flockVelocity /= 8;
        return flockVelocity;
    }
}

