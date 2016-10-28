using UnityEngine;
using System.Collections;

public class BodyControllerScript : MonoBehaviour 
{
	public float inputDelay = 0.1f;
	public float forwardVel = 12;
	public float defaultVel = 12;
	public float dashVel = 25;
	public float rotateVel = 100;
	public float gravity = -1f;

	Quaternion targetRotation;
	Rigidbody rb;
	float forwardInput, turnInput;

	public Stat health;

	public Stat dash;

	LevelSwitch ls;
	SonarFx sfx;

	public bool dashReady = false;

	public GameObject deathScreen = null;

	public Quaternion TargetRotation
	{
		get{ return targetRotation; }
	}

	// Use this for initialization
	void Start () 
	{
		sfx = FindObjectOfType<SonarFx> ();
		ls = FindObjectOfType<LevelSwitch> ();
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody>())
			rb = GetComponent<Rigidbody>();
		else 
				Debug.LogError("Need Rigidbody");
			
		forwardInput = turnInput = 0;

		forwardVel = defaultVel;
		dashVel = dashVel * Time.deltaTime;
		defaultVel = defaultVel * Time.deltaTime;
	}

	public void Awake()
	{
		health.Initialize ();
		dash.Initialize ();
		//deathScreen.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{
		GetInput ();
		Turn ();
		Dash ();
	}

	void FixedUpdate()
	{
		Run ();
		//Strafe ();
		Gravity ();
	}

	void GetInput()
	{
		forwardInput = Input.GetAxis("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
		//strafeInput = Input.GetAxis ("Strafe");
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

	void Dash()
	{
		if (dash.CurrentVal > 0.5f) 
		{
			dashReady = true;
		}
		if (dash.CurrentVal >= dash.MaxVal) 
		{
			dash.CurrentVal = dash.MaxVal;
		}
		if (dash.CurrentVal <= 0.0f) 
		{
			dash.CurrentVal = 0.0f;
		}
			if (dashReady == true) 
		{
			if (Input.GetKey (KeyCode.LeftShift)) 
			{
				forwardVel = dashVel;
				dash.CurrentVal -= Time.deltaTime;
			} 
			else 
			{
				forwardVel = defaultVel;
				dash.CurrentVal += Time.deltaTime * 0.25f;
			}
		}
	}


	/*void Strafe()
	{
		if (Mathf.Abs (strafeInput) > inputDelay) 
		{
			rb.velocity = transform.right * strafeInput * forwardVel;
		}
	}
*/
	void Gravity()
	{
		rb.AddForce (0, gravity, 0, ForceMode.Impulse);
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.name.StartsWith ("Floor"))
			{
				ls.changeToScene (0);
				//Destroy(gameObject);
			}
	}

	void Falling()
	{
		if (rb.velocity (Vector3.down) != null) 
		{
			
		}
	}
}
