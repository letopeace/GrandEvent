using System.Collections;
using UnityEngine;
using System;

public class RevolverAnim : MonoBehaviour
{
    public Animator animator;
    public Transform parentOffset;
	public ParticleSystem shootingEffect;
	public GameObject canvas;
	public Revolver revolver = null;

	public GameObject bullet_1;
	public GameObject bullet_2;
	public GameObject bullet_3;
	public GameObject bullet_4;
	public GameObject bullet_5;
	public GameObject bullet_6;

	public GameObject circle_1;
	public GameObject circle_2;
	public GameObject circle_3;
	public GameObject circle_4;
	public GameObject circle_5;
	public GameObject circle_6;

	public AudioSource BulletAudio;

	private void Start()
	{
		animator = GetComponent<Animator>();
		parentOffset = transform.parent;
		Clear();
	}

	public void Initialize()
	{
		bool[] newDrum = new bool[6];
		newDrum[0] = bullet_1.activeSelf;
		newDrum[1] = bullet_2.activeSelf;
		newDrum[2] = bullet_3.activeSelf;
		newDrum[3] = bullet_4.activeSelf;
		newDrum[4] = bullet_5.activeSelf;
		newDrum[5] = bullet_6.activeSelf;

		revolver = new Revolver(newDrum);
	}

	public void RotationReset()
	{
		parentOffset.eulerAngles = Vector3.zero;
	}

	public void OpenDrum()
	{
		animator.SetTrigger("Open");
		ShowBullets();
	}

	public void CloseDrum()
	{
		animator.SetTrigger("Close");
		Clear();
	}

	public void Spin()
	{
		animator.SetTrigger("Spin");
	}

	public void Shoot()
	{
		animator.SetTrigger("Shoot");
	}

	public void ShootingEffect()
	{
		shootingEffect.Play();
	}

	public void Clear()
	{
		bullet_1.SetActive(false);
		bullet_2.SetActive(false);
		bullet_3.SetActive(false);
		bullet_4.SetActive(false);
		bullet_5.SetActive(false);
		bullet_6.SetActive(false);

		circle_1.SetActive(false);
		circle_2.SetActive(false);
		circle_3.SetActive(false);
		circle_4.SetActive(false);
		circle_5.SetActive(false);
		circle_6.SetActive(false);
	}

	public void ShowBullets()
	{
		if (revolver == null) return;

		bool[] bullets = revolver.Show();

		bullet_1.SetActive(bullets[0]);
		bullet_2.SetActive(bullets[1]);
		bullet_3.SetActive(bullets[2]);
		bullet_4.SetActive(bullets[3]);
		bullet_5.SetActive(bullets[4]);
		bullet_6.SetActive(bullets[5]);
	}

	

	public void ShowTip()
	{
		canvas?.SetActive(true);
	}

	public void HideTip()
	{
		canvas?.SetActive(false);
	}

	public void Bullet_1()
	{
		bullet_1.SetActive(true);
		animator.SetTrigger("1");
		BulletAudio.Play();
	}

	public void Bullet_2()
	{
		bullet_2.SetActive(true);
		animator.SetTrigger("2");
        BulletAudio.Play();
    }

	public void Bullet_3()
	{
		bullet_3.SetActive(true);
		animator.SetTrigger("3");
        BulletAudio.Play();
    }

	public void Bullet_4()
	{
		bullet_4.SetActive(true);
		animator.SetTrigger("4");
        BulletAudio.Play();
    }

	public void Bullet_5()
	{
		bullet_5.SetActive(true);
		animator.SetTrigger("5");
        BulletAudio.Play();
    }

	public void Bullet_6()
	{
		bullet_6.SetActive(true);
		animator.SetTrigger("6");
        BulletAudio.Play();
    }


	public void Bullet_1_Inverse()
	{
		animator.SetTrigger("1_");
		StartCoroutine(SetFalse(bullet_1));
        BulletAudio.Play();
    }

	public void Bullet_2_Inverse()
	{
		StartCoroutine(SetFalse(bullet_2));
		animator.SetTrigger("2_");
        BulletAudio.Play();
    }

	public void Bullet_3_Inverse()
	{
		StartCoroutine(SetFalse(bullet_3));
		animator.SetTrigger("3_");
        BulletAudio.Play();
    }

	public void Bullet_4_Inverse()
	{
		StartCoroutine(SetFalse(bullet_4));
		animator.SetTrigger("4_");
        BulletAudio.Play();
    }

	public void Bullet_5_Inverse()
	{
		StartCoroutine(SetFalse(bullet_5));
		animator.SetTrigger("5_");
        BulletAudio.Play();
    }

	public void Bullet_6_Inverse()
	{
		StartCoroutine(SetFalse(bullet_6));
		animator.SetTrigger("6_");
        BulletAudio.Play();
    }

	private IEnumerator SetFalse(GameObject bullet)
	{
		yield return new WaitForSeconds(0.167f);
		bullet.SetActive(false);
	}
}
