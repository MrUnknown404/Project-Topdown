using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	public Transform Target;
	public float SmoothSpeed = 10f;
	public Vector3 Offset;
	public bool RenderDebug;

	private Camera MainCamera;

	void Start () {
		//ID
		MainCamera = FindObjectOfType<Camera>();
	}

	void FixedUpdate () {

		//IDs
		Plane GroundPlane = new Plane(Vector3.up,Vector3.zero);
		Ray CameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
		float RayLength;

		//
		if (GroundPlane.Raycast (CameraRay, out RayLength)) {
			//Other Vector
			Vector3 PointToLook = CameraRay.GetPoint (RayLength);

			//Vectors
			Vector3 DesiredPosition = Target.position + Offset;
			Vector3 SmoothedPosition = Vector3.Lerp (transform.position,DesiredPosition,SmoothSpeed * Time.deltaTime);
			//Vectors 2    (Ez Copy&Paste ---> Input.mousePosition - PointToLook - Target.position)
			Vector3 SmoothedPosition2 = Vector3.Lerp (PointToLook + Offset,DesiredPosition,SmoothSpeed * Time.deltaTime);

			if (RenderDebug == true) {
				Debug.DrawLine (transform.position, PointToLook, Color.red);
			}
			//Move Camera
			if (Input.GetButton ("Mouse_RightClick")) {
				transform.position = SmoothedPosition2;
			} else {
				transform.position = SmoothedPosition;
			}
		}
	}
}