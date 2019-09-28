using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Proyectil : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 destino;

    public void setDamage(int damage)
    {
        this.damage = damage;
    }
    //public int Damage { get { return damage; } set { damage = value; } }

    void Update()
    {
        updateDestino();
        var dir = destino - transform.localPosition;
        var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
        transform.position += Vector3.Normalize(destino - transform.position) * speed * Time.deltaTime;
    }

    protected abstract void updateDestino ();

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            Destroy(gameObject);
            
            Monster actualMonster = col.gameObject.GetComponent<Monster>();
            actualMonster.applyDamage(damage);
        }
    }
}