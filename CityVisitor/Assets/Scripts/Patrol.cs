using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

	private int _destPoint = 0;

	public NavMeshAgent Agent;
	public Transform[] Points;

	private void Update() {
		if (Points.Length == 0) {
			return;
		}

		if (!Agent.pathPending && Agent.remainingDistance < 0.5f) {
			Agent.destination = Points[_destPoint].position;
			_destPoint = (_destPoint + 1) % Points.Length;
		}
	}

}