using UnityEngine;
using System.Collections;

public class BoidFlocking : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public float range = int.MaxValue;

    void Start()
    {
        StartCoroutine("BoidSteering");
    }

    IEnumerator BoidSteering()
    {
        while (true)
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

            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 CalcVelocity()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();

        FlockKeeper boidController = GetComponent<FlockKeeper>();
        Vector3 flockCenter = boidController.flockCenter;
        Vector3 flockVelocity = boidController.flockVelocity;

        flockCenter = flockCenter - transform.localPosition;
        flockVelocity = flockVelocity - rigidbody.velocity;

        return (flockCenter + flockVelocity + randomize * randomness);
    }
}