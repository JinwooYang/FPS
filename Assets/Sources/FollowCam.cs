using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour 
{
    public Transform target;
    Vector3 offset;

	void Start () 
    {
        offset = this.transform.position - target.position;
	}
	
	void FixedUpdate () 
    {
        this.transform.position = target.position + offset;
	}
}
