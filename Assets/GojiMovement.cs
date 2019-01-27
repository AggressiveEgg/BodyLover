using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class movement
{
    int MaxHp;
    public int hp;

    public float speed;
    public bool isAtive;

    public int idBox;
    public movement()
    {
        MaxHp = 6;
        hp = MaxHp;
        speed = 10.0f;
    }
}

public class GojiMovement : MonoBehaviour
{
    public Transform[] listLocate;
    public movement movement;

    [SerializeField] Coroutine currentCoro;

    void Start()
    {
        init();
    }

    public void init()
    {
        movement = new movement();
        movement.isAtive = true;
        selectBoxToGo();
    }

    void Update()
    {
        if (movement == null)
            return;
        
        if (!movement.isAtive)
            return;
        // update UI
    }

    void selectBoxToGo()
    {
        if (!movement.isAtive)
            return;
        
        int amount = listLocate.Length;
        int i = Random.Range(0, amount);
        //print("Select i : " + i);
        if (movement.idBox != i)
        {
            movement.idBox = i;
        }
        else
        {
            selectBoxToGo();
            return;
        }

        StartMove();
    }

    void StartMove()
    {
        if (currentCoro != null)
            StopCoroutine(currentCoro);
        currentCoro = StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        float NearDis = Mathf.Infinity;
        yield return new WaitForSeconds(0);
        while(true)
        {
            NearDis = Vector2.Distance(this.transform.position,listLocate[movement.idBox].position);
            //print("Near : " + NearDis);
            if(NearDis <= 1.0f)
            {
                break;
            }
            else
            {
                if(listLocate[movement.idBox] != null)
                    this.transform.position = Vector3.Lerp(this.transform.position,listLocate[movement.idBox].position,Time.deltaTime * NearDis * movement.speed);    
            }
            yield return null;
        }
        float sec = Random.Range(4, 8);
        yield  return new WaitForSeconds(sec);
        selectBoxToGo();
    }

    public void StunGoji(int dmg)
    {
        if (!movement.isAtive)
            return;
        movement.isAtive = false;
        movement.hp -= dmg;
        if (movement.hp <= 0)
        {
            OnEnd();
        }
        else
        {
            if (currentCoro != null)
                StopCoroutine(currentCoro);
            currentCoro = StartCoroutine(Stuning());
        }
    }

    IEnumerator Stuning()
    {
        print("Goji has Stun");  
        vfxManager.instance.PlayVFX("Stun", this.transform.position, 3.0f);
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(3);
        this.GetComponent<Rigidbody>().useGravity = true;
        movement.isAtive = true;
        selectBoxToGo();
    }

    void OnEnd()
    {
        movement.isAtive = false;
        if(currentCoro != null)
            StopCoroutine(currentCoro);
        currentCoro = StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(3);
    }

}
