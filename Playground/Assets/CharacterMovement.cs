using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

	public float forwardVelocity;
	public float backwardVelocity;
	public float angularVelocity;
	public float jumpPower;

	private Animator animator;
	private Rigidbody rigidbody;
	private bool shouldJump;

	private void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		rigidbody = gameObject.GetComponent<Rigidbody>();
		shouldJump = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			shouldJump = true;
		}
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.W)) {
			rigidbody.AddForce(forwardVelocity * transform.forward);
			animator.SetBool("Running", true);
		} else if (Input.GetKey(KeyCode.S)) {
			rigidbody.AddForce(-backwardVelocity * transform.forward);
			animator.SetBool("Running", true);
		} else {
			if (rigidbody.velocity.magnitude > 0)
			{
				rigidbody.AddForce(-backwardVelocity * Vector3.Normalize(rigidbody.velocity));
			}
			animator.SetBool("Running", false);
		}
		if (shouldJump)
		{
			rigidbody.AddForce(jumpPower * transform.up);
			shouldJump = false;
		}
		if (Input.GetKey(KeyCode.A)) {
			rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles - angularVelocity * transform.up));
		} else if (Input.GetKey(KeyCode.D)) {
			rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles + angularVelocity * transform.up));
		}
	}
}
