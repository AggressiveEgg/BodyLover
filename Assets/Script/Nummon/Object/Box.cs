using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class BoxInfo
{
    public int MaxHp;
    public int hp;
    public bool Active = true;

    public BoxInfo()
    {
        MaxHp = 1;
        hp = MaxHp;
    }

    public BoxInfo(int HP)
    {
        MaxHp = HP;
        hp = MaxHp;
    }

    public void reset()
    {
        hp = MaxHp;
    }
}

public class Box : MonoBehaviour 
{
    Coroutine coroutine;
    [SerializeField] BoxInfo boxInfo;
    [SerializeField] Material[] BoxStyle;
    [SerializeField] bool isUnBrakeable;

    void Start()
    {
        //boxInfo = new BoxInfo();
        InitBox();
    }


    void BoxHit(int dmg)
    {
        if (!boxInfo.Active)
            return;
        if (!isUnBrakeable)
            boxInfo.hp -= dmg;
        
        print(boxInfo.hp);

        if (boxInfo.hp <= 0)
        {
            OnShow(false);
        }
    }
    public void InitBox()
    {
        boxInfo.reset();

        Renderer m = GetComponent<MeshRenderer>();

        if (isUnBrakeable)
        {
            m.material = BoxStyle[0];
            return;
        }
        if (boxInfo.hp > 1)
        {
            m.material = BoxStyle[1];
        }
        else
        {
            m.material = BoxStyle[2];
        }

    }

    void OnShow(bool on)
    {
        boxInfo.Active = on;
        if (on)
        {
            
        }
        else
        {
            vfxManager.instance.PlayVFX("BoxBroken",this.transform.position,1.0f);
            refresh();
        }
        setMaterials(boxInfo.Active);
        this.gameObject.GetComponent<Collider>().enabled = boxInfo.Active;
    }

    void setMaterials(bool on)
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.enabled = on;
        /*Material material = renderer.material;
        material.SetFloat("_Mode", 3f);

        Color32 col = renderer.material.GetColor("_Color");
        if (on)
        {
            col.a = 255;
        }
        else
        {
            col.a = 0;
        }

        renderer.material.SetColor("_Color", col);*/
    }

    public void refresh()
    {
        coroutine = StartCoroutine(IRefresh());
    }

    IEnumerator IRefresh()
    {
        yield return new WaitForSeconds(5);
        Reset();
    }

    public void Reset()
    {
        boxInfo.reset();
        OnShow(true);
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y <= this.gameObject.transform.position.y 
                && (this.gameObject.transform.position.x > collision.gameObject.transform.position.x - collision.gameObject.GetComponent<Collider>().bounds.size.x / 2)
                && (this.gameObject.transform.position.x < collision.gameObject.transform.position.x + collision.gameObject.GetComponent<Collider>().bounds.size.x / 2))
            {
                //print("scale box : " + this.gameObject.GetComponent<Collider>().bounds.size);
                //print("pos box : " + this.gameObject.transform.localPosition);
                //print("pos Player : " + collision.gameObject.transform.position);
                BoxHit(1);
            }
        }
    }

    void BoxBroken()
    {
        Vector3 Dir;
        if(Random.Range(0,100) %2 == 0)
        {
            Dir = Vector3.right;
        }
        else
        {
            Dir = Vector3.left;
        }
        float range = 2.0f;

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.up, out hit, range))
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<PlayerController>().Stun(Dir);
            }
        }
        Debug.DrawRay(this.transform.position, Vector3.up * range, Color.red);
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
