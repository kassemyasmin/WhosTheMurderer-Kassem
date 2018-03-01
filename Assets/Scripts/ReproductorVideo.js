#pragma strict
 
function Start () {
    (GetComponent.<Renderer>().material.mainTexture as MovieTexture).Play();
}