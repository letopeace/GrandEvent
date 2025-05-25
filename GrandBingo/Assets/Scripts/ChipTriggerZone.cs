using UnityEngine;
using System;
using System.Collections.Generic;

public class ChipTriggerZone : MonoBehaviour
{
	public GameObject buutonNext;
	public Boostrap boostrap;

    private MeshRenderer meshRenderer;
	public List<Chip> currentCount;
	

	private void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		currentCount = new List<Chip>();
		Hide();
		HideButton();
	}

	public void Hide()
    {
		meshRenderer.enabled = false;
    }

	public void Show()
	{
		if (currentCount.Count >= boostrap.round) { return; }
		meshRenderer.enabled = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Chip")
		{
			currentCount.Add(other.GetComponent<Chip>());

			Chip chip = other.GetComponent<Chip>();
			if (chip.players)
				boostrap.player.BetChip(chip.chipType);
			else
				boostrap.opponent.BetChip(chip.chipType);

			if (currentCount.Count == boostrap.round)
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
			currentCount.Remove(other.GetComponent<Chip>());

			Chip chip = other.GetComponent<Chip>();
			if (chip.players)
				boostrap.player.BetChipCancel(chip.chipType);
			else
				boostrap.opponent.BetChipCancel(chip.chipType);
            

			if (currentCount.Count == boostrap.round)
			{
				ShowButton();
			}
			else if (currentCount.Count < boostrap.round) 
			{
				Show();
				HideButton();
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
