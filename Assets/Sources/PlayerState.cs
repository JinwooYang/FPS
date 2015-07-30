using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour 
{
    public GameObject popup = null;
    public UISlider hpBar;
    public int hpMax = 5;

    int hp;
    bool isDead = false;

    public bool dead
    {
        get
        {
            return this.isDead;
        }
    }

    public void Damage()
    {
        if (isDead)
        {
            return;
        }

        if(--hp <= 0)
        {
            popup.SetActive(true);
            isDead = true;
        }
        
        hpBar.value = (float)hp / 5f;
    }

	void Start () 
    {
        hp = hpMax;
	}
	
	void Update () 
    {
	
	}
}
