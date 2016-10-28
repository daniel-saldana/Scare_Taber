using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform target;

	public  float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0,6,-8);
	public float xTilt = 10;

	Vector3 destination = Vector3.zero;
	BodyController bodCon;
	float rotateVel = 0;

	// Use this for initialization
	void Start () 
	{
		SetCameraTarget (target);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void LateUpdate()
	{
		MoveToTarget ();
		LookAtTarget ();
	}

	void SetCameraTarget(Transform t)
	{
		target = t;
		if (target != null) 
		{
			if (target.GetComponent<BodyController> ()) 
			{
				bodCon = target.GetComponent<BodyController> ();
			} 
			else
				Debug.LogError ("Camera's Target needs a BodyController");
		} 
		else
			Debug.LogError ("Need Camera Target");
	}

	void MoveToTarget()
	{
		destination = bodCon.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget()
	{
		float eulerYAngle = Mathf.SmoothDampAngle (transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler (transform.eulerAngles.x, eulerYAngle, 0);
	}

}
