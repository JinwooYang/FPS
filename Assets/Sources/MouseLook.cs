using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour 
{
    public float sensitivity = 500f;

    float rotationX = 0f;
    float rotationY = 0f;

    void Start () 
    {
        	
	}
	
	void Update () 
    {
        float dirX = Input.GetAxis("Mouse X");
        float dirY = Input.GetAxis("Mouse Y");

        rotationX += -dirY * sensitivity * Time.deltaTime;
        rotationY += dirX * sensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        this.transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);
	}
}
