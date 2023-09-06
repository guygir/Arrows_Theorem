using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Error : MonoBehaviour
{
    public Text errorText;
    public string errorMessage;
    
    // Start is called before the first frame update
    void Start()
    {
        errorText.GetComponent<UnityEngine.UI.Text>().text = errorMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetError(){
        GetComponent<Animator>().enabled=true;
    }

    public void SetNoError(){
        GetComponent<Animator>().enabled=false;
    }
}
