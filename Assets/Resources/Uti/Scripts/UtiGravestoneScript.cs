using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiGravestoneScript : Tile
{
    int dieChance;
    bool pickedUp = false;
    public GameObject ghost;
    bool ghostCalled = false;
    public GameObject blackApple;
    // Start is called before the first frame update
    void Start()
    {
        dieChance = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (dieChance < 66)
        {
            die();
        }
    }

    public override void pickUp(Tile tilePickingUsUp)
    {
        if (!hasTag(TileTags.CanBeHeld))
        {
            return;
        }
        if (_body != null)
        {
            _body.velocity = Vector2.zero;
            _body.bodyType = RigidbodyType2D.Kinematic;
        }
        transform.parent = tilePickingUsUp.transform;
        transform.localPosition = new Vector3(heldOffset.x, heldOffset.y, -0.1f);
        transform.localRotation = Quaternion.Euler(0, 0, heldAngle);
        removeTag(TileTags.CanBeHeld);
        tilePickingUsUp.tileWereHolding = this;
        _tileHoldingUs = tilePickingUsUp;
        if (!pickedUp)
        {
            GameObject apple = Instantiate(blackApple);
            apple.transform.position = transform.position;
            if (GameObject.Find("UtiCursed(Clone)") && !ghostCalled)
            {
                GameObject ghost1 = Instantiate(ghost);
                ghost1.transform.position = transform.position;
                ghostCalled = true;
            }
            pickedUp = true;
        }
        updateSpriteSorting();
    }

    protected override void die()
    {
        _alive = false;

        if (tileWereHolding != null)
        {
            tileWereHolding.dropped(this);
        }
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        if (deathSFX != null)
        {
            AudioManager.playAudio(deathSFX);
        }
        if (dieChance > 66)
        {
            AddCursed();
            if (!pickedUp)
            {
                GameObject apple = Instantiate(blackApple);
                apple.transform.position = transform.position;
            }
            if (GameObject.Find("UtiCursed(Clone)") && !ghostCalled)
            {
                GameObject ghost1 = Instantiate(ghost);
                ghost1.transform.position = transform.position;
                ghostCalled = true;
            }
        }
        
        Destroy(gameObject);
    }

    void AddCursed()
    {
        if (GameObject.Find("UtiCursed(Clone)"))
        {
           
            GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().cursed += 1;
        }
    }
}
