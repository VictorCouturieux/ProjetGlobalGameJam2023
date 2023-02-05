using UnityEngine;
using System.Collections;

public class TrajectoryHelper : MonoBehaviour
{
	public Transform handPoint;
	public Transform minTarget;
	public Transform maxTarget;

	public float minApex = 5;
	public float maxApex = 5;

	[SerializeField] private float gravity = -50f;

	public bool debugPath;

    void Start()
    {
		Physics.gravity = new Vector3(0, gravity, 0);    
    }

    private void OnDrawGizmos()
    {
		if(debugPath)
        {
			DrawPath(minTarget.position, minApex);
			DrawPath(maxTarget.position, maxApex);
		}
    }

	public Vector3 CalculateVelocity(float value)
    {
		value = Mathf.Clamp(value, 0, 1);
		Vector3 target = minTarget.position + ((maxTarget.position - minTarget.position) * value);
		float apex = minApex + ((maxApex - minApex) * value);
		return CalculateLaunchData(target, apex).initialVelocity;
    }

    private LaunchData CalculateLaunchData(Vector3 target, float h)
	{
		float displacementY = target.y - handPoint.position.y;
		Vector3 displacementXZ = new Vector3(target.x - handPoint.position.x, 0, target.z - handPoint.position.z);
		float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath(Vector3 target, float h)
	{
		LaunchData launchData = CalculateLaunchData(target, h);
		Vector3 previousDrawPoint = handPoint.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++)
		{
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = handPoint.position + displacement;
			Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	public struct LaunchData
	{
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData(Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
	}
}