using UnityEngine;
using System.Collections;

public class LevelWobble : MonoBehaviour
{
    public float xWobbleScale = 5.0f;
    public float zWobbleScale = 4.3f;


	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {


        float x = Mathf.Sin(Time.realtimeSinceStartup) * xWobbleScale;
        float z = Mathf.Cos(Time.realtimeSinceStartup) * zWobbleScale;

        //Debug.Log(Time.realtimeSinceStartup + " : " + Mathf.Sin( Time.realtimeSinceStartup) + " : " + x);

        Quaternion xRot = Quaternion.AngleAxis(x, new Vector3(1, 0, 0));
        Quaternion zRot = Quaternion.AngleAxis(z, new Vector3(0, 0, 1));

        Quaternion rot = xRot * zRot;

        transform.rotation = rot;
    }
}
