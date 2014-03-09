using UnityEngine;
using System.Collections;

public class AttractedBird : MonoBehaviour {
	
	private AttractorController attractorController;

	void Start () {
		attractorController = (AttractorController)FindObjectOfType(typeof(AttractorController));
		if(!attractorController)
			throw new UnityException("There does not seem to be a Attractor Controller in the scene.");
	}

	void Update () {
		Debug.Log("Updating attracted bird.");
		Vector2 birdPosition = new Vector2(transform.position.x, transform.position.y);
		foreach(Vector2 attractingPoint in attractorController.AttractingPoints) {
			Vector2 distance = attractingPoint - birdPosition;
			if(Mathf.Abs(distance.magnitude) < Constants.AreaOfAttraction) {
				rigidbody.velocity += Constants.AttractionSpeed * new Vector3(distance.x, distance.y, 0);
				//Vector3.ClampMagnitude(rigidbody.velocity, Constants.maxVelocity);
			}
		}
	}
}
