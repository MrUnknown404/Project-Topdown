using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public bool IsFiring;
	public bool Move;
    public bool RenderDebug;
    public float Health;
	public float Speed;
    public Transform Target;
	public EnemyGunController Gun;

    private Camera MainCamera;

    void Start() {
        MainCamera = FindObjectOfType<Camera>();
    }

    void Update () {
		if (Health <= 0) {
			Destroy (gameObject);
		}
		if (IsFiring == true) {
			Gun.IsFiring = true;
		}
	}

    void FixedUpdate() {
        transform.LookAt(new Vector3(Target.position.x, transform.position.y, Target.position.z));
		if (Move == true) {
			transform.Translate (Vector3.forward * Speed * Time.deltaTime);
		}
        if (RenderDebug == true) {
            Debug.DrawLine(transform.position, Target.position, Color.red);
        }
    }
	void OnTriggerEnter (Collider Bullet) {
		if (Bullet.gameObject.tag == "Bullet") {
			Health -= 1; //*2
			Destroy (Bullet.gameObject);
		}
	}
}

