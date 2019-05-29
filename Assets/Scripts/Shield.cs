using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float totalShieldTime;
    public bool active = false;

    private float remainingShieldTime;

    private void Start()
    {
        remainingShieldTime = totalShieldTime;
    }

    // Update is called once per frame
    private void Update()
    {
        GetComponent<MeshRenderer>().enabled = active;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            remainingShieldTime = Mathf.Max(remainingShieldTime - Time.deltaTime, 0.0f);
            GameManager.Instance.shieldPerc = Mathf.Round((remainingShieldTime / totalShieldTime) * 100);
        }

        if (remainingShieldTime <= 0.0f)
        {
            active = false;
        }
    }
}
