using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public int damage = 20;                 //El daño mínimo
    public float velocity = 2;                          //Velocidad mínima, la velocidad se reduce a uno si pisa brea
    public int health = 500;                  //La salud mínima de los monstruos es 500, luego 1000, 1500 y 2000
    //[SerializeField] int range = 0;                   //Cuando el rango es 0 es porque es de melee
    [SerializeField] float reachDistance = 0.1f;        //La distancia máxima a la que considera un objetivo alcanzado 

    [SerializeField] Path path;                             //La ruta actual que tiene asignada el monstruo
    public bool moving = true;                               //Si está moviendose

    [SerializeField] PathManager pathManager;
    [SerializeField] bool debug = false;

    [SerializeField] float cooldown = 3f;
    
    public bool caminoObstruido;
    public Destructible destructible;
    float originalVelocity;
    float lastAttackTime;
    AudioSource audioData;

    private int actualWaypoint = 0;                         //Hacia que waypoint de la ruta actual se dirige

	// Use this for initialization
	void Start () {
        lastAttackTime -= cooldown;
        originalVelocity = velocity;
        audioData = gameObject.GetComponent<AudioSource>();
        
        if (pathManager != null)
        {
            path = pathManager.GetNearestPath(transform.position);
            if(debug) Debug.Log("El monstruo " + gameObject.name + " seguirá la ruta " + path.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (caminoObstruido && Time.time >= (lastAttackTime + cooldown))
        {
            lastAttackTime = Time.time;
            destructible.applyDamage(damage);
        }
        else if (path != null) MoveThroughPath();                //Si tiene algún path se mueve a través de este
	}

    void MoveThroughPath()
    {
        if (moving)                                         //Si continua moviendose
        {
            if (Vector3.Distance(transform.position, path.actualPath[actualWaypoint]) < reachDistance)  //Si alcanza el waypoint pasa al siguiente
            {
                actualWaypoint++;
                if (actualWaypoint > path.actualPath.Count-1)   //Si ha alcanzado el waypoint final deja de moverse
                {
                    moving = false;
                    GameManager.instance.RegisterFinishedEnemy(this);
                    return;
                }
            }
            transform.position += Vector3.Normalize(
                path.actualPath[actualWaypoint] - transform.position) * velocity * Time.deltaTime;  //Se mueve hacia el waypoint
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Defense")
        {
            velocity = 0;
            destructible = collision.gameObject.GetComponent<Destructible>();
            caminoObstruido = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Defense")
        {
            velocity = originalVelocity;
            caminoObstruido = false;
        }
    }

    public void applyDamage(int damage)
    {
        this.health -= damage;
        audioData.Play(0);
        if (health <= 0) {
            //audioData.Play(0);
            pathManager.playDeath();
            GameManager.instance.totalMonster--;
            Destroy(gameObject);
        }   
    }
}
