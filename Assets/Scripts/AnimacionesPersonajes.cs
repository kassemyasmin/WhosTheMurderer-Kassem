using UnityEngine;
using System.Collections;

public class AnimacionesPersonajes : MonoBehaviour {

   [SerializeField]
   string Animacion1="";

    [SerializeField]
    string Animacion2="";

    [SerializeField]
    string Animacion3="";

    // Use this for initialization
    void Start () {

        IniciarAnimaciones();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IniciarAnimaciones()
    {
        var animation = this.GetComponent<Animation>();
        if (!(animation[Animacion1] == null))
        {
            animation.Play(Animacion1);
        }
        if (!(animation[Animacion2] == null))
        {
            animation[Animacion2].layer = 1;
            animation.Play(Animacion2);
        }
        if (!(animation[Animacion3] == null))
        {
            animation[Animacion3].layer = 2;
            animation.Play(Animacion3);
        }
    }
}
