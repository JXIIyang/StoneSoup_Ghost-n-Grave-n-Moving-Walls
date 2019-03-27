using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class utiGhostScript : Tile
{

    int timer = 0;
    int timerMax = 250;
    float speed = 0.0001f;
    public bool gelPresent = false;
    GameObject tempGel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        health = 10000;

        if (timer > timerMax)
        {
            timer = timerMax + 1;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, timer / timerMax);
        }
        if (SceneManager.GetActiveScene().name != "PlayScene")
        {
            Destroy(this.gameObject);
        }
        else if (GameObject.Find("player_tile(Clone)") && GameObject.Find("UtiCursed(Clone)") && timer > timerMax)
        {
            speed = 0.0022f * GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().cursed;
            
            if (!gelPresent)
            {
                transform.position = Vector2.Lerp(transform.position, GameObject.Find("player_tile(Clone)").transform.position, Random.Range(speed, speed + (speed * 1.1f)));

                
                
            } else
            {
                transform.position = Vector2.Lerp(transform.position, tempGel.transform.position + (Vector3.down), 0.001f);
            }
            transform.position = new Vector3(transform.position.x + Random.Range(-0.05f, 0.05f), transform.position.y + Random.Range(-0.05f, 0.05f), -1f);
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<Tile>() != null && timer > timerMax && collision.gameObject.transform.name != "UtiClone(Ghost)")
        {
            Tile otherTile = collision.gameObject.GetComponent<Tile>();
            otherTile.takeDamage(this, 1);
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "gel(Clone)")
        {
            gelPresent = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.transform.name == "gel(Clone)")
        {
            gelPresent = true;
            tempGel = other.gameObject;
        }
    }
}
