using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class ControladorCamara : MonoBehaviour
{

    FirstPersonController fpc;
    Camera camara;
    ControladorCursor cursor;

    float normalFov;
    float zoomFov;
    bool zoomed = false;

    [SerializeField]
    float zoom=8;

    public int ContadorZoom { get; private set; }

    // Use this for initialization
    void Start()
    {

        fpc = FindObjectOfType<FirstPersonController>();
        //        var aux = fpc.GetComponentsInChildren<GameObject>();
        cursor = FindObjectOfType<ControladorCursor>();


        var aux = fpc.transform.Find("Main Camera");
            
            
        camara=aux.GetComponent<Camera>();
       

        normalFov = camara.fieldOfView;
        zoomFov = normalFov / zoom;

        Reset();
   }


    public void LockCamera()
    {
        fpc.enabled = false;
        cursor.Disable();
    }

    public void UnlockCamera()
    {
        fpc.enabled = true;
        cursor.Enable();
    }

    public void ZoomTogle()
    {
        if (zoomed)
            camara.fieldOfView = normalFov;
        else
        {
            camara.fieldOfView = zoomFov;
            ContadorZoom++;
        }
        zoomed = !zoomed;        
    }


    public void Reset()
    {
        ContadorZoom = 0;
    }
}
