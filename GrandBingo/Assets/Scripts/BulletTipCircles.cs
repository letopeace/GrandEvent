using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTipCircles : MonoBehaviour
{
	public RevolverAnim revolver;
	public int ind;

	private GameObject circle;
	private bool isChoosen = false;
	

	private void Start()
	{
		circle = transform.GetChild(0).gameObject;
	}

	private void OnMouseEnter()
	{
		circle.SetActive(true);
	}

	private void OnMouseExit()
	{
		circle.SetActive(false);
	}

	private void OnMouseDown()
	{
		if (isChoosen)
		{
			switch (ind)
			{
				case 1:
					revolver.Bullet_1_Inverse(); break;

				case 2:
					revolver.Bullet_2_Inverse(); break;

				case 3:
					revolver.Bullet_3_Inverse(); break;

				case 4:
					revolver.Bullet_4_Inverse(); break;

				case 5:
					revolver.Bullet_5_Inverse(); break;

				case 6:
					revolver.Bullet_6_Inverse(); break;

			}

			isChoosen = false;
		}
		else
		{
			switch (ind)
			{
				case 1:
					revolver.Bullet_1(); break;

				case 2:
					revolver.Bullet_2(); break;

				case 3:
					revolver.Bullet_3(); break;

				case 4:
					revolver.Bullet_4(); break;

				case 5:
					revolver.Bullet_5(); break;

				case 6:
					revolver.Bullet_6(); break;

			}

			isChoosen = true;
		}

	}
}
