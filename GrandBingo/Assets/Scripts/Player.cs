using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
	public Boostrap boostrap;
	public Animator animator;
	public CameraManager cameraM;
	public bool isPlayer;

	public Chip handChip;
	public Chip eyeChip;
	public Chip legChip;
	public Chip stomachChip;
	public Chip lungsChip;
	public Chip kidneyChip;
	public Chip liverChip;
	public Chip headChip;

	public Transform revolver, hand;
	public RevolverAnim revolverAnim;
	public List<ChipType> available_chips = new List<ChipType>{ ChipType.hand, ChipType.eye, ChipType.leg, ChipType.stomach, ChipType.lungs, ChipType.kidney, ChipType.liver, ChipType.head};

	public int handCount = 2;
    public int eyeCount = 2;
    public int legCount = 4;

    public int stomach = 1, lungs = 1, kidney = 1, liver = 1;
	public bool isHoldingRevolver = false;

	private bool temperaryTurn = true;
	public System.Random random = new System.Random();


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
	public void Take()
	{
		animator.SetTrigger("Take");
		animator.SetTrigger("Open");
	}

	public void Bid()
	{

	}

	public void Fraud()
	{

	}

	public void Shoot()
	{
		bool isShoot = revolverAnim.revolver.Shoot();
		Debug.Log(isShoot);
        
    }

	public void RevolverHasTaken()
	{
		isHoldingRevolver = true;
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

	

	public void CameraFreeze()
	{
		cameraM.freeze = true;
	}

	public void CameraUnfreeze()
	{
		cameraM.freeze = false;
	}

	public void ShootOpponent()
	{
		animator.SetTrigger("ShootOpponet");
	}

	public void ShootYourSelf()
	{
		animator.SetTrigger("ShootYourSelf");
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

			default:
				break;
		}
	}

	public List<ChipType> BetChips(int count = 1)
	{
		List<ChipType> result = new();
		List<ChipType> temporaryChips = new(available_chips); // Копируем доступные фишки

		if (count < temporaryChips.Count)
		{
			temporaryChips.Remove(ChipType.head);
		}

		for (int i = 0; i < count; i++)
		{
			if (temporaryChips.Count == 0) break; // На случай если фишек не хватает

			int ind = random.Next(0, temporaryChips.Count);
			ChipType res = temporaryChips[ind];
			temporaryChips.RemoveAt(ind); // Лучше RemoveAt для производительности
			result.Add(res);
		}

		return result;
	}

	public void Betting(List<ChipType> chips)
	{
		animator.SetTrigger("Bet");

		StartCoroutine(ChipsBetAwait(0.6f, chips));
	}

	public void OpenDrums()
	{
		if (isPlayer) revolverAnim.OpenDrum();
		else
		{
			boostrap.Spinner();
		}
		
	}
	public void CloseDrums()
	{
		revolverAnim.CloseDrum();
        boostrap.ActivateShootingBottom();
    }

	public void SpinDrums()
	{
		revolverAnim.Spin();
	}

	public void Reload()
	{
		animator.SetTrigger("Close");
		revolverAnim.Initialize();
		revolverAnim.revolver.Randomize();
		revolverAnim.Clear();

		if (!isPlayer)
		{
			if (random.Next(0, 10) < 4) ShootYourSelf();
			else ShootOpponent();
		}
	}

	public void SetRandomBulletOrder()
	{
		int number = boostrap.remainBullet;
		while(number > 0)
		{
			int id = random.Next(0, 6);
			if (id == 0 && revolverAnim.bullet_1.activeSelf == false) revolverAnim.bullet_1.SetActive(true);
			else if (id == 1 && revolverAnim.bullet_2.activeSelf == false) revolverAnim.bullet_2.SetActive(true);
			else if (id == 2 && revolverAnim.bullet_2.activeSelf == false) revolverAnim.bullet_3.SetActive(true);
			else if (id == 3 && revolverAnim.bullet_2.activeSelf == false) revolverAnim.bullet_4.SetActive(true);
			else if (id == 4 && revolverAnim.bullet_2.activeSelf == false) revolverAnim.bullet_5.SetActive(true);
			else if (id == 5 && revolverAnim.bullet_2.activeSelf == false) revolverAnim.bullet_6.SetActive(true);
			else number++;
			number--;
        }
	}

	private IEnumerator ChipsBetAwait(float duration, List<ChipType> chips)
	{
		yield return new WaitForSeconds(duration);

		foreach (ChipType chip in chips)
		{
			switch (chip)
			{
				case ChipType.leg:
					legChip.Betting();
					break;

				case ChipType.hand:
					handChip.Betting();
					break;

				case ChipType.eye:
					eyeChip.Betting();
					break;

				case ChipType.kidney:
					kidneyChip.Betting();
					break;

				case ChipType.head:
					headChip.Betting();
					break;

				case ChipType.liver:
					liverChip.Betting();
					break;

				case ChipType.lungs:
					lungsChip.Betting();
					break;

				case ChipType.stomach:
					stomachChip.Betting();
					break;

			}
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
		yield return new WaitForSeconds(0.5f);
		boostrap.TakeRevolver();
	}

	private IEnumerator ConstrainRevolver()
	{ 
		isHoldingRevolver = true;

		while (isHoldingRevolver)
		{
			revolver.position = hand.position;
			revolver.rotation = hand.rotation;
			yield return null;
		}
		revolver.localPosition = new Vector3(0.174f, 0f, -0.187f);
		revolver.eulerAngles = Vector3.zero;
	}

	public void HoldRevolver()
	{
		StartCoroutine(ConstrainRevolver());
	}

	public void DestroyConstrain()
	{
		isHoldingRevolver = false;
	}
}

[SerializeField]
public enum ChipType
{
	hand,
	eye,
	leg,
	stomach,
	lungs,
	kidney,
	liver,
	head
}