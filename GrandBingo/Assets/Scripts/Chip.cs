using System.Collections;
using TMPro;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public Boostrap boostrap;
	public ChipTriggerZone destinationTrigger;
	public float up = 1.86f;
	public bool players = false;
	public Vector3 target;

    private Rigidbody rb;
    private GameObject outline;
	private bool isGrabbed = false;


	public void Betting()
	{
		StartCoroutine(Reach(target));
	}


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline = transform.GetChild(0).gameObject;
    }

	private void OnMouseEnter()
	{
		if (!players) return;
		outline.SetActive(true);
	}

    private void OnMouseExit()
	{
		if (!players) return;
		if (isGrabbed)
		{
			return;
		}
        outline.SetActive(false);
    }

	private void OnMouseDown()
	{
		if (!players) return;
		if (boostrap.bid && boostrap.turn)
		{
			rb.useGravity = false;
			StartCoroutine(Up());
			isGrabbed = true;

			destinationTrigger.Show();
		}
	}

	private void OnMouseUp()
	{
		if (!players) return;
		StopAllCoroutines();
		rb.useGravity = true;
		isGrabbed = false;

		destinationTrigger.Hide();
	}

	private void OnMouseDrag()
	{
		if (!players) return;
		Vector3 place = CameraManager.PointOnCamera();

		place.y = up;

		if (place.z > 0.5f)
		{
			place.z = 0.5f;
		}
		else if (place.z < -1.358018f)
		{
			place.z = -1.358018f;
		}
		if (place.x > -2.19f)
		{
			place.x = -2.19f;
		}
		else if (place.x < -3.828147f)
		{
			place.x = -3.828147f;
		}

		transform.position = place;
	}


	private IEnumerator Up()
	{
		Debug.Log("UPSTARTED");
		while (up - transform.position.y > 0.05f)
		{
			Vector3 dest = transform.position;
			dest.y = up;

			transform.position = Vector3.Lerp(transform.position, dest, 0.1f);
			yield return null;
		}
		
		Debug.Log("UPENDED");
	}

	private IEnumerator Reach(Vector3 targetPosition)
	{
		float elapsed = 0f;

		while (elapsed < 1 && (transform.position - targetPosition).magnitude > 0.14f)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition, elapsed / 1);
			elapsed += Time.deltaTime;
			yield return null;
		}
	}
}
