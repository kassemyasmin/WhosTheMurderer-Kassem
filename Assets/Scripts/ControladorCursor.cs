using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ControladorCursor : MonoBehaviour
{

    [SerializeField]
    Texture cross;
    [SerializeField]
    Texture hand;

    public Texture talkSymbol;

    MouseLook[] mouseLook;
    Rect rect;

    bool handActive;
    bool talkActive;
    int Last;

    public bool canvasActivo;

    // Use this for initialization
    void Start()
    {

        handActive = true;
        talkActive = true;
        Last = 0;

        mouseLook = GetComponentsInChildren<MouseLook>();

        var size = Screen.width / 32;
        rect = new Rect((Screen.width - size) / 2 + size / 3, (Screen.height - size) / 2 + size / 3, size, size);
    }


    // Update is called once per frame
    void Update()
    {

        if (Last != 1 || Last != 2)
        {
            if(handActive)
                Last = 1;
            if (talkActive)
                Last = 2;


            if(handActive || talkActive)
                GetClickedObject();
        }

        
    }

    void OnGUI()
    {
        Texture useThis;

        if (canvasActivo)
            useThis = null;
        else
        {
            if (UseHand())
            {
                useThis = hand;
            }

            else if (UseTalkSymbol())
            {
                useThis = talkSymbol;
            }

            else
            {
                useThis = cross;
            }
        }
       

        GUI.DrawTexture(rect, useThis);
    }



    private bool UseHand()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(rect.center.x, rect.center.y));
        return Physics.Raycast(ray, out hit) && hit.collider != null && (hit.collider.GetComponent<Pista>() != null || hit.collider.GetComponent<ObjetoInteractuable>() != null || hit.collider.GetComponent<SafeBox>() != null);
    }

    private bool UseTalkSymbol()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(rect.center.x, rect.center.y));
        return Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.GetComponent<Personaje>() != null;
    }

    public void GetClickedObject()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(rect.center.x, rect.center.y));
            if(Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.GetComponent<AssetClickeable>() != null)
            {
                hit.collider.GetComponent<AssetClickeable>().Clicked();
            }
           
        }
    }

    public void Togle()
    {
        handActive = !handActive;

    }

    public void Enable()
    {
        handActive = true;
    }

    public void Disable()
    {
        handActive = false;

    }
}
