using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plurality : MonoBehaviour
{
    public Candidate[] candidates;
    public Voter[] voters;
    bool arrowPlaced=false;
    bool voterVotedTrigger=false;
    public Arrow arrow;
    public float delay;
    int[] votes;
    bool[] votedForArrow;
    public Text goalText,achievedText;
    public int neededScore;
    public Button nextButton;
    public float lightningDelay=0.25f;
    int count=0;
    public bool twoCandidates=true;
    public int goalCandidate=0;
    public bool toggleOutAlready=true;
    


    void Start()
    {
        if(goalCandidate!=0){
            FindObjectOfType<Arrow>().MakeMeNotFavorite();
        }
        votes=new int[candidates.Length];
        for(int j=0;j<candidates.Length;j++){
            votes[j]=0;
        }
        votedForArrow=new bool[voters.Length];
        for(int j=0;j<voters.Length;j++){
            votedForArrow[j]=false;
        }
        if(twoCandidates)
            goalText.GetComponent<UnityEngine.UI.Text>().text = "Goal: "+neededScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(toggleOutAlready){
            if(!FindObjectOfType<Helper>().toggle&&FindObjectOfType<Helper>().needPainting){
                FindObjectOfType<Helper>().needPainting=false;
                PaintVotersGray();
            }
        }
        
        if(!arrowPlaced&&arrow.GetPlaced()){
            arrowPlaced=true;
            Voting();
        }
        if(!voterVotedTrigger&&voters[0].VotedYet()){
            voterVotedTrigger=true;
            Score();
        }
        
        if(toggleOutAlready){
            if(!arrowPlaced&&FindObjectOfType<Helper>().toggle){
                for(int i=0;i<voters.Length;i++){
                float minDis=100f;
                Candidate chosen=candidates[0];
                for(int j=0;j<candidates.Length;j++){
                    float dist=Vector3.Distance(candidates[j].transform.position,voters[i].transform.position);
                    if(dist<minDis){
                        minDis=dist;
                        chosen=candidates[j];
                    }
                }
                voters[i].SetCandidateMaterial(chosen.GetMaterial());
                voters[i].ChangeMaterial();
                }
            }
        }

        
        //THIS IS FOR CONSTANT PLACEMENTS COLORING:
        /*
        for(int i=0;i<voters.Length;i++){
            float minDis=100f;
            Candidate chosen=candidates[0];
            for(int j=0;j<candidates.Length;j++){
                float dist=Vector3.Distance(candidates[j].transform.position,voters[i].transform.position);
                if(dist<minDis){
                    minDis=dist;
                    chosen=candidates[j];
                }
            }
            voters[i].SetCandidateMaterial(chosen.GetMaterial());
            voters[i].ChangeMaterial();
        }
        */
        


    }

    void Voting(){

        for(int i=0;i<voters.Length;i++){
            float minDis=100f;
            Candidate chosen=candidates[0];
            int chosenIndex=0;
            Material chosenMat=chosen.GetMaterial();
            for(int j=0;j<candidates.Length;j++){
                float dist=Vector3.Distance(candidates[j].transform.position,voters[i].transform.position);
                if(dist<minDis){
                    minDis=dist;
                    chosen=candidates[j];
                    chosenMat=chosen.GetMaterial();
                    chosenIndex=j;
                }
            }
            votes[chosenIndex]++;
            voters[i].SetCandidateMaterial(chosenMat);
            //DisableAllVoters(i);
            voters[i].VotingAnimation();
            //StartCoroutine(Delay(i));
            if(chosenIndex==0){
                votedForArrow[i]=true;
            }
        }
        
    }

    public void Score(){
        achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+count.ToString());
        StartCoroutine(LightUp());
        //achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+votes[0].ToString());
    }

    public void WinCheck(){
        /*OLD
        if(votes[0]>=neededScore){
            nextButton.GetComponent<Button>().interactable=true;
            FindObjectOfType<AudioCtrl>().Play("Win");
        }*/
        for(int j=0;j<candidates.Length;j++){
            if(j==goalCandidate)
                continue;
            if(votes[j]>=votes[goalCandidate]){
                return;
            }
        }
        nextButton.GetComponent<Button>().interactable=true;
        FindObjectOfType<AudioCtrl>().Play("Win");
    }

    IEnumerator LightUp(){
        for(int i=0;i<votedForArrow.Length;i++){
            if (votedForArrow[i]==true){
                count++;
                voters[i].Lightning();
                achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+count.ToString());
                yield return new WaitForSeconds(lightningDelay);
            }
        }
        WinCheck();
    }

    public void PaintVotersGray(){
        for(int j=0;j<voters.Length;j++){
            voters[j].PaintGray();
        }
    }

    /* SLOW VOTING
    IEnumerator Delay(int i){
        yield return new WaitForSeconds(0.01f);
        EnableAllVoters(i);
    }

    void EnableAllVoters(int i){
        for (int j=0;j<voters.Length;j++){
            if(j!=i){
                voters[2].EnableAnim();
                voters[3].EnableAnim();
            }
        }
    }

    void DisableAllVoters(int i){
        for (int j=0;j<voters.Length;j++){
            if(j!=i){
                voters[2].DisableAnim();
                voters[3].DisableAnim();
            }
        }
    }
    */


}
