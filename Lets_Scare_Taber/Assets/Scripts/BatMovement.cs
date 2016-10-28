using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{
	public float inputDelay = 0.1f;

    public float forwardSpeed;
	public float backSpeed;
    public float verticalSpeed;

	public float ascendInput;

	public float startingAltitude;
	public float currentAltitude;
	public float maxAltitude;
	public float minAltitude;

	//public Vector3 pos;

    public float speed;


	// Added speed defaults
	void Start ()
    {
		ascendInput = 0;
		speed = speed * Time.deltaTime;
		forwardSpeed = forwardSpeed * Time.deltaTime;
		backSpeed = backSpeed * Time.deltaTime;
		verticalSpeed = verticalSpeed * Time.deltaTime;
	}

	//Starts the Bat at the default starting altitude
	void OnEnable()
	{
		if(transform.position.y != startingAltitude)
		{
			transform.position = new Vector3(transform.position.x, startingAltitude, transform.position.z);        
		}
	}

	//Gets Input controls for flying
	void Update()
	{
		Ascend ();
		GetInput ();
	}

	//Changed controls to allow Bat to fly Forward and Backward. Bat flys backward at a slower rate.
    void FixedUpdate()
    {
		Ascend ();
		//pos = transform.position;
        //pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position += transform.forward * speed;

        if (Input.GetKey(KeyCode.W))
        {
			transform.Translate(Vector3.forward * forwardSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
			transform.Translate(Vector3.back * backSpeed);
        }
    }

	//Controls ascending and descending on the global Y axis. KeyCode."D" for up and KeyCode."A" for down.
	void GetInput()
	{
		ascendInput = Input.GetAxis ("Fly");
	}

	void Ascend()
	{
		if (Mathf.Abs (ascendInput) > inputDelay) 
		{
			transform.Translate(new Vector3(0, verticalSpeed * ascendInput, 0));
		} 

		//Limits the altitude to within a set range

		if (transform.position.y >= maxAltitude) 
		{
			transform.position = new Vector3(transform.position.x, maxAltitude, transform.position.z);   
		}

		if (transform.position.y <= minAltitude) 
		{
			transform.position = new Vector3(transform.position.x, minAltitude, transform.position.z);   		
		}
	}
}
