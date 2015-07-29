using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
    public GameObject particle = null;

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);

        GameObject obj = Instantiate(particle);
        obj.transform.position = this.transform.position;
    }
}
