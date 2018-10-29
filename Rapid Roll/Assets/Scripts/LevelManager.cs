using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public float gameSpeed;
	private GameObject brickWall;
	private Transform gameLayer;
	private float brickWidth, gameLayerTop, gameLayerBottom,
		midPosX, leftPosX1, leftPosX2, rightPosX1, rightPosX2,
		startPosX, startPosY, lastPosY;

	float getTilePositionX()
	{
		switch (Random.Range (0, 4)) 
		{
		case 0:
			return leftPosX2;
		case 1:
			return leftPosX1;
		default:
			return midPosX;
		case 3:
			return rightPosX1;
		case 4:
			return rightPosX2;
		}
	}

	float getTilePositionY(float lastY)
	{
		return lastY - 2;
	}

	void spawnTiles(Transform parent, GameObject lastTile)
	{
		GameObject tempTile;
		lastPosY = getTilePositionY (lastTile.transform.position.y);
		for (int tileIndex = 0; tileIndex < 4; tileIndex++) 
		{
			tempTile = Instantiate (brickWall) as GameObject;
			tempTile.transform.Translate (new Vector2(
					getTilePositionX(), lastPosY
				)
			);
			tempTile.transform.parent = parent;
		}
	}

	void checkAndSpawnTileRow(GameObject lastTile)
	{
		if (lastTile.transform.position.y - 2 > gameLayerBottom) 
		{
			spawnTiles (gameLayer, lastTile);
		}
	}

	void createAndDestroyTiles()
	{
		int childCount = gameLayer.childCount;
		GameObject childTile;
		for (int childIndex = 0; childIndex < childCount; childIndex++) 
		{
			childTile = gameLayer.GetChild (childIndex).gameObject;
			if (childTile.transform.position.y > gameLayerTop + 1) {
				Destroy (childTile);
			}
			if (childIndex == childCount - 1) 
			{
				checkAndSpawnTileRow (childTile);
			}
		}
		
	}

	void moveGameLayer()
	{
		gameLayer.Translate (new Vector3(0, Time.deltaTime * gameSpeed, 0));
	}

	// Use this for initialization
	void Start () 
	{
		gameSpeed = 0.8f;
		brickWall = GameObject.Find ("brick_wall");
		startPosX = (float)GameObject.Find ("startPos").transform.position.x;
		startPosY = (float)GameObject.Find ("startPos").transform.position.y;
		lastPosY = startPosY;
		brickWidth = brickWall.transform.GetComponent<Renderer> ().bounds.size.x;
		midPosX = startPosX;
		leftPosX1 = midPosX - brickWidth;
		rightPosX1 = midPosX + brickWidth;
		leftPosX2 = leftPosX1 - brickWidth;
		rightPosX2 = rightPosX1 + brickWidth;
		gameLayer = GameObject.Find ("GameLayer").transform;
		Bounds gameLayerBounds = GameObject.Find ("Main Camera").GetComponent<Collider2D> ().bounds;
		float boundSize = (gameLayerBounds.size.y / 2);
		float gameLayerCenter = gameLayerBounds.center.y;
		gameLayerTop = gameLayerCenter + boundSize;
		gameLayerBottom = gameLayerCenter - boundSize;
		Debug.Log (string.Format("{0}, {1}, {2}, {3}, {4}", leftPosX2, leftPosX1, midPosX, rightPosX1, rightPosX2));
	}
	
	// Update is called once per frame
	void Update () 
	{
		createAndDestroyTiles ();
		moveGameLayer ();
	}
}
