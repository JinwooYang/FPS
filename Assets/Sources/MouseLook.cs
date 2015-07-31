using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour 
{
    public float sensitivity = 500f;
    public CNJoystick joystick = null;

    float rotationX = 0f;
    float rotationY = 0f;

    PlayerState playerState = null;

    void Start () 
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
	}
	
	void Update () 
    {
        if(playerState.dead)
        {
            return;
        }

#if UNITY_ANDROID
        float dirX = joystick.GetAxis("Horizontal");
        float dirY = joystick.GetAxis("Vertical");
#else
        joystick.SetActive(false);
        float dirX = Input.GetAxis("Mouse X");
        float dirY = Input.GetAxis("Mouse Y");
#endif

        rotationX += -dirY * sensitivity * Time.deltaTime;
        rotationY += dirX * sensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        this.transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);
	}
}
