using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour {

	private GameObject playerBall;
	private float gameLayerBottom;
	// Use this for initialization

	void gameOver()
	{
		Destroy (playerBall);
		if(SystemInfo.deviceType == DeviceType.Handheld)
			Handheld.Vibrate ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (2);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag ("Spikes") ) 
		{
			gameOver ();
		}
	}

	void Start () {
		playerBall = GameObject.FindGameObjectWithTag ("Player");
		Bounds gameLayerBounds = GameObject.Find ("Main Camera").GetComponent<Collider2D> ().bounds;
		gameLayerBottom = gameLayerBounds.center.y - (gameLayerBounds.size.y / 2);
	}
	 
	// Update is called once per frame
	void Update () {
		
	}
}
