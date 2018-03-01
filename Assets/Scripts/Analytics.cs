using UnityEngine;
using System.Collections;

public class Analytics : MonoBehaviour {

    [SerializeField]
    public GoogleAnalyticsV4 gv4;

    // Use this for initialization
    void Start () {

        gv4.StartSession();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
