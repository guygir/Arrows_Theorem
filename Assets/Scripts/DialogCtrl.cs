using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogCtrl : MonoBehaviour
{
    private Queue<string> sentences;
    bool started=false;
    public Text nameText;
    public Text dialogText;
    bool isDone=false;
    Color32 light=new Color32(255,255,255,255);
    Color32 dark=new Color32(130,130,130,255);
    public Image arrowImg,candidateImg;
    bool arrowTurn=false;
    public float typingDelay=0.05f;
    
    public Animator dialogAnimator;

    // Start is called before the first frame update
    void Start()
    {
        sentences=new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialog(Dialog dialog){
        if(!started){
            nameText.text=dialog.name;
            sentences.Clear();
            foreach(string sentence in dialog.sentences){
                sentences.Enqueue(sentence);
            }
            started=true;
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(!isDone&&sentences.Count==0){
            EndDialog();
            isDone=false;
            return;
        }
        if(isDone)
            return;
        string sentence=sentences.Dequeue();
        StopAllCoroutines();//for sentences one on another
        StartCoroutine(TypeSentence(sentence));
        if(arrowTurn){
            arrowImg.GetComponent<Image>().color=dark;
            candidateImg.GetComponent<Image>().color=light;
            arrowTurn=false;
        }
        else{
            arrowImg.GetComponent<Image>().color=light;
            candidateImg.GetComponent<Image>().color=dark;
            arrowTurn=true;
        }
    }

    IEnumerator TypeSentence(string sentence){
        dialogText.text="";
        FindObjectOfType<AudioCtrl>().Play("Typing");
        foreach (char letter in sentence.ToCharArray()){
            dialogText.text+=letter;
            yield return new WaitForSeconds(typingDelay);
        }
        FindObjectOfType<AudioCtrl>().Stop("Typing");

    }

    public void EndDialog(){

        dialogAnimator.SetBool("Done",true);
    }
}
