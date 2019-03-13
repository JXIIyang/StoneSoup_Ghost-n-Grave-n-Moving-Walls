using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eleanor_FPController : Tile
{

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Tile maybeTile = other.GetComponent<Tile>();
        if (maybeTile == null) return;
        if (maybeTile.hasTag(TileTags.Creature))
        {
            _anim.SetTrigger("Push");
//            maybeTile.takeDamage(this,1);
            maybeTile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 100, ForceMode2D.Impulse);
        }
    }
    
}
