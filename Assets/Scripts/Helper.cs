using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Helper : MonoBehaviour
{
    
    public Sprite off,on;
    [HideInInspector]
    public bool toggle;
    public bool needPainting=false;
    
    // Start is called before the first frame update
    void Start()
    {
        toggle=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(){
        if(toggle){
            toggle=false;
            GetComponent<Image>().sprite=off;
            needPainting=true;
            
        }
        else{
            toggle=true;
            GetComponent<Image>().sprite=on;
        }
    }
}
