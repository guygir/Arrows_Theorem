using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    bool isPlaced=false;
    public int offset;
    Camera cam;
    Ray ray;
    bool ready=false;
    public Candidate[] otherCandidates;
    public float placementRadius=2.25f;
    public Error errorButton;
    public bool oneColorVote=true;
    bool imFavorite=true;
    bool doubleClicked=false;
    public bool alone=false;
    
    // Start is called before the first frame update
    void Start()
    {
        cam=GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position=cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,20));
        /*ray=cam.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit)){
            Debug.Log(hit.collider.name);
        }*/
        if(isPlaced&&Input.GetMouseButtonDown(0)){
            doubleClicked=true;
        }
        if(!isPlaced){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance=0;
            //Collider coll=GameObject.Find("Plane").GetComponent<Collider>();
            Plane placingPlane=new Plane(new Vector3(0,10,0),new Vector3(1,3.5f,0));
            if (placingPlane.Raycast(ray, out distance))
            {
                Vector3 pos=ray.GetPoint(distance);
                Vector3 fixedPos=new Vector3(Mathf.Clamp(pos.x,-15,15),pos.y,Mathf.Clamp(pos.z,-15,15));
                transform.position = fixedPos;
            }
        }
        if(!isPlaced&&ready&&Input.GetMouseButtonDown(0)&&Input.mousePosition.y<800){
            if(GoodPlacement()){
                isPlaced=true;
                FindObjectOfType<AudioCtrl>().Play("Place");
                GetComponent<Animator>().SetTrigger("Place");
            }
        }
        

    }

    public bool GoodPlacement(){
        if(alone){
            return false;
        }
        for(int i=0;i<otherCandidates.Length;i++){
            if(Vector3.Distance(transform.position,otherCandidates[i].gameObject.transform.position)<placementRadius){
                errorButton.SetError();
                FindObjectOfType<AudioCtrl>().Play("Blocked");
                return false;
            }
        }
        return true;
    }

    public void SetReady(){
        ready=true;
    }

    public bool GetPlaced(){
        return isPlaced;
    }

    public bool returnImFavorite(){
        return imFavorite;
    }

    public void MakeMeNotFavorite(){
        imFavorite=false;
    }

    public bool OneColorVote(){
        return oneColorVote;
    }

    public bool GetDoubleClicked(){
        return doubleClicked;
    }


}
