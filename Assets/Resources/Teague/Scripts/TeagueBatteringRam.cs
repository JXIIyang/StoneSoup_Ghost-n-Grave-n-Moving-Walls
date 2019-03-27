using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueBatteringRam : Tile
{
    public List<Tile> tilesHit = new List<Tile>();

    public override void useAsItem(Tile tileUsingUs)
    {
        if (tileUsingUs != null)
        {
            tileUsingUs.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ContactFilter2D cf = new ContactFilter2D();
            cf.NoFilter();
            if (Physics2D.Raycast(tileUsingUs.transform.position, tileUsingUs.aimDirection, cf, _maybeRaycastResults, 25) > 0)
            {
                //get which tile was hit
                Tile affectedTile = null;
                int i = 0;
                //while we're in range, the collider is hit, and the tile hasn't been set
                while (i < _maybeRaycastResults.Length && _maybeRaycastResults[i].collider != null && affectedTile == null)
                {
                    Tile tempTile = _maybeRaycastResults[i].collider.GetComponent<Tile>();
                    if (tempTile != null && tempTile != this && !tempTile.hasTag(TileTags.Player) && (!tempTile.hasTag(TileTags.CanBeHeld) || tempTile.hasTag(TileTags.Wall)))
                    {
                        affectedTile = tempTile;
                        print(affectedTile);
                    }
                    i++;
                }
                if (affectedTile && Vector2.Distance(affectedTile.transform.position, tileUsingUs.transform.position) <= 25)
                {
                    if (affectedTile.gameObject.GetComponent<Rigidbody2D>() == null)
                    {
                        
                        affectedTile.gameObject.AddComponent<Rigidbody2D>().drag = 1f;
                        affectedTile.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        affectedTile.gameObject.AddComponent<TeagueMovableTile>().trueTile = affectedTile;
                        takeDamage(this, 1);
                    }
                    affectedTile.addForce(tileUsingUs.aimDirection * 4000);
                }
            }
            tileUsingUs.addForce(tileUsingUs.aimDirection * 2000);
        }
    }

    public void Update()
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

        updateSpriteSorting();
    }
}
