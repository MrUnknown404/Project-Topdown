using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public bool IsFiring;
	public BulletController Bullet;
	public float BulletSpeed;
	public float TimeBetweenShots;
	public Transform FirePoint;
	public bool RenderDebug;
	public Transform DrawLine;

	private Camera MainCamera;
	private float ShotCounter;

	void Start () {
		//ID
		MainCamera = FindObjectOfType<Camera>();
	}

	void Update () {
		if (IsFiring == true) {
			ShotCounter -= Time.deltaTime;
			if (ShotCounter <= 0) {
				ShotCounter = TimeBetweenShots;
				BulletController NewBullet = Instantiate (Bullet, FirePoint.position,FirePoint.rotation);
				NewBullet.Speed = BulletSpeed;
			}
		} else {
			ShotCounter = 0;
		}
	}

	void FixedUpdate () {
		
		Plane GroundPlane = new Plane(Vector3.up,Vector3.zero);
		Ray CameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
		float RayLength;

		if (GroundPlane.Raycast (CameraRay, out RayLength)) {
			Vector3 PointToLook = CameraRay.GetPoint (RayLength);

			transform.LookAt(new Vector3(PointToLook.x,transform.position.y,PointToLook.z));

			if (RenderDebug == true) {
				Debug.DrawLine (DrawLine.position, PointToLook, Color.cyan);
			}
		}
	}
}
