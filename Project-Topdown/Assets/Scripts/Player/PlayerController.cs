using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float Health;
	public float MoveSpeed;
	public GunController Gun;
	public bool RenderDebug;

	private Rigidbody Rb;
	private Vector3 MoveInput;
	private Vector3 MoveVelocity;
	private Camera MainCamera;

	void Start () { // Use this for initialization
		Rb = GetComponent<Rigidbody>();
		MainCamera = FindObjectOfType<Camera>();
	}

	void Update () { // Update is called once per frame
		//Get Input
		MoveInput = new Vector3(Input.GetAxisRaw("Key_Horizontal"),0f,Input.GetAxisRaw("Key_Vertical"));
		MoveVelocity = MoveInput * MoveSpeed;

		Ray CameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
		Plane GroundPlane = new Plane(Vector3.up,Vector3.zero);
		float RayLength;

		if (GroundPlane.Raycast(CameraRay,out RayLength)) {

			//Vectors
			Vector3 PointToLook = CameraRay.GetPoint (RayLength);
			Vector3 PlayerPosition = transform.position;

			//Rotate Player
			transform.LookAt(new Vector3(PointToLook.x,transform.position.y,PointToLook.z));
			if (RenderDebug == true) {
				Debug.DrawLine (transform.position, PointToLook, Color.blue);
			}
		}
		if (Input.GetButtonDown("Mouse_LeftClick")) {
			Gun.IsFiring = true;

		}
		if (Input.GetButtonUp ("Mouse_LeftClick")) {
			Gun.IsFiring = false;
		}
		if (Health <= 0) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate () {
		Rb.velocity = MoveVelocity;
	}

	void OnTriggerEnter (Collider Bullet) {
		if (Bullet.gameObject.tag == "Bullet") {
			Health -= 1; //*2
			Destroy (Bullet.gameObject);
		}
	}
}