using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayScript : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public List<Tile> Enemies = new List<Tile>();
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Sprite.color.a < 0.7f) return;
        Tile enemyTile = other.GetComponent<Tile>();
        if (enemyTile.hasTag(TileTags.Enemy) && !Enemies.Contains(enemyTile))
        {
            Enemies.Add(enemyTile);
        }
    }
}
