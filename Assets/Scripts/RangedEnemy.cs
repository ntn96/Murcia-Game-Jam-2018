using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float attackRange = 0.3f;
    //[SerializeField] private float health = 0f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int damage = 20;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector3 shootPosition;
    //[SerializeField] private float velocity = 0f;

    [SerializeField] private bool debug = false;
    AudioSource audioData;
    float timeAttack;

    private float originalAttackSpeed;
    private GameObject target;
    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();

    void Start()
    {
        CircleCollider2D sc = gameObject.GetComponent<CircleCollider2D>();
        sc.radius = attackRange;
        audioData = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target == null && EnemyList.Count != 0)
        {
            nextTarget();
        }
        if (target != null && (timeAttack + attackSpeed) <= Time.time)
        {
            Attack();
            timeAttack = Time.time;
        } 
    }

    void nextTarget()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i] == null) EnemyList.RemoveAt(i);
            else
            {
                target = EnemyList[i];
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            EnemyList.Add(col.gameObject);
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
        //sonido disparo
        audioData.Play(0);
        if (target != null)
        {
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.localPosition = transform.TransformPoint(shootPosition);
            ProyectilArquero pr = newProjectile.GetComponent<ProyectilArquero>();
            pr.setTarget(target);
            pr.setDamage(damage);
        }
    }
    
    public void applyShock(float stuntSeconds)
    {
        StartCoroutine(Stunt(stuntSeconds));
    }

    IEnumerator Stunt(float stuntSeconds)
    {
        originalAttackSpeed = attackSpeed;
        yield return new WaitForSeconds(stuntSeconds);
        attackSpeed = originalAttackSpeed;
    }

    void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, transform.localScale.x * attackRange);
            Gizmos.DrawWireSphere(transform.TransformPoint(shootPosition), 1f);
        }
    }
}