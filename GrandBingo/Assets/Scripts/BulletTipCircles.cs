using UnityEngine;
using System;

public class BulletTipCircles : MonoBehaviour
{
	public Boostrap boostrap;
	public RevolverAnim revolver;
	public int ind;

	private GameObject circle;
	public bool isChoosen = false;
	

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
			boostrap.remainBullet++;
			isChoosen = false;
		}
		else
		{
			if (boostrap.remainBullet <= 0)
			{
				return;
			}

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

			boostrap.remainBullet--;

			isChoosen = true;
		}

	}
}
