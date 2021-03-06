﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiTile1Script : Tile
{
    public GameObject noisePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale =
            new Vector3(-Mathf.Sign(GameObject.Find("player_tile(Clone)").transform.position.x - transform.position.x) * 0.35f,
            transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player_tile(Clone)")
        {
            GameObject scarynoise = Instantiate(noisePrefab);
            scarynoise.GetComponent<AudioSource>().Play();
            Tile otherTile = collision.gameObject.GetComponent<Tile>();
            otherTile.takeDamage(this, 1);
            Destroy(this.gameObject);
        }
    }
}
