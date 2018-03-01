using UnityEngine;

public class SafeBox : MonoBehaviour {

    bool opened = false;

    SafeBoxCanvas safeBoxCanvas;

	// Use this for initialization
	void Start () {
        safeBoxCanvas = FindObjectOfType<SafeBoxCanvas>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnMouseDown()
    {
        if (!opened)
            safeBoxCanvas.Mostrar();
    }
 
    public void Open()
    {
        if (!opened)
        {
            var animation = this.GetComponent<Animation>();
            animation.Play();
        }
    }

}
