using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiAppleScript : Tile
{
    int dieChance;
    // Start is called before the first frame update
    void Start()
    {
        dieChance = Random.Range(0, 100);
        if (dieChance < 80)
        {
            die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GameObject.Find("UtiCursed(Clone"))
                {
                    AddCursed();
                }
                GameObject.Find("player_tile(Clone)").GetComponent<Player>().health++;
                Destroy(this.gameObject);
            }
        }
    }
    void AddCursed()
    {
        if (GameObject.Find("UtiCursed(Clone)"))
        {

            GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().cursed += 1;
        }
    }
}
