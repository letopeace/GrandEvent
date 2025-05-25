using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boostrap : MonoBehaviour
{
    public Player player;
    public Player opponent;

	public int round = 1;
	public int miniRound = 1;
	public int remainBullet { get { return _remainBullet; } set { _remainBullet = value; UpdateSpinButton(); } }
	private int _remainBullet;
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

	public void NextTurn()
	{
		turn ^= true;
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

}
