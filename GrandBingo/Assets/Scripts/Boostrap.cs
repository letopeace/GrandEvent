using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class Boostrap : MonoBehaviour
{
    public Player player;
    public Player opponent;

	public int round = 1;
	public int miniRound = 1;
	public int remainBullet { get { return _remainBullet; } set { _remainBullet = value; UpdateSpinButton(); } }
	[SerializeField] private int _remainBullet;
	public int currentBulletNumber;
	public RevolverAnim revolverAnim;
	private bool spinButtonBlocker = true;


	public bool turn = true;
    public bool bid = false;
    public bool fraud = false;
    public bool shoot = false;

	[SerializeField] private GameObject bottomYourSelf;
	[SerializeField] private GameObject bottomOpponent;
	[SerializeField] private GameObject spinButton;

	private void Start()
	{
		bid = true;
		bottomYourSelf?.SetActive(false);
		bottomOpponent?.SetActive(false);

		remainBullet = round;
		currentBulletNumber = round;
	}


	private void Update()
    {


        if (bid)
        {
			if (turn)
			{
				player.Bid();
			}
			else
			{
				opponent.Bid();
			}
		}
		else if (fraud)
		{
			if (turn)
			{
				player.Fraud();
			}
			else
			{
				opponent.Fraud();
			}
		}
		else if (shoot)
		{
			if (turn)
			{
				player.Turn();
			}
			else
			{
				opponent.Turn();
			}
		}

		player.animator.SetBool("turn", turn);
		opponent.animator.SetBool("turn", !turn);

	}

	public void UpdateSpinButton()
	{
		if (spinButtonBlocker)
			return;

		if (_remainBullet == 0)
		{
			spinButton?.SetActive(true);
		}
		else
		{
			spinButton?.SetActive(false);	
		}

	}

	public async Task NextTurn()
	{
		Debug.Log("NextTurn");
		turn = !turn;
		miniRound++;

		await Task.Delay(3000);

		TakeRevolver();
	}

	public void TakeRevolver()
	{
		if (turn)
		{
			player.Take();
			spinButtonBlocker = false;
		}
		else opponent.Take();
	}

	public void Betted()
	{
		List<ChipType> opponentBets = opponent.BetChips(round > 6 ? 6 : round);

		opponent.Betting(opponentBets);
		bool whoseTurn = opponent.random.Next(0,2) == 0 ? false : true;
		turn = whoseTurn;
		opponent.SpinAnimate(whoseTurn);

		bid = false;

		fraud = true;
	}

	public void Spinner()
	{
		fraud = false;
		shoot = true;

		if (turn)
		{
			player.Reload();
			spinButtonBlocker = false;
		}
		else
		{
			opponent.SetRandomBulletOrder();
			opponent.Reload();
		}
	}

	public void ActivateShootingBottom()
	{
		bottomOpponent?.SetActive(true);
		bottomYourSelf?.SetActive(true);
	}

	public void NextRound()
	{
		miniRound = 1;
		round++;
		revolverAnim.Clear();

		remainBullet = round;
		currentBulletNumber = round;
		
		turn = true;
		bid = true;
		fraud = false;
		shoot = false;

		Debug.Log("NextRound");

		for(int i = 0;i<opponent.betted_chips.Count;i++)
		{
			if (opponent.betted_chips[i] == ChipType.hand) opponent.handChip.Return();
			else if (opponent.betted_chips[i] == ChipType.eye) opponent.eyeChip.Return();
			else if (opponent.betted_chips[i] == ChipType.leg) opponent.legChip.Return();
			else if (opponent.betted_chips[i] == ChipType.stomach) opponent.stomachChip.Return();
			else if (opponent.betted_chips[i] == ChipType.lungs) opponent.lungsChip.Return();
			else if (opponent.betted_chips[i] == ChipType.kidney) opponent.kidneyChip.Return();
			else if (opponent.betted_chips[i] == ChipType.liver) opponent.liverChip.Return();
			else opponent.headChip.Return();
		}
        for (int i = 0; i < player.betted_chips.Count; i++)
        {
            if (player.betted_chips[i] == ChipType.hand) player.handChip.Return();
            else if (player.betted_chips[i] == ChipType.eye) player.eyeChip.Return();
            else if (player.betted_chips[i] == ChipType.leg) player.legChip.Return();
            else if (player.betted_chips[i] == ChipType.stomach) player.stomachChip.Return();
            else if (player.betted_chips[i] == ChipType.lungs) player.lungsChip.Return();
            else if (player.betted_chips[i] == ChipType.kidney) player.kidneyChip.Return();
            else if (player.betted_chips[i] == ChipType.liver) player.liverChip.Return();
            else player.headChip.Return();
        }
    }

}
