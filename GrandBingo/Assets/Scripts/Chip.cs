using System.Collections;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public Boostrap boostrap;
	public float up = 1.86f;

    private Rigidbody rb;
    private GameObject outline;
	private bool isGrabbed = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline = transform.GetChild(0).gameObject;
    }

	private void OnMouseEnter()
	{
		outline.SetActive(true);
	}

    private void OnMouseExit()
    {
		if (isGrabbed)
		{
			return;
		}
        outline.SetActive(false);
    }

	private void OnMouseDown()
	{
		if (boostrap.bid && boostrap.turn)
		{
			rb.useGravity = false;
			StartCoroutine(Up());
			isGrabbed = true;
		}
	}

	private void OnMouseUp()
	{
		StopAllCoroutines();
		rb.useGravity = true;
		isGrabbed = false;
	}

	private void OnMouseDrag()
	{
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
}
