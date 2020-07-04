using UnityEngine;

public class SafeBox : AssetClickeable {

    bool opened = false;

    SafeBoxCanvas safeBoxCanvas;

	// Use this for initialization
	void Start () {
        safeBoxCanvas = FindObjectOfType<SafeBoxCanvas>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Open()
    {
        if (!opened)
        {
            var animation = this.GetComponent<Animation>();
            animation.Play();
        }
    }

    protected override void OnMouseDown()
    {
        if (!opened)
            safeBoxCanvas.Mostrar();
    }
}
