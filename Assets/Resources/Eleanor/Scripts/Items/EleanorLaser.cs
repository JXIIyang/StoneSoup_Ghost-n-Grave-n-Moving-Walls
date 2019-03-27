using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EleanorLaser : Tile
{
    private float timer;
    private bool _causeDamage;

    private SpriteRenderer _renderer1;
    public SpriteRenderer _renderer2;

    public bool Move;

    public enum Direction
    {

        Vertical,
        Horizontal
    }

    public Direction Dir;
    private Vector3 _initialPos;

    private float _offset;
    private bool _hasAdjacentWall;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer1 = GetComponent<SpriteRenderer>();
        _causeDamage = true;
        _renderer1.enabled = true;
        _renderer2.enabled = true;
        timer = Random.Range(0.0f, 4.0f);
        _offset = Random.Range(0.0f, 3.14f);
        
        _initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
//        _renderer.color = new Color(1,1,1,(Mathf.Sin(6)) );


        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, 1f))
        {
            if (col.GetComponent<Tile>() != null && col.GetComponent<Tile>().hasTag(TileTags.Wall))
                _hasAdjacentWall = true;
        }

        if (!_hasAdjacentWall)
        {
            takeDamage(null, 1);
        }
        
        timer -= Time.deltaTime;
        if (timer < 1)
        {
            _renderer1.enabled = false;
            _renderer2.enabled = false;
            _causeDamage = false;
            
        }

        if (timer < 0)
        {
            timer = 4;
            _renderer1.enabled = true;
            _renderer2.enabled = true;
            _causeDamage = true;
//            _renderer.color = new Color(1,1,1,Mathf.Sin(0.1f)*0.8f);        
               
        }
        if (timer >= 1f && Move)
        {
            if (Dir == Direction.Horizontal)
                transform.position = _initialPos + Vector3.up * Mathf.Sin(Time.time + _offset);
            if (Dir == Direction.Vertical) 
                transform.position = _initialPos + Vector3.left * Mathf.Sin(Time.time + _offset);
                
        }
        
        _hasAdjacentWall = false;
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        Tile maybeTile = other.GetComponent<Tile>();
        if (maybeTile.tileName.Contains("rock") || maybeTile.tileName.Contains("bullet"))
        {
            maybeTile.takeDamage(this, 1);
        }
        if (maybeTile != null && _causeDamage && rb != null && !maybeTile.hasTag(TileTags.Weapon))
        {
            maybeTile.takeDamage(this, 1);
            rb.AddForce(Vector3.Normalize(other.transform.position - transform.position)* 50, ForceMode2D.Impulse);
            if (maybeTile.hasTag(TileTags.Wall)) {takeDamage(maybeTile,1);}
        }
       
    }
    
    
    
}
