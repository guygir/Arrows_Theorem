using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candidate : MonoBehaviour
{
    public Transform transform;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material GetMaterial(){
        GameObject blob=this.transform.GetChild(0).gameObject;
        GameObject head=blob.transform.GetChild(0).gameObject;
        return head.GetComponent<MeshRenderer>().material;
    }
}
