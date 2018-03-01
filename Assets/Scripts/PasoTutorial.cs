using UnityEngine;
using System.Collections;

public class PasoTutorial : MonoBehaviour {

    [SerializeField]
    private int orden;

    [SerializeField]
    private string evento;

    [SerializeField]
    private Sprite imagen;

    [SerializeField]
    public float timeOut;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int Orden { get { return orden; } }
    public string Evento { get { return evento; } }
    public Sprite Imagen { get { return imagen; } }
}
