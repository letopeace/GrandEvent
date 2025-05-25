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
	public List<ChipType> betted_chips = new List<ChipType>();
	public Animation damageAnim;

	public int handCount = 2;
    public int eyeCount = 2;
    public int legCount = 4;

    public int stomach = 1, lungs = 1, kidney = 1, liver = 1, head = 1;
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

		if (boostrap.miniRound == 1)
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
		if (isPlayer)
			cameraM.freeze = true;
	}

	public void CameraUnfreeze()
	{
		if (isPlayer)
			cameraM.freeze = false;
	}


	public void ShootOpponentCheck()
	{
		bool isShoot = revolverAnim.revolver.Shoot();
		Debug.Log(isShoot);

		boostrap.NextTurn();

		if (isShoot)
		{
			revolverAnim.Shoot();


			if (isPlayer)
			{
				boostrap.opponent.DestroyRandomChip();
			}
			else
			{
				boostrap.player.DestroyRandomChip();
				damageAnim.Play();
			}
		}

	}

	public void ShootYourSelfCheck()
	{
		bool isShoot = revolverAnim.revolver.Shoot();
		Debug.Log(isShoot);

		if (isShoot)
		{
			revolverAnim.Shoot();
            boostrap.NextTurn();


            if (isPlayer)
			{
				boostrap.player.DestroyRandomChip();
				damageAnim.Play();
			}
			else
			{
				boostrap.opponent.DestroyRandomChip();
			}
		}
	}


	public void ActivateShootingBottom()
	{
		if(boostrap.shoot && isPlayer)
			boostrap.ActivateShootingBottom();
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
		List<ChipType> temporaryChips = new(available_chips); //  опируем доступные фишки

		if (count < temporaryChips.Count)
		{
			temporaryChips.Remove(ChipType.head);
		}

		for (int i = 0; i < count; i++)
		{
			if (temporaryChips.Count == 0) break; // Ќа случай если фишек не хватает

			int ind = random.Next(0, temporaryChips.Count);
			ChipType res = temporaryChips[ind];
			temporaryChips.RemoveAt(ind); // Ћучше RemoveAt дл€ производительности
			result.Add(res);
		}

		betted_chips = new(result);

		return result;
	}

	public void Betting(List<ChipType> chips)
	{
		animator.SetTrigger("Bet");

		StartCoroutine(ChipsBetAwait(0.6f, chips));
	}

	public void OpenDrums()
	{
		revolverAnim.OpenDrum();
		if (!isPlayer)
		{
			animator.SetTrigger("Close");

			boostrap.Spinner();
		}
		
	}
	public void CloseDrums()
	{
		revolverAnim.CloseDrum();
    }

	public void SpinDrums()
	{
		revolverAnim.Spin();
	}

	public void Reload()
	{

		revolverAnim.Initialize();
		revolverAnim.revolver.Randomize();
		revolverAnim.Clear();

		if (isPlayer)
		{
			animator.SetTrigger("Close");
		}

		else
		{
			//animator.SetTrigger("Close");

			if (random.Next(0, 10) < 4) ShootYourSelf();
			else ShootOpponent();
		}
	}


	public void SetRandomBulletOrder()
	{
		int number = boostrap.round;
		Revolver newRevolver = new Revolver(number);

		bool[] bullets = newRevolver.Show();

		revolverAnim.bullet_1.SetActive(bullets[0]);
		revolverAnim.bullet_2.SetActive(bullets[1]);
		revolverAnim.bullet_3.SetActive(bullets[2]);
		revolverAnim.bullet_4.SetActive(bullets[3]);
		revolverAnim.bullet_5.SetActive(bullets[4]);
		revolverAnim.bullet_6.SetActive(bullets[5]);
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


	public void BetChip(ChipType chip)
	{
		betted_chips.Add(chip);
	}
	public void BetChipCancel(ChipType chip)
	{
		betted_chips.Remove(chip);
	}

	public void DestroyRandomChip()
	{
		List<ChipType> chips = new(betted_chips);
		ChipType willDestroyChip;

		bool contains = chips.Contains(ChipType.head);

		if (contains)
		{
			if (chips.Count > 1)
			{
				chips.Remove(ChipType.head);
				willDestroyChip = chips[random.Next(chips.Count)];
			}
			else
				willDestroyChip = ChipType.head;
		}
		else
		{
			willDestroyChip = chips[random.Next(chips.Count)];
		}

		ChipDestroy(willDestroyChip);
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


	private void ChipDestroy(ChipType type)
	{

		animator.SetTrigger("Damaged");

		switch (type)
		{
			case ChipType.hand: HandDestroy(); break;

			case ChipType.leg: LegDestroy(); break;

			case ChipType.eye: EyeDestroy(); break;

			case ChipType.kidney: KidneyDestroy(); break;

			case ChipType.lungs: LungsDestroy(); break;

			case ChipType.liver: LiverDestroy(); break;

			case ChipType.stomach: StomachDestroy(); break;

			case ChipType.head: HeadDestroy(); break;

		}
	}

	private void HandDestroy()
	{
		handCount = 0;
		handChip.ChipDestroy(this);
	}

	private void EyeDestroy()
	{
		eyeCount = 0;
		eyeChip.ChipDestroy(this);
	}

	private void LegDestroy()
	{
		legCount = 0;
		legChip.ChipDestroy(this);
	}

	private void StomachDestroy()
	{
		stomach = 0;
		stomachChip.ChipDestroy(this);
	}

	private void LungsDestroy()
	{
		lungs = 0;
		lungsChip.ChipDestroy(this);
	}

	private void KidneyDestroy()
	{
		kidney = 0;
		kidneyChip.ChipDestroy(this);
	}

	private void LiverDestroy()
	{
		liver = 0;
		liverChip.ChipDestroy(this);
	}

	private void HeadDestroy()
	{
		head = 0;
		headChip.ChipDestroy(this);
	}

	private IEnumerator SpinRevolver(bool turn)
	{
		Transform _revolver = revolver.parent;

		float duration = 2.5f; // сколько секунд длитс€ вращение
		float startAngle = _revolver.eulerAngles.y;

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
			_revolver.eulerAngles = new Vector3(0, currentAngle, 0);

			yield return null;
		}

		_revolver.eulerAngles = new Vector3(0, endAngle, 0); // точна€ установка финального угла
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
		revolver.eulerAngles = Vector3.zero;
		revolver.position = new Vector3(-2.82299995f, 1.59899998f, -0.0820000097f);
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