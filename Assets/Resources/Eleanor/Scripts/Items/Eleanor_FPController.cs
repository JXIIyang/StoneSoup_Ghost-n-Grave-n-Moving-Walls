using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eleanor_FPController : Tile
{

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        rb.AddForce(Vector3.up * 100, ForceMode2D.Impulse);
        Tile maybeTile = other.GetComponent<Tile>();
        if (maybeTile != null && !maybeTile.hasTag(TileTags.Creature))
            maybeTile.gameObject.AddComponent<apt283ExplosiveRock>();
    }
        
    
}
