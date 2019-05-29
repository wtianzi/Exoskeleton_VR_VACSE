using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public Text promptText;
    public float disappearAfter = 1;
    public int penalty = 2;
    public Transform shield;

    private void FixedUpdate()
    {
        remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0.0f);
        if (promptText && remainingTime <= 0.0f)
        {
            promptText.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!shield.GetComponent<Shield>().active)
        {
            if (promptText)
            {
                promptText.enabled = true;
            }
            remainingTime = disappearAfter;
            GameManager.Instance.score -= penalty;
            Debug.Log("Ouch!");
        }
   }

    private float remainingTime;
    
}
