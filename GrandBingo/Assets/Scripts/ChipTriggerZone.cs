using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipTriggerZone : MonoBehaviour
{
	public GameObject buutonNext;
	public Boostrap boostrap;

    private MeshRenderer meshRenderer;
	private int currentCount = 0;
	

	private void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		Hide();
		HideButton();
	}

	public void Hide()
    {
		meshRenderer.enabled = false;
    }

	public void Show()
	{
		if (currentCount >= boostrap.round) { return; }
		meshRenderer.enabled = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Chip")
		{
			currentCount++;
			
			if (currentCount == boostrap.round)
			{
				ShowButton();
			}
			else
			{
				HideButton();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Chip")
		{
			currentCount--;

			if (currentCount == boostrap.round)
			{
				ShowButton();
			}
			else if (currentCount < boostrap.round) 
			{
				Show();
			}
			else
			{
				HideButton();
			}
		}
	}

	private void ShowButton()
	{
		buutonNext?.SetActive(true);
	}
	
	public void HideButton()
	{
		buutonNext?.SetActive(false);
	}
}
