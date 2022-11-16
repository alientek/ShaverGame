using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RigidBodyController : MonoBehaviour
{
	public SteamVR_Input_Sources inputController;
	public SteamVR_Action_Vector2 input;
	public SteamVR_Action_Boolean jump;
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;


	void Awake()
	{
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
	}

	void FixedUpdate()
	{
		if (grounded)
		{
			var inputX = input.axis.x;
			var inputY = input.axis.y;

			Vector3 targetVelocity = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
			targetVelocity *= speed;

			if(targetVelocity != new Vector3(0,0,0))
			{
				Vector3 velocity = GetComponent<Rigidbody>().velocity;
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.y = 0;
				GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
			}

			if (canJump && jump.GetStateDown(inputController))
			{
				Vector3 velocity = GetComponent<Rigidbody>().velocity;
				GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}

		GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

		grounded = false;
	}

	private void OnCollisionStay(Collision collision)
	{
		grounded = true;
	}

	float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}
