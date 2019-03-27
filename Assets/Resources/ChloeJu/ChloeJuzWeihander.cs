using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeJuzWeihander : Tile
{

	// Sound effects to play when we're swung or picked up.
	public AudioClip swingSound, pickupSound;

	// We use a pivot object to swing around whatever's holding us
	// (since we can't rotate whatever's holding us).
	// When we're not swinging, the pivot hangs around as our child.
	// When we're swinging, we swap places with the pivot so it becomes our parent.
	public Transform swingPivot;


	// We behave differently when we're swinging vs. when we're not.
	protected bool _swinging = false;
	public float damageForce = 1000;

	// The speed we swing (in degrees/second)
	public float swingSpeed = 1440f;
	// The current angle of our swing (we swing 360 degrees before we stop swinging)
	protected float _swingAngle;

	// We use the aim direction to determine where to start our swing, so we need to keep track of the start angle
	// to tell when we've hit 360 degrees.
	protected float _pivotStartAngle;


	// We don't take damage if we're swinging or being held by an object.
	public override void takeDamage(Tile tileDamagingUs, int amount, DamageType damageType) {
		if (_swinging || _tileHoldingUs != null) {
			return;
		}
		base.takeDamage(tileDamagingUs, amount, damageType);
	}

	// Pick up is the same except we play an extra sound.
	public override void pickUp(Tile tilePickingUsUp) {
		base.pickUp(tilePickingUsUp);
		if (_tileHoldingUs != null) {
			AudioManager.playAudio(pickupSound);
		}
	}


	public override void useAsItem(Tile tileUsingUs) {



		// These values can be tuned to make us rotate/offset differently from our pivot.
		transform.localPosition = new Vector3(1.2f, 0, 0);
		transform.localRotation = Quaternion.Euler(0, 0, -90);



		_swingAngle = 0;
	}

	// Can't drop us while we're swinging.
	public override void dropped(Tile tileDroppingUs) {
		if (_swinging) {
			return;
		}
		base.dropped(tileDroppingUs);
	}

	void Update() {

	}

	// Finally, try to hurt any tile we hit while we're swinging. 
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.GetComponent<Tile>() != null) {
			Tile otherTile = other.gameObject.GetComponent<Tile>();
			if (otherTile != _tileHoldingUs && !otherTile.hasTag(TileTags.CanBeHeld)) {
				otherTile.takeDamage(this, 1);
				//otherTile.addForce((other.transform.position-_tileHoldingUs.transform.position).normalized*damageForce);
			}
		}
	}

	void OnColliderEnter2D(Collider2D other) {
		if (other.gameObject.GetComponent<Tile>() != null) {
			Tile otherTile = other.gameObject.GetComponent<Tile>();

		}
	}
}
