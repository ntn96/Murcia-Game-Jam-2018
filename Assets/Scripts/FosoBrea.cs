using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FosoBrea : MonoBehaviour {

    [SerializeField] float radius = 3f;
    [SerializeField] float slowdown = 2f;
    [SerializeField] bool debug = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().velocity/=slowdown;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            other.gameObject.GetComponent<Monster>().velocity *= slowdown;
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
