using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxPlayer : MonoBehaviour {

    PlayerController[] pos;
    [SerializeField] float MaxPos;
    public GameObject minX;
    public GameObject maxX;
    public GameObject minY;
    public GameObject maxY;
    Vector3 min;
    Vector3 max;
    float highy;
    float lowy;
    float highx;
    float lowx;
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
        highy = Mathf.NegativeInfinity;
        lowy = Mathf.Infinity;

        highx = Mathf.NegativeInfinity;
        lowx = Mathf.Infinity;

        foreach (PlayerController p in pos)
        {
            if (p.isActiveAndEnabled)
            {
                if (p.gameObject.transform.position.y > highy)
                {
                    maxY = p.gameObject;
                    highy = p.gameObject.transform.position.y;
                }

                if(p.gameObject.transform.position.x > highx)
                {
                    maxX = p.gameObject;
                    highx = p.gameObject.transform.position.x;
                }

                if (p.gameObject.transform.position.y < lowy)
                {
                    minY = p.gameObject;
                    lowy = p.gameObject.transform.position.y;
                }

                if (p.gameObject.transform.position.x < lowx)
                {
                    minX = p.gameObject;
                    lowx = p.gameObject.transform.position.x;
                }

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
        min = new Vector3(lowx,lowy,0);
        max = new Vector3(highx,highy,0);
        calpos = Vector3.Lerp(min, max, 0.5f);
        float dif = Vector3.Distance(min,max);

        if (dif > 0 && dif < MaxPos)
        {
            lastpos = dif;
        }

        transform.position = new Vector3(calpos.x,calpos.y,calpos.z-lastpos) ;
	}
}
