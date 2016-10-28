using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/*
 * This script is used with any on screen bar like "Health" or "Energy".
 */


public class BarScript : MonoBehaviour 
{
	[SerializeField]
	private float fillAmount;

	public Image content;

	public float MaxValue { get; set; }

	public float Value 
	{
		set 
		{
			fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}

	// Use this for initializationa
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleBar ();
	}

	public void HandleBar()
	{
		if (fillAmount != content.fillAmount) 
		{
			content.fillAmount = fillAmount;
		}
	}

	private float Map (float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
