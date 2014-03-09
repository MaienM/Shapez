using UnityEngine;
using System.Collections;

public class ImageFlipping : MonoBehaviour {
    string prevDir;
    string currDir;
    Vector3 prevPos;
    Vector3 currPos;

	// Use this for initialization
	void Start () 
    {
        prevPos = rigidbody.position;
        prevDir = "right";
	}
	
	// Update is called once per frame
	void Update () 
    {
        currPos = rigidbody.position;
        if (prevPos != currPos)
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
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        prevPos = currPos;
        prevDir = currDir;
	}
}
