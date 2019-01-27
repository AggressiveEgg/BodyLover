using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour {

    static public AnimEvent Instance;
    private void Awake()
    {
        Instance = this;
    }
    public AudioSource AS;
    public AudioSource ASBG;
    public AudioClip[] sound;


   public void HideSelf()
    {
        gameObject.SetActive(false);
    }
    public void PlaySound(int index)
    {
        AS.PlayOneShot(sound[index]);
    }
    public void setBG(int index)
    {
        ASBG.clip = sound[index];
        ASBG.Play();
    }
    public void SetBGVolume(float value)
    {
        ASBG.volume = value;

    }

}
