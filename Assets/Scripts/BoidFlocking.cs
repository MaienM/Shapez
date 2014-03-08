﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidFlocking : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public float range = 10;
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
        foreach (GameObject boid in GameObject.FindGameObjectsWithTag(tag))
        {
            if ((transform.position - boid.transform.position).magnitude <= range)
            {
                boids.Add(boid);
            }
        }

        int flockSize = boids.Count;
        flockCenter = Vector3.zero;
        flockVelocity = Vector3.zero;

        foreach (GameObject boid in boids)
        {
            flockCenter += boid.transform.localPosition;
            flockVelocity += boid.rigidbody.velocity;
        }

        flockCenter /= flockSize;
        flockVelocity /= flockSize;
    }

    private void UpdateVelocity()
    {
        rigidbody.velocity = rigidbody.velocity + CalcVelocity() * Time.deltaTime;

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
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();

        Vector3 center = flockCenter - transform.localPosition;
        Vector3 velocity = flockVelocity - rigidbody.velocity;

        return (center + velocity + randomize * randomness);
    }
}
