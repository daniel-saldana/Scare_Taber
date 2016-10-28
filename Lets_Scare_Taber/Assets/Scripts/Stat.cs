using UnityEngine;
using System.Collections;
using System;



/*
 * This script is used with any on screen bar like "Health" or "Energy".
 * This isn't actually attached to any objects, it's just used by the BarScript.
 */




[Serializable]
public class Stat 
{
	[SerializeField]
	private BarScript bar;

	[SerializeField]
	private float maxVal;

	[SerializeField]
	private float currentVal;

	public float CurrentVal 
	{
		get 
		{
			return currentVal;
		}

		set 
		{
			this.currentVal = value;
			bar.Value = currentVal;
		}
	}

	public float MaxVal 
	{
		get 
		{
			return maxVal;
		}

		set 
		{
			this.maxVal = value;
			bar.MaxValue = maxVal;
		}

	}

	public void Initialize()
	{
		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}
}
