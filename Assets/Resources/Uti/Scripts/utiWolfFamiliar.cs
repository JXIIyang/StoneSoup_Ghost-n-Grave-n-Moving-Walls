using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utiWolfFamiliar : Tile
{
    bool attacking = false;
    bool attackPresent = false;
    Vector3 attackLocation;
    int attackTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        this.gameObject.AddComponent<DestroyOnDeath>();
    }

    // Update is called once per frame
    void Update()
    {

        health = 100;
        if (GameObject.Find("player_tile(Clone)"))
        {
            
            if (attacking)
            {
                transform.position = Vector2.Lerp(transform.position, attackLocation, Random.Range(0.1f, 0.15f));
                transform.position = new Vector3(transform.position.x + Random.Range(-0.05f, 0.05f), transform.position.y + Random.Range(-0.05f, 0.05f), -1f);
                GetComponent<SpriteRenderer>().color = Color.red;
                if (Vector3.Distance(transform.position, attackLocation) < 10)
                {
                    transform.position = new Vector3(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y + Random.Range(-0.3f, 0.3f), -1f);
                    attackTimer++;
                    attackPresent = true;
                    if (attackTimer > 100)
                    {
                        
                        attackTimer = 0;
                        attackPresent = false;
                        attacking = false;
                    }
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                if (Input.GetMouseButtonDown(1))
                {
                    attackLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    attacking = true;
                }
                transform.position = Vector2.Lerp(transform.position, GameObject.Find("player_tile(Clone)").transform.position, Random.Range(0.001f, 0.009f));
                transform.position = new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.1f, 0.1f), -1f);
            }
        } else
        {
            transform.position = new Vector3(0, 0, 0);
        }
        transform.localScale =
            new Vector3(-Mathf.Sign(GameObject.Find("player_tile(Clone)").transform.position.x - transform.position.x) * 6,
            transform.localScale.y, transform.localScale.z);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackPresent)
        {
            if (collision.gameObject.GetComponent<Tile>() != null)
            {
                Tile otherTile = collision.gameObject.GetComponent<Tile>();
                otherTile.takeDamage(this, 1);
            }
        }
    }
}
