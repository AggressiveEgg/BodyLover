using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCall : MonoBehaviour {

    public void PlaySound(int index)
    {
        AnimEvent.Instance.PlaySound(index);
    }
    public void HideSelf()
    {
        gameObject.SetActive(false);
    }
    public void LoadAnimScene(string name)
    {
        AnimEvent.Instance.AnimLoadScene(name);
    }
    public void SetBG(int index)
    {
        AnimEvent.Instance.setBG(index);
    }
    public void SetBGVolume(float volume)
    {
        AnimEvent.Instance.SetBGVolume(volume);
    }
}
