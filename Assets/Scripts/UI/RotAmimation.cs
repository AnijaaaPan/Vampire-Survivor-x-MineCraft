using UnityEngine;

public class RotAmimation : MonoBehaviour
{
	public float angle = 1;
    public bool is_rot = true;
	public bool isImportant = false;

    void LateUpdate()
	{
		if (IsPlaying.instance && !IsPlaying.instance.isPlay() && isImportant == false) return;

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