using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UtiWolfHeadScript : Tile
{
    
    bool cursed = false;
    public AudioClip[] sounds;
    bool found;
    public GameObject wolfFamiliar;
    int timer = 0;

    public Shader[] shaders;
    public Texture2D texture2D;
    public AudioClip drums;
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            if (!GameObject.Find("UtiCursed(Clone)"))
            {
                GameObject controller = Instantiate(new GameObject("UtiCursed"));
                DontDestroyOnLoad(controller);
                controller.AddComponent<UtiCursedController>().soundArray = sounds;
                controller.GetComponent<UtiCursedController>().shaders = shaders;
                controller.GetComponent<UtiCursedController>().texture2D = texture2D;
                controller.GetComponent<UtiCursedController>().drums = drums;
                controller.GetComponent<UtiCursedController>().ghost = ghost;

            }
            if (!found)
            {
                GameObject.Find("UtiCursed(Clone)").gameObject.GetComponent<UtiCursedController>().cursed++;
                found = true;
            }
        }
        else
        {
            if (found)
            {
                timer++;
                if (timer > 30)
                {
                    GameObject hello = Instantiate(wolfFamiliar);
                    hello.transform.position = transform.position;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
