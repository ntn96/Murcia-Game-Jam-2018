using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacha : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float radio;
    //[SerializeField] private float enfiramiento;

    
    private float timer = 0f;
    private List<GameObject> EnemyList = new List<GameObject>();
    AudioSource audioData;
    private float timeSound;
    

    void Start()
    {
        //timeAttack = enfiramiento*(-1);
        audioData = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.tag == "Monster")
        {
            audioData.Play(0);
            EnemyList.Add(col.gameObject);
            List<GameObject> nueva = new List<GameObject>();
            foreach (GameObject i in EnemyList)
            {
                if (i != null) nueva.Add(i);
            }
            EnemyList = nueva;
            col.GetComponent<Monster>().applyDamage(this.damage);
            //Attack();
        }
    }

     private void OnTriggerExit2D(Collider2D col)
    {
        
        if (col.tag == "Monster")
        {
            EnemyList.Remove(col.gameObject);
            
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.back, 200f * Time.deltaTime);    
        
        timer = timer - 1f;
        
    }
/* 
    void Attack ()
    {
        //Animación de las Hachas
        //Sonido del Hacha
        //EnemyList.RemoveAll(item => item == null);
        List<GameObject> nueva = new List<GameObject>();
        foreach (GameObject i in EnemyList)
        {
            if (i != null) nueva.Add(i);
        }
        //if (nueva.Count != 0) audioData.Play(0);
     
        if (nueva.Count != 0 && timer <= 0f) {
            
            audioData.Play(0);
            
            timer = 90f;
            
        }

        //EnemyList = nueva;

        foreach (GameObject i in nueva)
        {  
            Monster actual = i.GetComponent<Monster>();
            actual.applyDamage(this.damage);
        }
        


    }
    */
}