using UnityEngine;
using System.Collections;

public class BirdFlapping : MonoBehaviour {
    private float pos;

	// Use this for initialization
	void Start () 
    {
        pos = Random.Range(0, (int)(Mathf.PI * 2));
	}
	
	// Update is called once per frame
	void Update () 
    {
        pos += Time.deltaTime;
        transform.localScale += new Vector3(0, Mathf.Sin(pos) * -0.003f, 0);
	}
}
