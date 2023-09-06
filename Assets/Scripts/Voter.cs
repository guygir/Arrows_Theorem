using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voter : MonoBehaviour
{
    public Transform transform;
    Animator myAnim;
    Material candidateMaterial;
    bool voted=false;
    bool votedTwice=false;
    GameObject lightning,redLightning;
    public Material grayMat;
    Material secondCandidateMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim=this.GetComponent<Animator>();
        lightning=this.gameObject.transform.GetChild(1).gameObject;
        redLightning=this.gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMaterial(){
        GameObject blob=this.transform.GetChild(0).gameObject;
        GameObject head=blob.transform.GetChild(0).gameObject;
        GameObject body=blob.transform.GetChild(1).gameObject;
        head.GetComponent<MeshRenderer>().material=candidateMaterial;
        body.GetComponent<MeshRenderer>().material=candidateMaterial;
        if(FindObjectOfType<Arrow>().GetPlaced())
            voted=true;
        if(FindObjectOfType<Arrow>().GetDoubleClicked()){
            votedTwice=true;
        }
        if(!FindObjectOfType<Arrow>().OneColorVote())
            head.GetComponent<MeshRenderer>().material=secondCandidateMaterial;
    }

    public void PaintGray(){
        GameObject blob=this.transform.GetChild(0).gameObject;
        GameObject head=blob.transform.GetChild(0).gameObject;
        GameObject body=blob.transform.GetChild(1).gameObject;
        head.GetComponent<MeshRenderer>().material=grayMat;
        body.GetComponent<MeshRenderer>().material=grayMat;
    }

    public bool VotedYet(){
        return voted;
    }

    public bool VotedTwiceYet(){
        return votedTwice;
    }

    public void VotingAnimation(){
        myAnim.SetTrigger("Vote");
    }

    public void DisableAnim(){
        myAnim.enabled=false;
    }

    public void EnableAnim(){
        myAnim.ResetTrigger("Vote");
        myAnim.enabled=true;
    }


    public void SetCandidateMaterial(Material newMaterial){
        this.candidateMaterial=newMaterial;
    }

    public void SetSecondCandidateMaterial(Material newMaterial){
        this.secondCandidateMaterial=newMaterial;
    }

    public void Lightning(){
        if(FindObjectOfType<Arrow>().returnImFavorite()){
            lightning.SetActive(true);
        }
        else{
            redLightning.SetActive(true);
        }
        FindObjectOfType<AudioCtrl>().Play("Light");
    }
}
