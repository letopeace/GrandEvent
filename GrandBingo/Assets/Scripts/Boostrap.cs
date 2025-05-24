using System.Collections;
using System.Collections.Generic;
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
		if (Input.GetMouseButtonDown(0))
		{
			Betted();
		}


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

	public void Betted()
	{
		List<ChipType> opponentBets = opponent.BetChips(round > 6 ? 6 : round);

		opponent.Betting(opponentBets);
	}

}
