using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilCatapulta : Proyectil 
{
    //[SerializeField] Vector3 target;
    [SerializeField] float areaImpacto = 2;
    [SerializeField] bool debug = false;
    //AudioSource audioData;
    public void setDestino(Vector3 destino)
    {
        this.destino = destino;
    }

     void Start()
    {
        //this.destino = target;
        //audioData = gameObject.GetComponent<AudioSource>();
    }

     protected override void updateDestino()
    {
        if (Vector3.Distance(gameObject.transform.position,this.destino) < 0.5) 
        {
            //audioData.Play(0);
            Vector2 vec2Pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            RaycastHit2D[] hits = Physics2D.CircleCastAll(vec2Pos, this.areaImpacto, Vector2.right, 0, LayerMask.GetMask("Monster"));
            if(debug) Debug.Log("hits de circle cast " + hits.Length);
            foreach (RaycastHit2D i in hits)
            {
                
                if (i.collider.tag == "Monster")
                {
                    if(debug) Debug.Log(i.collider.name);
                    Monster actual = i.collider.gameObject.GetComponent<Monster>();
                    if (actual != null) actual.applyDamage(this.damage);
                }
            }
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(gameObject.transform.position, areaImpacto);
    }
}