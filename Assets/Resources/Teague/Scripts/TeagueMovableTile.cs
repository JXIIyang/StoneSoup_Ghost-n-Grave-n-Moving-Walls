using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeagueMovableTile : Tile
{
    //the actual tile that controls this object
    public Tile trueTile;
    public float requiredForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Tile tileWeHit = collision.gameObject.GetComponent<Tile>();
        if (tileWeHit != null && !tileWeHit.hasTag(TileTags.Player))
        {
            if (collisionImpactLevel(collision) > requiredForce)
            {
                Tile tileLaunched = this;
                if (trueTile != null)
                {
                    tileLaunched = trueTile;
                }
                tileWeHit.takeDamage(tileLaunched, 3, DamageType.Explosive);
                tileLaunched.takeDamage(tileLaunched, 3, DamageType.Explosive);
            }
        }
    }
}
