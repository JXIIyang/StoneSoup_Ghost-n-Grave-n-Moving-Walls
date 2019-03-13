using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaguePressurePad : Tile
{
    public TeagueLock myLock;
    public GameObject triggering;

    void Update()
    {
        if (myLock == null && triggering == null)
        {
            TeagueLock[] allLocks = FindObjectsOfType<TeagueLock>();
            TeagueLock nearest = myLock;
            foreach (TeagueLock l in allLocks)
            {
                if (l != null && l.pressurePad == null)
                {
                    if (nearest == null || Vector2.Distance(l.transform.position, transform.position) < Vector2.Distance(nearest.transform.position, transform.position))
                    {
                        nearest = l;
                    }
                }
            }
            nearest.pressurePad = this;
            myLock = nearest;
        }
        if (triggering != null)
        {
            triggering.transform.position = Vector2.Lerp(triggering.transform.position, transform.position, Time.deltaTime * 5);
            if (myLock != null)
            {
                Destroy(myLock.gameObject);
                myLock = null;
            }
            if (triggering.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(triggering.GetComponent<Rigidbody2D>());
            }
            sprite.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tile>() != null && collision.gameObject.GetComponent<Tile>().hasTag(TileTags.Wall))
        {
            if (triggering == null && Vector2.Distance(collision.transform.position, transform.position) < 1)
            {
                triggering = collision.gameObject;
            }
        }
    }

    public override void takeDamage(Tile tileDamagingUs, int damageAmount, DamageType damageType)
    {

    }

    protected override void updateSpriteSorting()
    {
        
    }
}
