using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public MovingBG[] backgrounds;
    public float xLim=17.9f;
    public static Parallaxing instance;


    //DONT DESTROY ON LOAD??

    // Start is called before the first frame update
    void Awake()
    {
        if(instance==null)
            instance=this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForReset();
        
        for(int i=0;i<backgrounds.Length;i++){   
            backgrounds[i].firstTransform.localPosition=new Vector3(Mathf.Clamp(backgrounds[i].firstTransform.localPosition.x-backgrounds[i].speed*Time.deltaTime,-xLim,0),backgrounds[i].firstTransform.localPosition.y,backgrounds[i].firstTransform.localPosition.z);
            backgrounds[i].secondTransform.localPosition=new Vector3(Mathf.Clamp(backgrounds[i].secondTransform.localPosition.x-backgrounds[i].speed*Time.deltaTime,-xLim,0),backgrounds[i].secondTransform.localPosition.y,backgrounds[i].secondTransform.localPosition.z);
        }
        
    }

    public void CheckForReset(){
        for(int i=0;i<backgrounds.Length;i++){
            if(backgrounds[i].firstTransform.localPosition.x==-xLim){
                backgrounds[i].firstTransform.localPosition=new Vector3(0,backgrounds[i].firstTransform.localPosition.y,backgrounds[i].firstTransform.localPosition.z);
                backgrounds[i].secondTransform.localPosition=new Vector3(0,backgrounds[i].secondTransform.localPosition.y,backgrounds[i].secondTransform.localPosition.z);
            }
        }
    }
}
