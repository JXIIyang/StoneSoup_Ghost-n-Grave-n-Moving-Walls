using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGlue : Tile
{
    
    public GameObject GelPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }
    
    public override void useAsItem(Tile tileUsingUs) 
    {
        
        GameObject newGel = Instantiate(GelPrefab);
        newGel.transform.position = Vector2Int.FloorToInt(transform.position) - (tileUsingUs.GetComponent<Rigidbody2D>().velocity * 10).normalized * 1.5f;
        newGel.GetComponent<Tile>().init();
        takeDamage(this, 1);

    }


    
    
}
