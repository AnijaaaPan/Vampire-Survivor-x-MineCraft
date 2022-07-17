using UnityEngine;

public class RotAmimation : MonoBehaviour
{
	public float angle = 1;
	public bool is_rot = true;

	void LateUpdate()
	{
		if (is_rot)
		{
			transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
		}
		else
		{
			transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
}