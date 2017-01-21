using UnityEngine;
using System.Collections;

public class VFXController : MonoBehaviour {

    public AnimationCurve distortionOverTime;
    public AnimationCurve scaleOverTime;

    public Renderer rend;
	public float initTime;

    public float distortion;
    public float scale;
    
	// Use this for initialization
	void Start () 
    {
        rend = GetComponent<Renderer>();
		initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
		float timeSinceInit = Time.time - initTime;
		scale = scaleOverTime.Evaluate(timeSinceInit); 
        transform.localScale = new Vector3(scale,scale,scale);

        //do not use values over 1 in the curve edeitor
		distortion = 128*distortionOverTime.Evaluate(timeSinceInit);
        rend.material.SetFloat("_BumpAmt", distortion);
	}
}
