using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Next : MonoBehaviour
{
    bool finished=false;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Button>().interactable==false)
            myAnim.enabled=false;
        else{
            if(!finished)
                myAnim.enabled=true;
        }
    }

    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void DoneAnim(){
        finished=true;
        myAnim.enabled=false;
    }
    
}
