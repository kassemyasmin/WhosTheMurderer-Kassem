using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ControladorCursor : MonoBehaviour {

    [SerializeField]
    Texture cross;
    [SerializeField]
    Texture hand;

    MouseLook[] mouseLook;
    Rect rect;

    bool Active;
    bool Last;

    // Use this for initialization
    void Start () {
        Active = true;
        Last = false;

        mouseLook = GetComponentsInChildren<MouseLook>();

        var size = Screen.width / 32;
        rect = new Rect((Screen.width - size) / 2 +size/3, (Screen.height - size) / 2 + size/3, size, size);
    }
	
    
	// Update is called once per frame
	void Update () {

        if (Last != Active)
        {
            if (Active)
                EnableMouseLook();
            else
                DisableMouseLook();

            Last = Active;
        }

    }

    void OnGUI()
    {
        if (Active)
        {
            GUI.DrawTexture(rect, UseHand() ? hand : cross);
        }
    }

    private bool UseHand()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(rect.center.x, rect.center.y));
        return Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.GetComponent<Pista>() != null;
    }

    public void Togle()
    {
        Active = !Active;
 /*       if (Active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }*/
    }

    public void Enable()
    {
        Debug.LogError("--> Enable");
        Active = true;
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

    }

    public void Disable()
    {
        Debug.LogError("--> Disable");
        Active = false;
//        Cursor.lockState = CursorLockMode.Confined;
//        Cursor.visible = true;

    }

    private void EnableMouseLook()
    {
        Debug.LogError("--> Locked");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableMouseLook()
    {
        Debug.LogError("--> Confined");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
