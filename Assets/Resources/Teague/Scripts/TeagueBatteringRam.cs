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
            if (Physics2D.Raycast(tileUsingUs.transform.position, tileUsingUs.aimDirection, new ContactFilter2D(), _maybeRaycastResults, 100) > 0)
            {
                //get which tile was hit
                Tile affectedTile = null;
                int i = 0;
                //while we're in range, the collider is hit, and the tile hasn't been set
                while (i < _maybeRaycastResults.Length && _maybeRaycastResults[i].collider != null && affectedTile == null)
                {
                    Tile tempTile = _maybeRaycastResults[i].collider.GetComponent<Tile>();
                    if (tempTile != null && !tempTile.hasTag(TileTags.Player))
                    {
                        affectedTile = tempTile;
                    }
                    i++;
                }
                if (affectedTile && Vector2.Distance(affectedTile.transform.position, tileUsingUs.transform.position) <= 100)
                {
                    if (affectedTile.gameObject.GetComponent<Rigidbody2D>() == null)
                    {
                        
                        affectedTile.gameObject.AddComponent<Rigidbody2D>().drag = 1f;
                        affectedTile.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        affectedTile.gameObject.AddComponent<TeagueMovableTile>().trueTile = affectedTile;
                        takeDamage(this, 1);
                    }
                    affectedTile.addForce(tileUsingUs.aimDirection * 5000);
                }
            }
            tileUsingUs.addForce(tileUsingUs.aimDirection * 2000);
        }
    }
}
