using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilArquero : Proyectil {
    [SerializeField] GameObject target;

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    protected override void updateDestino()
    {
        if (target == null) Destroy(gameObject);
        else this.destino = target.transform.position;
    }
}
