using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour 
{
    public GameObject spider;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Factory());
	}
	
	IEnumerator Factory()
    {
        while(true)
        {
            float time = Random.Range(2f, 5f);
            yield return new WaitForSeconds(time);

            GameObject obj = Instantiate(spider);
            float x = Random.Range(-10f, 10f);
            float z = Random.Range(-10f, 10f);

            obj.transform.position = new Vector3(x, 0f, z);
        }
    }
}
