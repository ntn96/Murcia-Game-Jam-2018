using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MonsterMixerManager : MonoBehaviour {
    enum ComponentePocima { Damage = 0, Health = 1, Speed = 2 };
    [SerializeField] Image[] slots = new Image[3];
    [SerializeField] Color[] colors = new Color[3];
    [SerializeField] Color colorInicial;
    [SerializeField] int numSlotsOcupados = 0;
    [SerializeField] Button botonMezclar;
    [SerializeField] bool sonidoBurbujaActivo = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject monstruo;
    [Space]
    public GameObject[] creaciones = new GameObject[3];
    [SerializeField] ComponentePocima[] partesPocima = new ComponentePocima[3];
    [SerializeField] int tiposPorCrear = 3;
    [SerializeField] int maxNumMonsters = 20;
    [SerializeField] int actualMonsterNum = 0;
    public int[] monsterProportions = new int[3];
    [SerializeField] Text[] proporcionesIndicadores = new Text[3];
    int monstruoActual = 0;

    enum ZombieNombres { Explosivo = 3, Resistente = 30, Atleta = 300, Maton = 12, Potente = 102, Musculado = 21, Fitness = 120,
        Karateka = 201, Maratoniano = 210, Normie = 111 }

    public static MonsterMixerManager instance = null;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddPotion(int potion)
    {
        if (numSlotsOcupados < 3)
        {
            if (!sonidoBurbujaActivo)
            {
                sonidoBurbujaActivo = true;
                audioSource.Play();
            }
            slots[numSlotsOcupados].color = colors[potion];
            partesPocima[numSlotsOcupados] = (ComponentePocima) potion;
            numSlotsOcupados++;
            audioSource.pitch += 0.5f;
        }
        if (numSlotsOcupados == 3) botonMezclar.gameObject.SetActive(true);
    }

    public void clearPotion()
    {
        numSlotsOcupados = 0;
        foreach (Image i in slots) i.color = colorInicial;
        botonMezclar.gameObject.SetActive(false);
        sonidoBurbujaActivo = false;
        audioSource.pitch = 0.5f;
        audioSource.Stop();
    }

    public void crearMonstruo()
    {
        if (tiposPorCrear > 0)
        {
            GameObject creacion = GameObject.Instantiate(monstruo);
            Monster creacionMons = creacion.GetComponent<Monster>();
            int firstHealth = creacionMons.health;
            int firstDamage = creacionMons.damage;
            int zombieCode = 0;
            foreach (ComponentePocima cp in partesPocima)
            {
                switch (cp)
                {
                    case ComponentePocima.Damage:
                        creacionMons.damage += firstDamage;
                        zombieCode += 1;
                        break;
                    case ComponentePocima.Health:
                        creacionMons.health += firstHealth;
                        zombieCode += 10;
                        break;
                    case ComponentePocima.Speed:
                        zombieCode += 100;
                        creacionMons.velocity += 1;
                        break;
                }
            }
            creacion.name = "Zombie " + (ZombieNombres)zombieCode;
            creacion.transform.parent = this.gameObject.transform;
            tiposPorCrear--;
            creaciones[monstruoActual] = creacion;
            monstruoActual++;
            clearPotion();
        } 
    }

    public void addMonster(int index)
    {
        if(actualMonsterNum < maxNumMonsters && tiposPorCrear == 0)
        {
            monsterProportions[index]++;
            proporcionesIndicadores[index].text = monsterProportions[index].ToString();
            actualMonsterNum++;
        }
    }

    public void restarMonster(int index)
    {
        if (actualMonsterNum > 0 && monsterProportions[index]>0 && tiposPorCrear == 0)
        {
            monsterProportions[index]--;
            proporcionesIndicadores[index].text = monsterProportions[index].ToString();
            actualMonsterNum--;
        }
    }

    public void changeToLevel()
    {
        if (tiposPorCrear == 0)
        {
            audioSource.Stop();
            SceneManager.LoadScene(2);
        }
    }
    
    public void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActiveRecursively(false);
        }
    }
}
