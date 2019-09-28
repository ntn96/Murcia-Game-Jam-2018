using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTower : MonoBehaviour
{
    [SerializeField] private float attackRange = 0.6f;
    //[SerializeField] private float attackArea = 0.2f;
    [SerializeField] private float attackSpeed = 4f;
    [SerializeField] private float health = 2f;
    [SerializeField] private int dmg = 4;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private Vector3 shootPosition;

    [SerializeField] private bool debug = false;

    [SerializeField] private GameObject target = null;
    private float timeAttack;
    private List<GameObject> EnemyList = new List<GameObject>();
    AudioSource audioData;
    void Start()
    {
        timeAttack = -attackSpeed;
        CircleCollider2D sc = this.gameObject.GetComponent<CircleCollider2D>();
        sc.radius = this.attackRange;
        audioData = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target == null && EnemyList.Count != 0)
        {          
            target = EnemyList[0];
        }
        if (target != null && (timeAttack + this.attackSpeed) <= Time.time)
        {
            
            Attack();
            timeAttack = Time.time;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster")
        {
            EnemyList.Add(other.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            EnemyList.Remove(col.gameObject);
            if (col.gameObject == target) target = null;
        }
    }

    public void Attack()
    {       
         if (target != null) 
         {
            audioData.Play(0);
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.localPosition = transform.TransformPoint(shootPosition);
            newProjectile.GetComponent<ProyectilCatapulta>().setDestino(target.transform.position);
         }
    } 

    void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, transform.localScale.x * attackRange);
            Gizmos.DrawWireSphere(transform.TransformPoint(shootPosition), 1f);
        }
    }


}