using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeJuBeer : Tile
{


    // Start is called before the first frame update
    void Start()

    {
//		GameObject go = GameObject.Find("Player");

//		Player other = (Player) go.GetComponent(typeof(Player));
//		other.moveSpeed=-other.moveSpeed;

			
        
    }
	void OnTriggerEnter2D(Collider2D otherCollider) {
		Tile maybeTile = otherCollider.GetComponent<Tile>();
		if (maybeTile != null ) {
			

			Player py =maybeTile.GetComponent<Player> ();
			//py.moveSpeed = 3;

			py.moveSpeed+=5;
			py.moveSpeed = -py.moveSpeed;
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
