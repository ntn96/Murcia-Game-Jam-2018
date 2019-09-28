using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public enum gameStatus
{
    menu,
    lab,
    waves,
    play,
    lose,
    win
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject[] spawnPoints;
    public List<Monster> initialMonstersList = new List<Monster>();
    public List<Monster> finishedMonsterList = new List<Monster>();
    public List<GameObject>[] initialMonster = new List<GameObject>[3];
    public int totalMonster;
    public int points;
    public bool gameStarted = false;

    private MonsterMixerManager mixerManager;
    private int selectedTypeToSpawn;
    private UiManager uiManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        initialMonster[0] = new List<GameObject>();
        initialMonster[1] = new List<GameObject>();
        initialMonster[2] = new List<GameObject>();
      
        mixerManager = GameObject.Find("Gestor de monstruos").GetComponent<MonsterMixerManager>();
        ObtainCreatedMonsters();
        
        uiManager = GameObject.Find("Main Camera/Canvas").GetComponent<UiManager>();
        uiManager.UpdateMonsterType1Qty(initialMonster[0].Count);
        uiManager.UpdateMonsterType2Qty(initialMonster[1].Count);
        uiManager.UpdateMonsterType3Qty(initialMonster[2].Count);
        uiManager.SetButtonType1Text(mixerManager.creaciones[0].name);
        uiManager.SetButtonType2Text(mixerManager.creaciones[1].name);
        uiManager.SetButtonType3Text(mixerManager.creaciones[2].name);
    }

    private void ObtainCreatedMonsters()
    {
        for (int i = 0; i < mixerManager.monsterProportions[0]; i++)
        {
            initialMonster[0].Add(mixerManager.creaciones[0]);
        }        
        for (int i = 0; i < mixerManager.monsterProportions[1]; i++)
        {
            initialMonster[1].Add(mixerManager.creaciones[1]);
        }        
        for (int i = 0; i < mixerManager.monsterProportions[2]; i++)
        {
            initialMonster[2].Add(mixerManager.creaciones[2]);
        }
        
        mixerManager.DisableAllChildren();
    }

    void Update()
    {
        SpawnOnClickMonster();
        WinCondition();
        LoseCondition();
    }

    public void RegisterFinishedEnemy(Monster monster)
    {
        finishedMonsterList.Add(monster);
    }

    public void AddCreatedMonster(int type, GameObject monster)
    {
        initialMonster[type].Add(monster);
    }

    public void SelectTypeToSpawn(int type)
    {
        selectedTypeToSpawn = type;
    }

    public void WinCondition()
    {
        foreach (Monster monster in finishedMonsterList)
        {
            if (monster == null)
            {
                return;
            }

            if (!monster.moving)
            {
                StartCoroutine(ChangeOnWin());
            }
        }
    }

    private IEnumerator ChangeOnWin()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene(4);
        Destroy(this.gameObject);
    }
    
    public void LoseCondition()
    {
        if (totalMonster == 0 && gameStarted)
        {
            SceneManager.LoadScene(3);
            Destroy(this.gameObject);
        }  
    }

    private void SpawnOnClickMonster()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
           
            if(hit.collider != null && hit.transform.gameObject.CompareTag("Spawn"))
            {
                SpawnMonster(hit.collider.gameObject);
            }
        }
    }

    private void SpawnMonster(GameObject actualSpawn)
    {
        if (initialMonster[selectedTypeToSpawn].Count == 0)
        {
            return;
        }
        
        int lastMonster = initialMonster[selectedTypeToSpawn].Count - 1;
        GameObject recentSpanwed = Instantiate(initialMonster[selectedTypeToSpawn][lastMonster], actualSpawn.transform.position, actualSpawn.transform.rotation);
        recentSpanwed.SetActive(true);
        initialMonster[selectedTypeToSpawn].RemoveAt(lastMonster);
        totalMonster++;
        initialMonstersList.Add(recentSpanwed.GetComponent<Monster>());
        //restamos uno a la cantidad de la ui de su tipo
        switch (selectedTypeToSpawn)
        {
            case 0:
                uiManager.UpdateMonsterType1Qty(initialMonster[selectedTypeToSpawn].Count);
                break;
            case 1:
                uiManager.UpdateMonsterType2Qty(initialMonster[selectedTypeToSpawn].Count);
                break;
            case 2:
                uiManager.UpdateMonsterType3Qty(initialMonster[selectedTypeToSpawn].Count);
                break;
        }

        gameStarted = true;
    }

    public void AddPoints(int points)
    {
        this.points += points;
        uiManager.UpdatePoints(this.points);
    }
}