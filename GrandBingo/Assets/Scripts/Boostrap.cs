using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boostrap : MonoBehaviour
{
    public Player player;
    public Player opponent;

	public int round;

    
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
}
