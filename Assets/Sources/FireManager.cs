using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour 
{
    public Transform firePos = null;
    public GameObject ball = null;
    public PlayerState playerState = null;
    public float speed = 50f;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

	void Update ()
    {
        if(playerState.dead)
        {
            return;
        }

	    if(Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(ball);
            obj.transform.position = firePos.position;
            obj.GetComponent<Rigidbody>().velocity = firePos.forward * speed;
        }
	}
}
