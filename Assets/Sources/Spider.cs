using UnityEngine;
using System.Collections;

public enum SpiderState { None, Idle, Move, Attack, Damage, Dead, Max};

public class Spider : MonoBehaviour 
{
    public float sinkSpeed = 0.3f;

    const float idleTime = 2f;
    const float attackRange = 3f;
    const float attackTime = 3f;

    GameObject player = null;
    PlayerState playerState = null;
    Animation ani = null;
    SpiderState state = SpiderState.Idle;
    float interval = 0f;
    int hp = 5;

	void Start () 
    {
        player = GameObject.Find("Player");
        playerState = player.GetComponent<PlayerState>();
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
            playerState.Damage();
            ani.Play("Attack");
            ani.PlayQueued("Idle");
        }

        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        if(dist > attackRange)
        {
            state = SpiderState.Idle;
        }
    }

    void Damage()
    {
        interval = 0f;

        if (--hp <= 0)
        {
            state = SpiderState.Dead;
            return;
        }

        ani.Play("Damage");
        ani.PlayQueued("Idle");
        state = SpiderState.Idle;
    }

    void Dead()
    {
        if(state == SpiderState.None)
        {
            return;
        }

        ani.Play("Death");
        state = SpiderState.None;

        StartCoroutine(DeadProcess());
        //Destroy(this.gameObject);
    }

    IEnumerator DeadProcess()
    {
        while(this.transform.position.y > -2)
        {
            Vector3 temp = this.transform.position;
            temp.y -= sinkSpeed * Time.deltaTime;
            this.transform.position = temp;
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if(state == SpiderState.Dead || state == SpiderState.None)
        {
            return;
        }

        int layerIndex = other.gameObject.layer;

        if (layerIndex == LayerMask.NameToLayer("Ball"))
        {
            state = SpiderState.Damage;
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

            case SpiderState.Damage:
                Damage();
                break;

            case SpiderState.Dead:
                Dead();
                break;

            default:
                break;
        }
	}
}
