using UnityEngine;
using System.Collections;

public enum SpiderState { Idle, Move, Attack, Damage, Dead, Max};

public class Spider : MonoBehaviour 
{
    const float idleTime = 2f;
    const float attackRange = 3f;
    const float attackTime = 3f;

    GameObject player = null;
    Animation ani = null;
    SpiderState state = SpiderState.Idle;
    float interval = 0f;

	void Start () 
    {
        player = GameObject.Find("Player");
        ani = GetComponent<Animation>();
        ani.Play("Idle");
	}

    void Idle()
    {
        interval += Time.deltaTime;
        if(interval > idleTime)
        {
            interval = 0f;
            state = SpiderState.Move;
        }
    }

    void Move()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        if (dist < attackRange)
        {
            interval = attackTime;
            state = SpiderState.Attack;
        }
        else
        {
            ani.Play("Run");
            Vector3 dir = player.transform.position - this.transform.position;
            dir.Normalize();

            this.transform.position += dir * 5f * Time.deltaTime;

            Quaternion to = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, to, 10f * Time.deltaTime);
        }
    }

    void Attack()
    {
        interval += Time.deltaTime;

        if (interval > attackTime)
        {
            interval = 0f;
            ani.Play("Attack");
            ani.PlayQueued("Idle");
        }

        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        if(dist > attackRange)
        {
            state = SpiderState.Idle;
        }
    }

	void Update () 
    {
	    switch(state)
        {
            case SpiderState.Idle:
                Idle();
                break;

            case SpiderState.Move:
                Move();
                break;

            case SpiderState.Attack:
                Attack();
                break;
        }
	}
}
