using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeJuWaterGun : Tile
{
	private bool heldByPlayer;
	//Transform gun=this.transform;
	public GameObject water;
	void Update() {
		Vector2 attemptToMoveDir = Vector2.zero;
		
		if (heldByPlayer && Input.GetKeyDown (KeyCode.X)) {
			Instantiate (water, transform.position, Quaternion.identity);
		}



		bool tryToMoveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
		bool tryToMoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
		bool tryToMoveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
		bool tryToMoveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);



		if (tryToMoveUp) {
			attemptToMoveDir += Vector2.up;
		}
		else if (tryToMoveDown) {
			attemptToMoveDir -= Vector2.up;			
		}
		if (tryToMoveRight) {
			attemptToMoveDir += Vector2.right;
		}
		else if (tryToMoveLeft) {
			attemptToMoveDir -= Vector2.right;
		}
		attemptToMoveDir.Normalize();

		if (heldByPlayer) {
			if (attemptToMoveDir.x > 0) {
				_sprite.flipX = true;
			} else if (attemptToMoveDir.x < 0) {
				_sprite.flipX = false;
			}
		}




	}

	public override void pickUp(Tile tilePickingUsUp) {
		base.pickUp (tilePickingUsUp);
		if (tilePickingUsUp.hasTag (TileTags.Player)) heldByPlayer = true;
		}

	public override void dropped(Tile tileDroppingUs){
		heldByPlayer = false;
		base.dropped (tileDroppingUs);
	}


}