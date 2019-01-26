using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BoxInfo
{
    int MaxHp;
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
    [SerializeField]BoxInfo boxInfo;

	void Start ()
    {
    //    boxInfo = new BoxInfo();
	}

    void BoxHit(int dmg)
    {
        if (!boxInfo.Active)
            return;
        
        boxInfo.hp -= dmg;

        if(boxInfo.hp <= 0)
        {
            OnShow(false);
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
            
            refresh();
        }
       // setMaterials(boxInfo.Active);
        this.gameObject.GetComponent<Collider>().enabled = boxInfo.Active;
    }

    void setMaterials(bool on)
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        Material material = renderer.material;
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

        renderer.material.SetColor("_Color", col);
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
                && (this.gameObject.transform.localPosition.x > collision.gameObject.transform.position.x - this.gameObject.GetComponent<Collider>().bounds.size.x / 2)
                && (this.gameObject.transform.localPosition.x < collision.gameObject.transform.position.x + this.gameObject.GetComponent<Collider>().bounds.size.x / 2))
            {
                //print("scale box : " + this.gameObject.GetComponent<Collider>().bounds.size);
                //print("pos box : " + this.gameObject.transform.localPosition);
                //print("pos Player : " + collision.gameObject.transform.position);
                BoxHit(1);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
