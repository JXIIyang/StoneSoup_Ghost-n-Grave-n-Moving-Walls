using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Tile
{

	float f = 0;
	public bool startW = false;
    // Start is called before the first frame update
    void Start()
    {

    }

	void OnTriggerStay2D(Collider2D otherCollider) {
		Tile maybeTile = otherCollider.GetComponent<Tile>();
		if (maybeTile != null ) {


			Player py =maybeTile.GetComponent<Player> ();
			py.moveSpeed += 30;
			startW=true;

			//py.moveSpeed = -py.moveSpeed;
		}
	}
	void OnTriggerExit2D(Collider2D otherCollider) {
		Tile maybeTile = otherCollider.GetComponent<Tile>();
		if (maybeTile != null ) {


			Player py =maybeTile.GetComponent<Player> ();
			py.moveSpeed = 10;
			startW=true;

			//py.moveSpeed = -py.moveSpeed;
		}
	}


	// Update is called once per frame
	void Update()


	{
		
}
}