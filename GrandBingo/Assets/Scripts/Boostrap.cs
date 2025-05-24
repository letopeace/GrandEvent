using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boostrap : MonoBehaviour
{
    public Player player;
    public Player opponent;

	public int round = 1;
	public int remainBullet;

    
    public bool turn = true;
    public bool bid = false;
    public bool fraud = false;
    public bool shoot = false;

	[SerializeField] private GameObject bottomYourSelf;
	[SerializeField] private GameObject bottomOpponent;

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

	public void TakeRevolver()
	{
		if (turn)
		{
			player.Take();
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

	public void ActivateShootingBottom()
	{
		bottomOpponent?.SetActive(true);
		bottomYourSelf?.SetActive(true);
	}

}
