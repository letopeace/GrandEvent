
using UnityEngine;

public class Test : MonoBehaviour
{
    private Revolver revolver;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Show(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Show(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Show(3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Show(4);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Show(5);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Show(6);
		}
	}

    private void Show(int count)
    {
		revolver = new Revolver(count);
        bool[] drum = revolver.Show();

		foreach (bool drumItem in drum)
		{
			Debug.Log(drumItem);
		}
	}
}
