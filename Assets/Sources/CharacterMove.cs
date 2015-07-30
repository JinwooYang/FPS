using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour 
{
    public float speed = 10f;

    CharacterController charCtr = null;
    PlayerState playerState = null;

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

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(dirX, 0, dirZ);
        dir = Camera.main.transform.TransformDirection(dir);

        float velX = dir.x * speed * Time.deltaTime;
        float velZ = dir.z * speed * Time.deltaTime;

        charCtr.Move(new Vector3(velX, 0f, velZ));
	}
}
