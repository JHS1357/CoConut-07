using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamaraMove : MonoBehaviour
{
	public Transform maxL;
	public Transform maxR;
	public float speed = 3f;
	public float autoSpeed = 0.3f;
	
	private int d = 1;

	private float rX;
	private float rY;

	private void Update()
	{
		//MouseMove();
		AutoCamMove();
		//PhoneTilt();
	}

	private void MouseMove()
	{
		rX = Input.GetAxis("Mouse X");
		rY = Input.GetAxis("Mouse Y");

		transform.Rotate(Vector3.up * rX * speed * Time.deltaTime);
		transform.Rotate(-Vector3.right * rY * speed * Time.deltaTime);
	}

	private void AutoCamMove()
	{
		if (Vector3.Distance(transform.position, maxL.position) <= 0.2f)
			d = -1;
		else if (Vector3.Distance(transform.position, maxR.position) <= 0.2f)
			d = 1;

		transform.Translate(Vector3.right * d * autoSpeed * Time.deltaTime);
	}

	private void PhoneTilt()
	{
		rX = Input.acceleration.x;
		rY = Input.acceleration.y;

		transform.Rotate(Vector3.up * rX * speed * Time.deltaTime);
		transform.Rotate(-Vector3.right * rY * speed * Time.deltaTime);
	}
}
