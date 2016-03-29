using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	//speed
	public float speed =10;

	//Target (set by Tower)
	public Transform target;

	void FixedUpdate()
	{
		if (target) 
		{
			//Fly towards the target
			Vector3 dir = target.position - transform.position;
			GetComponent<Rigidbody> ().velocity = dir.normalized * speed;
		}
		else 
		{
			//Otherwise destroy self
			Destroy(gameObject);
		}
}
	void OnTriggerEnter(Collider co)
	{
		Health health = co.GetComponentInChildren<Health> ();
		if (health) {
			health.decrease ();
			Destroy (gameObject);
		}
	}

}