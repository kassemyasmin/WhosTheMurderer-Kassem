using UnityEngine;

public class ObjetoInteractuable : Objeto {

    public void IniciarAnimacion() {

        var animation = this.GetComponent<Animation>();
        animation.Play();

    }

	void OnMouseDown()
	{

		IniciarAnimacion ();
	}

   /* public void PararAnimacion() {
        var animation = this.GetComponentInChildren<Animation>();
        animation.Stop();
   }*/
}
