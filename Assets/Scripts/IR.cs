using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IR : MonoBehaviour
{
    public Candidate[] candidates;
    public Voter[] voters;
    bool arrowPlaced=false;
    bool voterVotedTrigger=false;
    bool voterVotedTwiceTrigger=false;
    public Arrow arrow;
    public float delay;
    int[] votes;
    int[] votedForArrow;
    public Text goalText,achievedText;
    public int neededScore;
    public Button nextButton;
    public float lightningDelay=0.25f;
    int count=0;
    public bool twoCandidates=true;
    public int goalCandidate=0;
    bool readyForRoundTwo=false;
    bool finishedRoundTwo=false;
    //public bool activateRoundTwo=false; //DONT TOUCH FROM INSPECTOR
    int loser=0;
    
    
    
    void Start()
    {
        if(goalCandidate!=0){
            FindObjectOfType<Arrow>().MakeMeNotFavorite();
        }
        if(voters.Length==0){
            voterVotedTrigger=true;
            voterVotedTwiceTrigger=true;
            arrowPlaced=true;
        }
        votes=new int[candidates.Length];
        for(int j=0;j<candidates.Length;j++){
            votes[j]=0;
        }
        votedForArrow=new int[voters.Length];
        for(int j=0;j<voters.Length;j++){
            votedForArrow[j]=0;
        }
        if(twoCandidates)
            goalText.GetComponent<UnityEngine.UI.Text>().text = "Goal: "+neededScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<Helper>().toggle&&FindObjectOfType<Helper>().needPainting){
            FindObjectOfType<Helper>().needPainting=false;
            PaintVotersGray();
        }
        
        if(!arrowPlaced&&arrow.GetPlaced()){
            arrowPlaced=true;
            Voting();
        }
        if(!voterVotedTrigger&&voters[0].VotedYet()){
            voterVotedTrigger=true;
            Score();
        }
        if(!voterVotedTwiceTrigger&&voters[0].VotedTwiceYet()){
            voterVotedTwiceTrigger=true;
            Score();
        }

        if(!arrowPlaced&&FindObjectOfType<Helper>().toggle){
            for(int i=0;i<voters.Length;i++){
            float chosenDis=300f;
            float secondDis=300f;
            Candidate chosen=candidates[0];
            Candidate second=candidates[1];
            Material chosenMat=chosen.GetMaterial();
            Material secondMat=second.GetMaterial();
            for(int j=0;j<candidates.Length;j++){
                float dist=Vector3.Distance(candidates[j].transform.position,voters[i].transform.position);
                if(dist<secondDis&&dist>=chosenDis){
                    secondDis=dist;
                    second=candidates[j];
                }
                if(dist<secondDis&&dist<chosenDis){
                    secondDis=chosenDis;
                    chosenDis=dist;
                    second=chosen;
                    chosen=candidates[j];
                }
            }
            voters[i].SetCandidateMaterial(chosen.GetMaterial());
            voters[i].SetSecondCandidateMaterial(second.GetMaterial());
            voters[i].ChangeMaterial();
            }
            /*for(int i=0;i<voters.Length;i++){
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
            }*/
            

        }

        if(readyForRoundTwo&&!finishedRoundTwo){
            bool secondRoundBegin=FindObjectOfType<Arrow>().GetDoubleClicked();
            if(secondRoundBegin){
                for(int j=0;j<candidates.Length;j++){
                    votes[j]=0;
                }
                for(int i=0;i<voters.Length;i++){
                    float chosenDis=300f;
                    float secondDis=300f;
                    Candidate chosen=candidates[0];
                    Candidate second=candidates[1];
                    int chosenIndex=0;
                    int secondIndex=1;
                    Material chosenMat=chosen.GetMaterial();
                    Material secondMat=second.GetMaterial();
                    for(int j=0;j<candidates.Length;j++){
                        if (j==loser){
                            continue;
                        }
                        float dist=Vector3.Distance(candidates[j].transform.position,voters[i].transform.position);
                        if(dist<secondDis&&dist>=chosenDis){
                            secondDis=dist;
                            second=candidates[j];
                            secondMat=second.GetMaterial();
                            secondIndex=j;
                        }
                        if(dist<secondDis&&dist<chosenDis){
                            secondDis=chosenDis;
                            chosenDis=dist;
                            second=chosen;
                            chosen=candidates[j];
                            secondMat=chosenMat;
                            chosenMat=chosen.GetMaterial();
                            secondIndex=chosenIndex;
                            chosenIndex=j;
                        }
                    }
                    votes[chosenIndex]+=1;
                    voters[i].SetCandidateMaterial(chosenMat);
                    voters[i].SetSecondCandidateMaterial(secondMat);
                    //DisableAllVoters(i);
                    voters[i].VotingAnimation();
                    //StartCoroutine(Delay(i));
                    if(chosenIndex==goalCandidate){
                        votedForArrow[i]=1;
                    }
                }
                finishedRoundTwo=true;
            }
        }
    }

    void Voting(){

        for(int i=0;i<voters.Length;i++){
            float chosenDis=300f;
            float secondDis=300f;
            Candidate chosen=candidates[0];
            Candidate second=candidates[1];
            int chosenIndex=0;
            int secondIndex=1;
            Material chosenMat=chosen.GetMaterial();
            Material secondMat=second.GetMaterial();
            for(int j=0;j<candidates.Length;j++){
                float dist=Vector3.Distance(candidates[j].transform.position,voters[i].transform.position);
                if(dist<secondDis&&dist>=chosenDis){
                    secondDis=dist;
                    second=candidates[j];
                    secondMat=second.GetMaterial();
                    secondIndex=j;
                }
                if(dist<secondDis&&dist<chosenDis){
                    secondDis=chosenDis;
                    chosenDis=dist;
                    second=chosen;
                    chosen=candidates[j];
                    secondMat=chosenMat;
                    chosenMat=chosen.GetMaterial();
                    secondIndex=chosenIndex;
                    chosenIndex=j;
                }
            }
            votes[chosenIndex]+=1;
            voters[i].SetCandidateMaterial(chosenMat);
            voters[i].SetSecondCandidateMaterial(secondMat);
            //DisableAllVoters(i);
            voters[i].VotingAnimation();
            //StartCoroutine(Delay(i));
            if(chosenIndex==goalCandidate){
                votedForArrow[i]=1;
            }
        }
        
    }


    public void Score(){
        achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+count.ToString());
        StartCoroutine(LightUp());
        //achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+votes[0].ToString());
    }

    public void LoseCheck(){
        if(votes[2]<=votes[1]&&votes[2]<=votes[0]){
            loser=2;
            //SAY WHOS OUT
        }
        if(votes[1]<=votes[2]&&votes[1]<=votes[0]){
            loser=1;
        }
        if(votes[0]<=votes[1]&&votes[0]<=votes[2]){
            loser=0;
        }
        achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+count.ToString()+", Min is: "+votes[loser]);
        if(loser!=goalCandidate){
            readyForRoundTwo=true;
            count=0;
        }
    }

    public void WinCheck(){
        //we need here less points
        votes[loser]=0;
        for(int j=0;j<candidates.Length;j++){
            if(j==goalCandidate)
                continue;
            if(votes[j]>=votes[goalCandidate]){
                int max=0;
                max=Mathf.Max(votes[0],votes[1],votes[2]);
                achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+count.ToString()+", Max is: "+max);
                return;
            }
        }
        nextButton.GetComponent<Button>().interactable=true;
        FindObjectOfType<AudioCtrl>().Play("Win");
    }

    IEnumerator LightUp(){
        for(int i=0;i<votedForArrow.Length;i++){
            if (votedForArrow[i]!=0){
                count+=votedForArrow[i];
                voters[i].Lightning();
                achievedText.GetComponent<UnityEngine.UI.Text>().text =("Achieved: "+count.ToString());
                yield return new WaitForSeconds(lightningDelay);
            }
        }
        if(!readyForRoundTwo){
            LoseCheck();
        }
        else{
            WinCheck();
        }
    }

    public void PaintVotersGray(){
        for(int j=0;j<voters.Length;j++){
            voters[j].PaintGray();
        }
    }

}
