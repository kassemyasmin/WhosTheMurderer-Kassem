using System.Collections.Generic;
using UnityEngine;

public abstract class AssetClickeable : MonoBehaviour {

    ControladorClickeable controladorObjetosClickeables;

    int originalLayer;
    Stack<int> layerStack = new Stack<int>();
    protected Analytics gAna;

	// Use this for initialization
	void Start () {

        controladorObjetosClickeables = FindObjectOfType<ControladorClickeable>();
        AssetStart();
        controladorObjetosClickeables.Registrar(this);
        layerStack.Push(this.gameObject.layer);
        originalLayer = this.gameObject.layer;
        gAna = FindObjectOfType<Analytics>();

    }

    protected virtual void AssetStart() { }
    protected virtual void AssetUpdate() { }


    // Update is called once per frame
    void Update () {
        AssetUpdate();
	}

    protected abstract void OnMouseDown();
    public void Clicked()
    {
        OnMouseDown();
    }

    public void Enable()
    {
        if (layerStack.Count != 0)
            this.gameObject.layer = layerStack.Pop();
        else
            this.gameObject.layer = originalLayer;
    }

    public void Disable()
    {
        layerStack.Push(this.gameObject.layer);
        this.gameObject.layer = 2; //ignorar raycast (evita on mouse down events)
    }
}
