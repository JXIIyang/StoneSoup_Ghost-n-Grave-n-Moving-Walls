using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiTraderScript : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("UtiCursed(Clone)"))
        {
            if (GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().cursed < 3)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

        /*
        if (GameObject.Find("UtiCurseTrader(Clone)"))
        {
            Destroy(this.gameObject);
        }*/
    }

    public override void pickUp(Tile tilePickingUsUp)
    {
        if (GameObject.Find("UtiCursed(Clone)"))
        {
            if (GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().cursed>0)
            {
                GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().ReduceCursed(1);
                Destroy(GameObject.Find("UtiWolfFamiliar(Clone)").gameObject);
                Destroy(this.gameObject);
                GameObject.Find("player_tile(Clone)").GetComponent<Tile>().health += 3;
            }
        }
    }
}
