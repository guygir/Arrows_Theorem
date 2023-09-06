using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public Dialog dialog;
    public Arrow arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialog(){
        FindObjectOfType<DialogCtrl>().StartDialog(dialog);
    }

    public void disableMe(){
        arrow.SetReady();
        this.gameObject.SetActive(false);
    }

}
