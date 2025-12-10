using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;

	void LateUpdate()
	{
		transform.position = target.position + offset;
	}
}