using UnityEngine;

public class RotAmimation : MonoBehaviour
{
	public float angle = 1;
    public bool is_rot = true;
	public bool isImportant = false;
	public bool deltaTime = false;

	void LateUpdate()
	{
		if (IsPlaying.instance && !IsPlaying.instance.isPlay() && isImportant == false) return;

		if (deltaTime) angle += angle * 0.1f * Time.deltaTime;

		if (is_rot)
		{
			transform.rotation *= Quaternion.AngleAxis(angle * 4, Vector3.back);
		}
		else
		{
			transform.rotation *= Quaternion.AngleAxis(angle * 4, Vector3.forward);
		}
	}
}