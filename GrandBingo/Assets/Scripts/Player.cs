using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Boostrap boostrap;
	public Animator animator;

	public Chip handChip;
	public Chip eyeChip;
	public Chip legChip;
	public Chip stomachChip;
	public Chip lungsChip;
	public Chip kidneyChip;
	public Chip liverChip;

	public Transform revolver;


	public int handCount = 2;
    public int eyeCount = 2;
    public int legCount = 4;

    public int stomach = 1, lungs = 1, kidney = 1, liver = 1;

	private bool temperaryTurn = true;

	private void Update()
	{
		/*
		if (Input.GetMouseButtonDown(0))
		{
			SpinAnimate(true);
		}
		if (Input.GetMouseButtonDown(1))
		{
			SpinAnimate(false);
		}*/
	}

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

	public void SpinAnimate(bool turn)
	{
		animator.SetTrigger("Spin");
		temperaryTurn = turn;
	}

	public void SpinAnimate2()
	{
		StartCoroutine(SpinRevolver(temperaryTurn));
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


	private IEnumerator SpinRevolver(bool turn)
	{
		float duration = 2.5f; // сколько секунд длится вращение
		float totalRotations = -5f; // сколько оборотов (1 оборот = 360 градусов)
		float startAngle = revolver.eulerAngles.y;

		float targetOffset = turn ? -1350 : -1170f;
		float endAngle = startAngle + /*totalRotations * 360f +*/ targetOffset;

		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;

			// ease-out: быстро сначала, медленно к концу
			float smoothT = 1 - Mathf.Pow(1 - t, 3);

			float currentAngle = Mathf.Lerp(startAngle, endAngle, smoothT);
			revolver.eulerAngles = new Vector3(0, currentAngle, 0);

			yield return null;
		}

		revolver.eulerAngles = new Vector3(0, endAngle, 0); // точная установка финального угла
	}
}
