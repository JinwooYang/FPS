using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour 
{
    public float speed = 10f;
    public CNJoystick joystick = null;

    CharacterController charCtr = null;
    PlayerState playerState = null;

    float jumpPower = 0f;

    bool isJump = false;

	void Start () 
    {
        playerState = GetComponent<PlayerState>();
        charCtr = GetComponent<CharacterController>();
	}
	
	void Update () 
    {
        if(playerState.dead)
        {
            return;
        }

        if(Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            jumpPower = 1f;
            print("Jump");
        }

#if UNITY_ANDROID
        float dirX = joystick.GetAxis("Horizontal");
        float dirZ = joystick.GetAxis("Vertical");
#else
        joystick.SetActive(false);
        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");
#endif
        Vector3 dir = new Vector3(dirX, 0, dirZ);
        dir = Camera.main.transform.TransformDirection(dir);

        float velX = dir.x * speed * Time.deltaTime;
        float velZ = dir.z * speed * Time.deltaTime;

        jumpPower += -1f * Time.deltaTime;

        charCtr.Move(new Vector3(velX, jumpPower, velZ));

        if(charCtr.collisionFlags == CollisionFlags.Below)
        {
            isJump = false;
        }
	}
}
