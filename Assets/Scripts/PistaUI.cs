using UnityEngine;
using UnityEngine.UI;

public class PistaUI : ControladorCanvas {

	AudioSource infoHablada;
    ListaInventarioUI inventario;

    protected override void CanvasStart()
    {
        inventario = FindObjectOfType<ListaInventarioUI>();
        base.CanvasStart();
    }


    public void ShowPista(Pista _pista)
    {
        bool encontrada;
        if (_pista.DependeDePista == "")
            encontrada = true;
        else
        {
            encontrada = false;
            foreach (var p in inventario.Pistas)
                if (p.name == _pista.DependeDePista)
                {
                    encontrada = true;
                    break;
                }
        }
        if (encontrada)
        {
            var texto = this.GetComponentInChildren<Text>();
            texto.text = _pista.Descripcion;

            var imagenes = this.GetComponentsInChildren<Image>();
            Image imagen = null;

            for (int c = 0; c < imagenes.GetLength(0); c++)
                if (imagenes[c].name == "ImagePista")
                    imagen = imagenes[c];

            imagen.sprite = _pista.Imagen;

            Mostrar();
            infoHablada = _pista.PistaHablada;
            PistaEncontradaSound();
        }
    }

    public override void Ocultar()
    {
        base.Ocultar();
		if(infoHablada !=null)
			infoHablada.Stop();
    }

    private void PistaEncontradaSound() {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
		infoHablada.Play (44100);

    }
}
