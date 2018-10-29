using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	private GameObject playerBall;
	private DeviceType deviceType;
	private Rigidbody2D ballRigidBody;
	public float speed = 30.0f;

	void moveBallUsingAccelerometer()
	{
		ballRigidBody.AddForce (new Vector2 (Input.acceleration.x * speed * 0.7f, 0.0f));
	}

	void moveBallUsingKeys()
	{
		if (Input.GetKey (KeyCode.LeftArrow))
			ballRigidBody.AddForce(new Vector2(-0.4f * speed, 0));
		else if (Input.GetKey (KeyCode.RightArrow))
			ballRigidBody.AddForce(new	Vector2(0.4f * speed, 0));
	}

	void moveBall()
	{
		if (deviceType == DeviceType.Handheld)
			moveBallUsingAccelerometer ();
		else
			moveBallUsingKeys ();
	}

	// Use this for initialization
	void Start () 
	{
		playerBall = GameObject.FindGameObjectWithTag ("Player");
		ballRigidBody = playerBall.GetComponent<Rigidbody2D> ();
		deviceType = SystemInfo.deviceType;
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveBall ();
	}
}
