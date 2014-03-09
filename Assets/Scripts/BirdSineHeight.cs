using UnityEngine;
using System.Collections;

public class BirdSineHeight : MonoBehaviour {
    private float pos;

	// Use this for initialization
	void Start ()
    {
        pos = Random.Range(0, (int) (Mathf.PI * 2));
	}
	
	// Update is called once per frame
	void Update () 
    {
        pos += Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, 
                                              transform.localPosition.y,
                                              Mathf.Sin(pos) * 20);
	}
}
