using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

	public float forwardForce;
	public float backwardForce;
	public float angularVelocity;
	public float jumpPower;
	public float maximumForwardSpeed;
	public float maximumBackwardSpeed;
	public float groundingColliderHeight;

	private Animator animator;
	private Rigidbody rigidbody;
	private BoxCollider collider;
	private bool shouldJump;

	private void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		rigidbody = gameObject.GetComponent<Rigidbody>();
		collider = gameObject.GetComponent<BoxCollider>();
		shouldJump = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			shouldJump = true;
		}
	}

	private void FixedUpdate()
	{
		if (isGrounded())
		{
			if (Input.GetKey(KeyCode.W))
			{
				if ((Vector3.Dot(transform.forward, rigidbody.velocity) * rigidbody.velocity.normalized).magnitude < maximumForwardSpeed)
				{
					rigidbody.AddForce(forwardForce * transform.forward);
				}
				animator.SetBool("Running", true);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				if ((Vector3.Dot(-transform.forward, rigidbody.velocity) * rigidbody.velocity.normalized).magnitude < maximumBackwardSpeed)
				{
					rigidbody.AddForce(backwardForce * -transform.forward);
				}
				animator.SetBool("Running", true);
			}
			else
			{
				if (rigidbody.velocity.magnitude > 0)
				{
					rigidbody.AddForce(-backwardForce * Vector3.Normalize(rigidbody.velocity));
				}
				animator.SetBool("Running", false);
			}
			if (shouldJump)
			{
				rigidbody.AddForce(jumpPower * transform.up);
			}
		}
		shouldJump = false;
		if (Input.GetKey(KeyCode.A)) {
			rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles - angularVelocity * transform.up));
		} else if (Input.GetKey(KeyCode.D)) {
			rigidbody.MoveRotation(Quaternion.Euler(rigidbody.rotation.eulerAngles + angularVelocity * transform.up));
		}
	}

	private bool isGrounded()
	{
		return !Physics.CapsuleCast(transform.position, transform.position - groundingColliderHeight * transform.up, collider.size.z, -transform.up);
	}
}
