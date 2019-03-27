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
                    if (tileThatThrewUs.tileWereHolding != null)
                    {
                        tileThatThrewUs.tileWereHolding.dropped(tileThatThrewUs);
                    }
                    pickUp(tileThatThrewUs);
                    thrown = false;
                }
            }
        }
        else
        {
            if (_tileHoldingUs != null && _tileHoldingUs.hasTag(TileTags.Player))
            {
                if (Player.instance.sprite.flipX && !sprite.flipX)
                {
                    sprite.flipX = true;
                    transform.localPosition = new Vector2(-heldOffset.x, heldOffset.y);
                }
                if (!Player.instance.sprite.flipX && sprite.flipX)
                {
                    sprite.flipX = false;
                    transform.localPosition = new Vector2(heldOffset.x, heldOffset.y);
                }
            }
        }
        updateSpriteSorting();
    }
    
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(thrown && collision.gameObject.GetComponent<Tile>() != null)
        {
            /*
            Tile tileWeHit = collision.gameObject.GetComponent<Tile>();
            if (tileWeHit.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                gameObject.AddComponent<TeagueCappyPlayer>().possessed = tileWeHit.gameObject;
                GetComponent<TeagueCappyPlayer>().playerState = Player.instance.gameObject;
                GetComponent<Collider2D>().isTrigger = true;
                transform.position = tileWeHit.transform.position + new Vector3(0, collision.collider.bounds.extents.y + collision.collider.offset.y, transform.position.z);
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                transform.parent = tileWeHit.transform;
                GetComponent<TeagueCappyPlayer>().UpdatePlayer();
                //Destroy(tileThatThrewUs.gameObject);
                Destroy(this);
            }
            */
            collision.gameObject.GetComponent<Tile>().takeDamage(this, 1);
            collision.gameObject.GetComponent<Tile>().addForce((collision.transform.position - transform.position).normalized * 1500);
        }
    }

    public override void takeDamage(Tile tileDamagingUs, int damageAmount, DamageType damageType)
    {
        
    }
}
