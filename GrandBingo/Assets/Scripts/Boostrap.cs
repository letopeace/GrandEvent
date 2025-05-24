using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boostrap : MonoBehaviour
{
    public Player player;
    public Player opponent;

	public int round = 1;

    
    public bool turn = true;
    public bool bid = false;
    public bool fraud = false;
    public bool shoot = false;

	private void Start()
	{
		bid = true;
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

}
