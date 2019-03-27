using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiSpawnGhost : MonoBehaviour
{
    public GameObject ghost;

   
    

    private void OnDestroy()
    {
        if (GetComponent<Tile>().health <= 0 && this.gameObject.name!="player_tile(Clone)")
        {
            GameObject ghost1 = Instantiate(ghost);
            ghost1.transform.position = transform.position;
        }
    }
}
