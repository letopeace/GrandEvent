using System.Collections;
using TMPro;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public Boostrap boostrap;
	public ChipTriggerZone destinationTrigger;
	public float up = 1.86f, brokeForce = 1;
	public bool players = false;
	public Vector3 target;
	public ChipType chipType;

	private Rigidbody[] chips = new Rigidbody[5];
    private Rigidbody rb;
    private GameObject outline;
	private bool isGrabbed = false;


	public static Vector3 GetRandomUpwardDirection(float maxAngleDegrees)
	{
		// Случайный угол в радианах от 0 до maxAngleDegrees
		float angle = Random.Range(0f, maxAngleDegrees);
		float azimuth = Random.Range(0f, 360f);

		// Преобразуем в сферические координаты
		float angleRad = angle * Mathf.Deg2Rad;
		float azimuthRad = azimuth * Mathf.Deg2Rad;

		// Сферические -> декартовы координаты
		float x = Mathf.Sin(angleRad) * Mathf.Cos(azimuthRad);
		float z = Mathf.Sin(angleRad) * Mathf.Sin(azimuthRad);
		float y = Mathf.Cos(angleRad); // Всегда вверх

		return new Vector3(x, y, z).normalized;
	}

	public void Betting()
	{
		StartCoroutine(Reach(target));
	}


	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		outline = transform.GetChild(0).gameObject;
		for (int i = 0; i < 5; i++)
		{
			chips[i] = transform.GetChild(i + 1).GetComponent<Rigidbody>();
		}
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

	public void ChipDestroy()
	{
		foreach (Rigidbody chipRb in chips)
		{
			Vector3 dir = GetRandomUpwardDirection(35f);
			chipRb.useGravity = true;
			chipRb.AddForce(dir * brokeForce);
		}

		StartCoroutine(DestroyYourSelf());
	}

	public IEnumerator DestroyYourSelf()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
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
