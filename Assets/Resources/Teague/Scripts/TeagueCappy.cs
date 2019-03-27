using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueCappy : Tile
{
    public Tile tileThatThrewUs;
    public Vector2 throwDestination;
    public float throwDistance;
    public float timeAtMax;
    public float maxTime;
    public bool thrown;

    public override void useAsItem(Tile tileUsingUs)
    {
        if (!thrown)
        {
            // We use IgnoreCollision to turn off collisions with the tile that just threw us.
            if (tileUsingUs.GetComponent<Collider2D>() != null)
            {
                Physics2D.IgnoreCollision(tileUsingUs.GetComponent<Collider2D>(), _collider, true);
            }

            throwDestination = (Vector2)tileUsingUs.transform.position + (tileUsingUs.aimDirection.normalized * throwDistance);

            // Have to do some book keeping similar to when we're dropped.
            _body.bodyType = RigidbodyType2D.Dynamic;
            _collider.isTrigger = false;
            transform.parent = tileUsingUs.transform.parent;
            tileUsingUs.tileWereHolding = null;
            _tileHoldingUs = null;
            tileThatThrewUs = tileUsingUs;

            timeAtMax = 0;
            thrown = true;
        }
    }

    protected virtual void Update()
    {
        if (thrown)
        {
            if (timeAtMax < maxTime)
            {
                transform.position = Vector2.MoveTowards(transform.position, throwDestination, Time.deltaTime * 20);
                if (Vector2.Distance(transform.position, throwDestination) < .5f)
                {
                    timeAtMax += Time.deltaTime;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, tileThatThrewUs.transform.position, Time.deltaTime * 20);
                if (Vector2.Distance(transform.position, tileThatThrewUs.transform.position) < .5f)
                {
                    addTag(TileTags.CanBeHeld);
                    _collider.isTrigger = true;
                    pickUp(tileThatThrewUs);
                    thrown = false;
                }
            }
        }

        updateSpriteSorting();
    }

    // When we collide with something in the air, we try to deal damage to it.
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
