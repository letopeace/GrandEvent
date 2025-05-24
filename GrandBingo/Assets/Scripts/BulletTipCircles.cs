using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTipCircles : MonoBehaviour
{
	public Boostrap boostrap;
	public int ind;

	private GameObject circle;
	

	private void Start()
	{
		circle = transform.GetChild(0).gameObject;
	}

	private void OnMouseEnter()
	{
		circle.SetActive(true);
	}

	private void OnMouseExit()
	{
		circle.SetActive(false);
	}
}
