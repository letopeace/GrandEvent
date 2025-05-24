using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAnim : MonoBehaviour
{
    public Animator animator;
    public Transform parentOffset;
	public ParticleSystem shootingEffect;

	private void Start()
	{
		animator = GetComponent<Animator>();
		parentOffset = transform.parent;
	}

	public void RotationReset()
	{
		parentOffset.eulerAngles = Vector3.zero;
	}

	public void OpenDrum()
	{
		animator.SetTrigger("Open");
	}

	public void CloseDrum()
	{
		animator.SetTrigger("Close");
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
}
