using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttractorController : MonoBehaviour {

	public List<Vector2> AttractingPoints;

	void Start() {
		AttractingPoints = new List<Vector2>();
		AttractingPoints.Add(Vector2.zero);
	}

	void Update () {
		
	}
}