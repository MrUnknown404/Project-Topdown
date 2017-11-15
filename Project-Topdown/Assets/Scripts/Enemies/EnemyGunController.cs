using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour {

    public bool RenderDebug;
    public bool IsFiring;
	public BulletController Bullet;
	public float BulletSpeed;
	public float TimeBetweenShots;
    public float BulletDamage;
    public Transform FirePoint;
	public Transform DrawLine;
	public Transform Target;

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
                NewBullet.Damage = BulletDamage;
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
			transform.LookAt(new Vector3(Target.position.x,transform.position.y,Target.position.z));
			//Debug
			if (RenderDebug == true) {
				Debug.DrawLine (DrawLine.position, Target.position, Color.green);
			}
		}
	}
}
