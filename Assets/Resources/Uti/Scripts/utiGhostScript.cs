using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class utiGhostScript : Tile
{

    int timer = 0;
    int timerMax = 250;
    float speed = 0.0001f;
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
        } else
        {
            GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, timer / timerMax);
        }
        if (GameObject.Find("player_tile(Clone)") && timer>timerMax)
        {
            speed = 0.001f * GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().cursed;
            transform.position = Vector2.Lerp(transform.position, GameObject.Find("player_tile(Clone)").transform.position, Random.Range(speed, speed + 0.001f));
            transform.position = new Vector3(transform.position.x + Random.Range(-0.05f, 0.05f), transform.position.y + Random.Range(-0.05f, 0.05f), -1f);

        }
        if(SceneManager.GetActiveScene().name != "PlayScene")
        {
            Destroy(this.gameObject);
        }
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tile>() != null && timer>timerMax)
        {
            Tile otherTile = collision.gameObject.GetComponent<Tile>();
            otherTile.takeDamage(this, 1);
        }
    }
}
