using UnityEngine;
using System.Collections;

public class BoidMover : MonoBehaviour {
    string prevDir;
    string currDir;
    Vector3 prevPos;
    Vector3 currPos;

	// Use this for initialization
	void Start () 
    {
        prevPos = new Vector3(0,0,0);
        currPos = rigidbody.position;
        prevDir = "right";
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (prevPos != new Vector3(0, 0, 0))
        {
            // Check if the boid moved to the right of the left.
            float moved = currPos.x - prevPos.x;
            if (moved > 0)
            {
                currDir = "right";
            }
            else
            {
                currDir = "left";
            }

            // Flip the image if needed.
            if (prevDir != currDir)
            {
                rigidbody.transform.Rotate(Vector3.right);
            }
        }
        prevPos = currPos;
	}
}
