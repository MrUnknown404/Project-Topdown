using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float Speed;
	public float LifeTime;

	private float LifeTimeCounter;

	void Start () {
		LifeTimeCounter = LifeTime;
	}

	void Update () {
		transform.Translate (Vector3.forward * Speed * Time.deltaTime);

		//Kill
		LifeTimeCounter -= Time.deltaTime;
		if (LifeTimeCounter <= 0) {
			Destroy (gameObject);
		}

	}
	void OnTriggerEnter (Collider Wall) {
		if (Wall.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}

	}
}