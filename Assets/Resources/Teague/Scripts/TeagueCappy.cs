using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueCappy : Tile
{
    // Sound that's played when we're thrown.
    public AudioClip throwSound;

    // How much force to add when thrown
    public float throwForce = 3000f;

    // How slow we need to be going before we consider ourself "on the ground" again
    public float onGroundThreshold = 0.8f;

    // How much relative velocity we need with a target on a collision to cause damage.
    public float damageThreshold = 14;
    // How much force we apply to a target when we deal damage. 
    public float damageForce = 1000;

    // We keep track of the tile that threw us so we don't collide with it immediately.
    protected Tile _tileThatThrewUs = null;

    // Keep track of whether we're in the air and whether we were JUST thrown
    protected bool _isInAir = false;
    protected float _afterThrowCounter;
    public float afterThrowTime = 0.2f;

    public override void useAsItem(Tile tileUsingUs)
    {
        if (_tileHoldingUs != tileUsingUs)
        {
            return;
        }
        if (onTransitionArea())
        {
            return; // Don't allow us to be thrown while we're on a transition area.
        }
        AudioManager.playAudio(throwSound);

        _sprite.transform.localPosition = Vector3.zero;

        _tileThatThrewUs = tileUsingUs;
        _isInAir = true;

        // We use IgnoreCollision to turn off collisions with the tile that just threw us.
        if (_tileThatThrewUs.GetComponent<Collider2D>() != null)
        {
            Physics2D.IgnoreCollision(_tileThatThrewUs.GetComponent<Collider2D>(), _collider, true);
        }

        // We're thrown in the aim direction specified by the object throwing us.
        Vector2 throwDir = _tileThatThrewUs.aimDirection.normalized;

        // Have to do some book keeping similar to when we're dropped.
        _body.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = tileUsingUs.transform.parent;
        _tileHoldingUs.tileWereHolding = null;
        _tileHoldingUs = null;

        _collider.isTrigger = false;

        // Since we're thrown so fast, we switch to continuous collision detection to avoid tunnelling
        // through walls.
        _body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Finally, here's where we get the throw force.
        _body.AddForce(throwDir * throwForce);

        _afterThrowCounter = afterThrowTime;
    }
}
