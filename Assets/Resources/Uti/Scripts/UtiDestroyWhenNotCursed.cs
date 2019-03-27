using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiDestroyWhenNotCursed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("UtiCursed(Clone)")==false)
        {
            Destroy(this.gameObject);
        }
    }
}
