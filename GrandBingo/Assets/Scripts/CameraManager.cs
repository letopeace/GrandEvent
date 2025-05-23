using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public Transform head;
	public Camera cam;

	public Vector3 startRot;
	public Vector2 maxRotation = new Vector2(30f, 60f); // Ограничения по X и Y

	public float sensitivity = 0.1f;


	public static Vector3 PointOnCamera(string lay = "Default")
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(lay)))
		{
			return hit.point;
		}
		return Vector3.zero; // Ничего не попали
	}

	private void Start()
	{
		startRot = head.eulerAngles;
		cam = GetComponent<Camera>();
	}

	private void LateUpdate()
	{
		// Получаем смещение курсора от центра экрана
		Vector2 mouseOffset = new Vector2(
			Input.mousePosition.x - Screen.width / 2,
			Input.mousePosition.y - Screen.height / 2
		);

		// Преобразуем смещение в желаемый угол поворота
		float targetY = startRot.y + mouseOffset.x * sensitivity; // вправо-влево
		float targetX = startRot.x - mouseOffset.y * sensitivity; // вверх-вниз (инверсия)

		// Ограничиваем повороты головы
		targetY = ClampAngle(targetY, startRot.y - maxRotation.y, startRot.y + maxRotation.y);
		targetX = ClampAngle(targetX, startRot.x - maxRotation.x, startRot.x + maxRotation.x);

		// Применяем поворот
		head.eulerAngles = new Vector3(targetX, targetY, startRot.z);
	}

	private float ClampAngle(float angle, float min, float max)
	{
		angle = NormalizeAngle(angle);
		min = NormalizeAngle(min);
		max = NormalizeAngle(max);

		// Если диапазон не пересекает 0°
		if (min < max)
		{
			return Mathf.Clamp(angle, min, max);
		}
		else // Если пересекает 0° (например, от 350 до 10)
		{
			if (angle > max && angle < min)
			{
				// Выбираем ближайшую границу
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