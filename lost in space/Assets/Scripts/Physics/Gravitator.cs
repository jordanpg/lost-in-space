using UnityEngine;
using System.Collections;

public class Gravitator : MonoBehaviour
{
	public GameObject target;

	private Rigidbody2D body;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if(target)
		{
			Vector3 relDown = target.transform.position - transform.position;
			body.AddForce(relDown);
			//transform.rotation = Quaternion.LookRotation(relDown);
		}
	}
}
