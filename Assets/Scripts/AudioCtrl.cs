using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class AudioCtrl : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioCtrl instance;
    public float fadeTime=0.1f;
    public Slider volSlider;
    
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

        
        foreach (Sound s in sounds){
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;
            s.source.loop=s.loop;
            s.source.playOnAwake=false;

        }
    }
    
    void Start(){
        if(!isPlaying("ThemeSlow")){
            Play("ThemeSlow");
        }
    }

    public void Play(string name){
        Sound s=Array.Find(sounds,sound=>sound.name==name);
        if(s==null){
            return;
        }
        s.source.Play();
    }

    public bool isPlaying(string name){
        Sound s=Array.Find(sounds,sound=>sound.name==name);
        if(s==null){
            return false;
        }
        return s.source.isPlaying;
    }

    public void Stop(string name){
        Sound s=Array.Find(sounds,sound=>sound.name==name);
        if(s==null){
            return;
        }
        StartCoroutine(FadeOut(s));
    }

    IEnumerator FadeOut(Sound s){    
        float startVolume=s.source.volume;
        while(s.source.volume>0){
            s.source.volume-=startVolume*Time.deltaTime/fadeTime;
            yield return null;
        }
        s.source.Stop();
        s.source.volume=startVolume;
    }

    public void ChangeVol(){
        float newVol=volSlider.GetComponent<Slider>().value;
        Sound s=Array.Find(sounds,sound=>sound.name=="Theme");
        if(s==null){
            return;
        }
        s.source.volume=newVol;
    }

    //FindObjectOfType<AudioCtrl>().Play(XXX);
}
