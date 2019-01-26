using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlood : MonoBehaviour {

    int OnWalk = 0;
    float speed = 30;
    Coroutine currentCoroutine;
    Rigidbody rd;
    bool isAction;
    GameObject modelCharacter;
	// Use this for initialization
	void Start () 
    {
        rd = this.gameObject.GetComponent<Rigidbody>();
        StartWalk();
        modelCharacter = this.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isAction)
            return;
                
        checkWayToGo();
	}

    void selectWay()
    {
        if(OnWalk == 1)
        {
            OnWalk = -1;
        }
        else if (OnWalk == -1)
        {
            OnWalk = 1;
        }
        else if(OnWalk == 0)
        {
            OnWalk = 1;
        }
    }

    void checkWayToGo()
    {
        float range = 1.0f;
        Vector3 dir = new Vector3(OnWalk,0,0);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, dir, out hit, range))
        {
            if (hit.collider.tag == "ground")
            {
                StartWalk();
            }
        }
        Debug.DrawRay(this.transform.position, dir * range, Color.red);
    }

    void StartWalk()
    {
        selectWay();
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(OnWalking(new Vector3(OnWalk,0,0)));
    }

    IEnumerator OnWalking(Vector3 dir)
    {
        while (true)
        {
            RotateDirection(dir);
            rd.velocity = dir * Time.deltaTime * speed;
            yield return null;
        }
    }

    public void Hit()
    {
        
    }

    public void Force(Vector3 dir)
    {

        //animation.RotateAttack();
        print("Force");
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(Forcing(dir));
    }

    IEnumerator Forcing(Vector3 dir)
    {
        isAction = true;
        //animation.resetAnim();
        //animation.playAnimTriiger("IsHit");
        float time = CommonConfig.HitTime;
        while (true)
        {
            if (time <= 0)
            {
                break;
            }
            else
            {
                time -= Time.deltaTime;
            }
            this.rd.velocity = (dir + Vector3.up) * 10.0f * time;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isAction = false;
        StartWalk();
        yield return null;
    }

    public void RotateDirection(Vector3 dir)
    {
        if (modelCharacter == null)
            return;

        if (dir.x > 0)
        {
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles, new Vector3(modelCharacter.transform.eulerAngles.x, 135, 0), 5 * Time.deltaTime);
        }
        else if (dir.x < 0)
        {
            modelCharacter.transform.eulerAngles = Vector3.Lerp(modelCharacter.transform.eulerAngles, new Vector3(modelCharacter.transform.eulerAngles.x, 215, 0), 5 * Time.deltaTime);
        }
    }
}
