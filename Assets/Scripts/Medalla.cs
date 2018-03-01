using UnityEngine;
using System.Collections;

public class Medalla : MonoBehaviour {

    [SerializeField]
    string caso;

    private Persister persister;

    // Use this for initialization
    void Awake () {
        string show = "False";

        persister = GameObject.Find("Persister").GetComponent<Persister>();

        switch(caso)
        {
            case "Caso1":
                show = PlayerPrefs.GetString("Caso1Resuelto");
                break;
            case "Caso2":
                show = PlayerPrefs.GetString("Caso2Resuelto");
                break;
            case "Caso3":
                show = PlayerPrefs.GetString("Caso3Resuelto");
                break;
        }

        if(show == "False")
        {
            this.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
