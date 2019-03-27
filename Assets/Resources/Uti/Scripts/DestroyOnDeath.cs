using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            Destroy(this.gameObject);
        } else if (SceneManager.GetActiveScene().name == "PlayScene" && GameObject.Find("player_tile(Clone)").GetComponent<Tile>().health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
