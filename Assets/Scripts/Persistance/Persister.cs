using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Persister : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(!PlayerPrefs.HasKey("Caso1Resuelto"))
            PlayerPrefs.SetString("Caso1Resuelto", "False");

        if (!PlayerPrefs.HasKey("Caso2Resuelto"))
            PlayerPrefs.SetString("Caso2Resuelto", "False");

        if (!PlayerPrefs.HasKey("Caso3Resuelto"))
            PlayerPrefs.SetString("Caso3Resuelto", "False");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F12)) //Con F12 en menu se borrara todo lo que este guardado, osea se va a resetear.
            PlayerPrefs.DeleteAll();
	}


}
