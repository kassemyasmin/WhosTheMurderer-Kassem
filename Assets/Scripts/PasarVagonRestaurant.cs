using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class PasarVagonRestaurant : MonoBehaviour {

    FirstPersonController fpc;

    // Use this for initialization
    void Start ()
    {
        fpc = FindObjectOfType<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        fpc.transform.position=new Vector3(240.706f, 2.213f, 182.58f);
    }

}
