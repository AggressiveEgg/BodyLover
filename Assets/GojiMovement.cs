using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class movement
{
    public int MaxHp;
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

    public GameObject GojiAndFriend;
    List<GameObject> friends;

    void Start()
    {
        //init();
    }

    public void init()
    {
        setMaterials(true);
        movement = new movement();
        friends = new List<GameObject>(); 
        movement.isAtive = true;
        selectBoxToGo();
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
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
        vfxManager.instance.PlayVFX("Jump", this.transform.position, 0.5f);

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
        sec -= Mathf.Abs(movement.MaxHp - movement.hp) / 2;
        yield  return new WaitForSeconds(sec);
        selectBoxToGo();
    }

    public void StunGoji(int dmg)
    {
        if (!movement.isAtive)
            return;
        movement.isAtive = false;
        movement.hp -= dmg;

        GameplayManager.instance.boxManager.resetAllBox();
        if(movement.hp == 4 || movement.hp == 2 || movement.hp == 1)
        {
            SpawnWhiteBlood();
        }

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
        GameObject vfx = vfxManager.instance.PlayVFX("Stun", this.transform.position, 3.0f);
        vfx.transform.SetParent(this.transform);
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
        CameraFollow.instance.ChangeMode(1,this.gameObject);
        GameObject vfx = vfxManager.instance.PlayVFX("BoomEnding",this.transform.position,3.0f);
        vfx.transform.SetParent(this.transform);
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        HideAll();
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1);
        vfxManager.instance.PlayVFX("Jump", this.transform.position, 0.5f);
        setMaterials(false);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(1);
        CameraFollow.instance.ChangeMode(0);
        GameplayManager.instance.gameState = GameState.End;
        GameplayManager.instance.GameStart = true;
    }

    void SpawnWhiteBlood()
    {
        GameObject newFriend = Instantiate(GojiAndFriend) as GameObject;
        newFriend.transform.position = this.transform.position - Vector3.down;
        friends.Add(newFriend);
    }

    void HideAll()
    {
        int amount = friends.Count;
        for (int i = 0; i < amount; i++)
        {
            Destroy(friends[i]);
        }
    }

    void setMaterials(bool on)
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.enabled = on;
    }
}
