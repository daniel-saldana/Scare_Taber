using UnityEngine;
using System.Collections;

public class BatVibration : MonoBehaviour {

    public float verticalSpeed;
    public Vector3 tempPosition;
    public float amplitude;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        tempPosition = transform.position;

        tempPosition.y = transform.position.y +( Mathf.Cos(Time.time * verticalSpeed) * amplitude);
        
    }
}
