using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.projectiles.Count > 0)
        {
            GetComponent<MeshRenderer>().enabled = true;
            Transform t = GameManager.Instance.projectiles[0].transform;
            //t.localScale.Set()
               
            transform.LookAt(t);
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
