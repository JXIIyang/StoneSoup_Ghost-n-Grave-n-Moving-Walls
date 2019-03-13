using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Tile maybeTile = other.GetComponent<Tile>();
        if (maybeTile == null) return;
        if (maybeTile.hasTag(TileTags.Creature) && _causeDamage)
        {
            maybeTile.takeDamage(this,1);
            
            
            
            
            
            
//            if (Dir == Direction.Vertical)
//            {
//              Vector3 force = transform.position + Vector3.down;
//            }
//            if (Dir == Direction.Horizontal)
//            {
//                Vector3 force = transform.position + Vector3.right;
//            }

            maybeTile.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(other.transform.position - transform.position)* 10, ForceMode2D.Impulse);
        }
    }
}
