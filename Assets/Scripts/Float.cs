using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour
{

    public float WaterLevel = 4;
    public float FloatHeight = 2;
    public float BounceDamp = 0.05f;
    public Vector3 BuoyancyCentreOffset;

    private float m_forceFactor;
    private Vector3 m_actionPoint;
    private Vector3 m_upLift;

	// Update is called once per frame
	void Update ()
    {
        m_actionPoint = transform.position + transform.TransformDirection(BuoyancyCentreOffset);
        m_forceFactor = 1f - ((m_actionPoint.y - WaterLevel) / FloatHeight);

        if(m_forceFactor > 0f)
        {
            m_upLift = -Physics.gravity * (m_forceFactor - GetComponent<Rigidbody>().velocity.y * BounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(m_upLift, m_actionPoint);
        }
	}
}
