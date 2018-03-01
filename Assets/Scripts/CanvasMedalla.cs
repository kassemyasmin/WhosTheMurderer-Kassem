using UnityEngine;
using System;

public class CanvasMedalla : MonoBehaviour {



void Start()
    {
        Activo = true;
    }

public bool Activo { get; private set; }

public void Togle()
    {
        if (!Activo)
            Mostrar();
        else
            Ocultar();
    }
	
public virtual void Mostrar()
    {
        if (!Activo)
        {
            this.gameObject.SetActive(true);
            Activo = true;
        }
    }

    public virtual void Ocultar()
    {
        if (Activo)
        {
            this.gameObject.SetActive(false);
            Activo = false;
        }
    }
	
}
