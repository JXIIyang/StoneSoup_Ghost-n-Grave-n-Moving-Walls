using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueGel : Tile
{
    public float TimeBeforeDry;
    public GameObject DriedGel;
    private float _clampSpeed;

    private GameObject _dried;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _clampSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeBeforeDry -= Time.deltaTime;
        if (TimeBeforeDry < 0)
        {
            DryOut();

        }
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider) {
        Tile maybeTile = otherCollider.GetComponent<Tile>();
        if (maybeTile.hasTag(TileTags.Creature))
        {

            maybeTile.GetComponent<Rigidbody2D>().velocity =
                new Vector2(
                    Mathf.Clamp(maybeTile.GetComponent<Rigidbody2D>().velocity.x, 0.0f, _clampSpeed),
                    Mathf.Clamp(maybeTile.GetComponent<Rigidbody2D>().velocity.y, 0.0f, _clampSpeed)
                );
        }
        Debug.Log("enter");
    }
    void OnTriggerStay2D(Collider2D otherCollider) {
        Tile maybeTile = otherCollider.GetComponent<Tile>();
        if (maybeTile.hasTag(TileTags.Creature))
        {

            maybeTile.GetComponent<Rigidbody2D>().velocity =
                new Vector2(
                    Mathf.Clamp(maybeTile.GetComponent<Rigidbody2D>().velocity.x, 0.0f, _clampSpeed),
                    Mathf.Clamp(maybeTile.GetComponent<Rigidbody2D>().velocity.y, 0.0f, _clampSpeed)
                );
        }
        Debug.Log("stay");
    }

    void DryOut()
    {
        if (_dried == null)
        {
            _dried = Instantiate(DriedGel, transform.localPosition, Quaternion.identity);
        }

        _clampSpeed = Mathf.Lerp(0.5f,10f,Time.deltaTime);
        _renderer.color = new Color(1,1,1,Mathf.Lerp(_renderer.color.a,0,Time.deltaTime));

        if (_renderer.color.a <= 0.01f)
        {           
            Destroy(gameObject);
        }
        
    }

    
}
