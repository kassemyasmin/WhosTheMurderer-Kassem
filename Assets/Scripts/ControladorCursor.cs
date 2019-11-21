using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ControladorCursor : MonoBehaviour {

    [SerializeField]
    Texture cross;

    MouseLook[] mouseLook;

    bool Active;
    bool Last;

    // Use this for initialization
    void Start () {
        Active = true;
        Last = false;

        mouseLook = GetComponentsInChildren<MouseLook>();
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
            var size = Screen.width / 32;

            GUI.DrawTexture(new Rect((Screen.width - size) / 2 +size/3, (Screen.height - size) / 2 + size/3, size, size), cross);
        }
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
        Active = true;
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

    }

    public void Disable()
    {
        Active = false;
//        Cursor.lockState = CursorLockMode.Confined;
//        Cursor.visible = true;

    }

    private void EnableMouseLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableMouseLook()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
