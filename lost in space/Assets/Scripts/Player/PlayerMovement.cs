using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 2f;
	public float jumpForce = 16f;

	public LayerMask groundMask;

	private Rigidbody body;

	private bool grounded = false;

	void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 vel = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y);
		body.velocity = vel;

		if(Input.GetAxis("Jump") > 0)
			body.AddForce(new Vector3(0f, jumpForce));
	}
}
