using UnityEngine;

public class TrainAnimator : MonoBehaviour {

    float currentZ;

    [SerializeField]
    float speed=5;

    [SerializeField]
    float start = 355;

    [SerializeField]
    float end = 1400f;


    // Use this for initialization
    void Start () {
        currentZ = 355f;
	}
	
	// Update is called once per frame
	void Update () {
        currentZ += speed;
        if (currentZ >= end) 
            currentZ = start;
        this.gameObject.transform.Translate(0,0,currentZ-this.gameObject.transform.position.z );
	}
}
