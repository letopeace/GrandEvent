using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Animator animator;

	public Chip handChip;
	public Chip eyeChip;
	public Chip legChip;
	public Chip stomachChip;
	public Chip lungsChip;
	public Chip kidneyChip;
	public Chip liverChip;


	public int handCount = 2;
    public int eyeCount = 2;
    public int legCount = 4;

    public int stomach = 1, lungs = 1, kidney = 1, liver = 1;


    public void Turn()
    {

    }

	public void Bid()
	{

	}

	public void Fraud()
	{

	}

	public void Shoot()
	{

	}




	public void TakeDamage(string chip)
    {
        switch (chip)
        {
            case "hand":
				HandDestroy();
				break;

			case "eye":
				EyeDestroy();
				break;

			case "leg":
				LegDestroy();
				break;

			case "stomach":
				StomachDestroy();
				break;

			case "lungs":
				LungsDestroy();
				break;

			case "kidney":
				KidneyDestroy();
				break;

			case "liver":
				LiverDestroy();
				break;

		}
	}

	private void HandUse()
	{
		handCount -= 1;

		if (handCount == 0)
		{
			HandDestroy();
		}
	}

	private void EyeUse()
	{
		eyeCount -= 1;

		if (eyeCount == 0)
		{
			EyeDestroy();
		}
	}

	private void LegUse()
	{
		legCount -= 1;

		if (legCount == 0)
		{
			LegDestroy();
		}
	}


	private void HandDestroy()
	{
		handCount = 0;
	}

	private void EyeDestroy()
	{
		eyeCount = 0;
	}

	private void LegDestroy()
	{
		legCount = 0;
	}

	private void StomachDestroy()
	{
		stomach = 0;
	}

	private void LungsDestroy()
	{
		lungs = 0;
	}

	private void KidneyDestroy()
	{
		kidney = 0;
	}

	private void LiverDestroy()
	{
		liver = 0;
	}
}
