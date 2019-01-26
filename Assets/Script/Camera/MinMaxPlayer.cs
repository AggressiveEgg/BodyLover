using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxPlayer : MonoBehaviour {

    PlayerController[] pos;
    [SerializeField] float MaxPos;
    public GameObject min;
    public GameObject max;

    Vector3 calpos;
    float lastpos;

	void Start () 
    {

	}

    public void FindAllPlayer()
    {
        pos = GameObject.FindObjectsOfType<PlayerController>();
    }

    void Cal()
    {
        float high = Mathf.NegativeInfinity;
        float low = Mathf.Infinity;
        foreach (PlayerController p in pos)
        {
            if (p.gameObject.transform.position.y > high)
            {
                max = p.gameObject;
                high = p.gameObject.transform.position.y;
            }


           if (p.gameObject.transform.position.y < low)
            {
                min = p.gameObject;
                low = p.gameObject.transform.position.y;
            }
        }
    }

	void Update ()
    {
        if (pos == null)
        {
            if (GameplayManager.instance.gameState == GameState.Start)
            {
                print("All Player");
                FindAllPlayer();
            }
            return;
        }
        
        Cal();
        calpos = Vector3.Lerp(min.transform.position, max.transform.position, 0.5f);
        float dif = Mathf.Abs(min.transform.position.y - max.transform.position.y);

        if (dif > 0 && dif < MaxPos)
        {
            lastpos = dif;
        }

        transform.position = new Vector3(calpos.x,calpos.y,calpos.z-lastpos) ;
	}
}
