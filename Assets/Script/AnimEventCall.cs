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
}
