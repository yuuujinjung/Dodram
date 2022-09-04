using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour {

	public float smooth = 1f;
	private Quaternion targetRotation;

	void Start () {
		targetRotation = transform.rotation;
	}
	
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) { 
			targetRotation *= Quaternion.AngleAxis (-90, Vector3.up);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			targetRotation *= Quaternion.AngleAxis (90, Vector3.up);
		}

		transform.rotation= Quaternion.Lerp (transform.rotation, targetRotation , 10 * smooth * Time.deltaTime);
	}
}
