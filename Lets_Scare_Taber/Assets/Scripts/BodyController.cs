using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour 
{
	public float inputDelay = 0.1f;
	public float forwardVel = 12;
	public float defaultVel = 12;
	public float rotateVel = 100;
	public float dashVel = 25;
	//public float jumpForce = 25;

	public float gravity = -9.8f;
	public float dashlength = 1.0f;

	public bool dashReady = false;
	public bool dashing = false;

	public float dashTimer = 0.0f;

	Quaternion targetRotation;
	Rigidbody rb;
	float forwardInput, turnInput, strafeInput;

	public Stat health;
	public Stat batMana;
	public Stat dashMana;
	public Stat mistMana;

	public GameObject deathScreen = null;

	public Quaternion TargetRotation
	{
		get{ return targetRotation; }
	}

	// Use this for initialization
	void Start () 
	{
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody>())
			rb = GetComponent<Rigidbody>();
		else 
				Debug.LogError("Need Rigidbody");
			
		forwardInput = turnInput = strafeInput = 0;
	}

	public void Awake()
	{
		health.Initialize ();
		batMana.Initialize ();
		dashMana.Initialize ();
		mistMana.Initialize ();
		deathScreen.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{
		GetInput ();
		Turn ();
	}

	void FixedUpdate()
	{
		Run ();
		Strafe ();
		Dash ();
		Gravity ();
		//Jump ();
	}

	void GetInput()
	{
		forwardInput = Input.GetAxis("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
		strafeInput = Input.GetAxis ("Strafe");
	}

	void Run()
	{
		if (Mathf.Abs (forwardInput) > inputDelay) 
		{
			rb.velocity = transform.forward * forwardInput * forwardVel;
		} 
		else
		rb.velocity = Vector3.zero;
	}

	void Turn()
	{
		if (Mathf.Abs (turnInput) > inputDelay) 
		{
			targetRotation *= Quaternion.AngleAxis (rotateVel * turnInput * Time.deltaTime, Vector3.up);
		}
		transform.rotation = targetRotation;
	}
		
	void Strafe()
	{
		if (Mathf.Abs (strafeInput) > inputDelay) {
			rb.velocity = transform.right * strafeInput * forwardVel;
		} 
	}

	void Dash()
	{
		if (dashMana.CurrentVal > 0.5f) 
		{
			dashReady = true;
		}
		if (dashReady == true) 
		{
			if (Input.GetKeyDown (KeyCode.LeftShift) && dashReady == true) 
			{
				dashing = true;
			}
		}
		if (dashing == true) 
		{
			dashTimer += Time.deltaTime;
			dashMana.CurrentVal -= Time.deltaTime;
			forwardVel = dashVel;
		}

		if (dashTimer > dashlength) 
		{
			dashTimer = 0.0f;
			forwardVel = defaultVel;
			dashReady = false;
			dashing = false;
		}
		if (dashMana.CurrentVal <= 0.1f) 
		{
			dashReady = false;
		}
	}

	/*void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//rb.velocity = Vector3.zero;
			rb.AddForce (0, jumpForce, 0);
		}
	}*/

	void Gravity()
	{
		rb.AddForce (0, gravity, 0, ForceMode.Impulse);
	}
}
