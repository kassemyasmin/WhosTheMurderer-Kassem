using UnityEngine;
using System.Collections.Generic;

public class ControladorClickeable : MonoBehaviour {

    private readonly IList<AssetClickeable> assets = new List<AssetClickeable>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Registrar(AssetClickeable _asset)
    {
        assets.Add(_asset);
    }

    public void EnableAll()
    {
        foreach (var a in assets)
            a.Enable();
    }

    public void DisableAll()
    {
        foreach (var a in assets)
            a.Disable();
    }
}
