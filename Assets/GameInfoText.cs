using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        GetComponent<Text>().text = "Score: " + GameManager.Instance.score + "\nShield: " + GameManager.Instance.shieldPerc + "%";
    }
}
