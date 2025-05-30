using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public Transform head;
	public Camera cam;

	public Vector3 startRot;
	public Vector2 maxRotation = new Vector2(30f, 60f); // ����������� �� X � Y

	public float sensitivity = 0.1f;

	public bool freeze = false;
	private Vector3 camStartPos;
	private Quaternion camStartRot;


	public static Vector3 PointOnCamera(string lay = "Default")
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(lay)))
		{
			return hit.point;
		}
		return Vector3.zero; // ������ �� ������
	}

	private void Start()
	{
		startRot = head.eulerAngles;
		cam = GetComponent<Camera>();
		camStartPos = transform.localPosition;
		camStartRot = transform.localRotation;
	}

	private void LateUpdate()
	{
		if (!freeze)
		{
			transform.localPosition = camStartPos;
			transform.localRotation = camStartRot;

			// �������� �������� ������� �� ������ ������
			Vector2 mouseOffset = new Vector2(
				Input.mousePosition.x - Screen.width / 2,
				Input.mousePosition.y - Screen.height / 2
			);

			// ����������� �������� � �������� ���� ��������
			float targetY = startRot.y + mouseOffset.x * sensitivity; // ������-�����
			float targetX = startRot.x - mouseOffset.y * sensitivity; // �����-���� (��������)

			// ������������ �������� ������
			targetY = ClampAngle(targetY, startRot.y - maxRotation.y, startRot.y + maxRotation.y);
			targetX = ClampAngle(targetX, startRot.x - maxRotation.x, startRot.x + maxRotation.x);

			// ��������� �������
			head.eulerAngles = new Vector3(targetX, targetY, startRot.z);
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(-2.8642730712890627f, 2f, -1.1278095245361329f), 0.2f);
			transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0.11790508031845093f, 0.08097749948501587f, -0.07370085269212723f, 0.9869699478149414f), 0.2f);
		}
	}

	private float ClampAngle(float angle, float min, float max)
	{
		angle = NormalizeAngle(angle);
		min = NormalizeAngle(min);
		max = NormalizeAngle(max);

		// ���� �������� �� ���������� 0�
		if (min < max)
		{
			return Mathf.Clamp(angle, min, max);
		}
		else // ���� ���������� 0� (��������, �� 350 �� 10)
		{
			if (angle > max && angle < min)
			{
				// �������� ��������� �������
				float distToMin = Mathf.DeltaAngle(angle, min);
				float distToMax = Mathf.DeltaAngle(angle, max);
				return (Mathf.Abs(distToMin) < Mathf.Abs(distToMax)) ? min : max;
			}
			return angle;
		}
	}

	private float NormalizeAngle(float angle)
	{
		angle = angle % 360f;
		if (angle < 0) angle += 360f;
		return angle;
	}
}