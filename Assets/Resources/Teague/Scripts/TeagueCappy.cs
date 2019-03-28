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
    
    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(thrown && collider.gameObject.GetComponent<Tile>() != null)
        {
            GameObject tileWeHit = collider.gameObject;
            if (tileWeHit.GetComponent<Rigidbody2D>() != null)
            {
                Component[] removedTiles = tileWeHit.GetComponents<Tile>();
                TeagueCappyPlayer cappyPlayer = tileWeHit.AddComponent<TeagueCappyPlayer>();
                cappyPlayer.init();
                cappyPlayer.possessed = removedTiles;
                foreach (Tile t in removedTiles)
                {
                    Destroy(t);
                }
                cappyPlayer.playerState = tileThatThrewUs.gameObject;
                cappyPlayer.cappyOffset = new Vector3(0, collider.bounds.extents.y + collider.offset.y, transform.position.z);
                cappyPlayer.cappy = gameObject;
                transform.position = tileWeHit.transform.position + cappyPlayer.cappyOffset;
                Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>(), true);
                transform.parent = tileWeHit.transform;
                tileThatThrewUs.gameObject.SetActive(false);
                cappyPlayer.UpdatePlayer();
                cappyPlayer.health = cappyPlayer.playerState.GetComponent<Tile>().health;
                Destroy(this);
            }
        }
    }

    public override void takeDamage(Tile tileDamagingUs, int damageAmount, DamageType damageType)
    {
        
    }
}
