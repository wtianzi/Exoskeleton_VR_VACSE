using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float buttonActivationTime;
    public float buttonActivationInterval;
    public float minimumReloadTime;
    public float maximumReloadTime;
    public Dictionary<int, List<Button>> buttonDict;
    public List<Projectile> projectiles;
    public List<Turret> turrets;
    public int score;
    public float shieldPerc;

    private int buttonActivationCount = 0;

    public void RegisterButton(int id, Button button) {
        if (!buttonDict.ContainsKey(id))
        {
            buttonDict.Add(id, new List<Button>());
        }

        List<Button> buttonList;
        buttonDict.TryGetValue(id, out buttonList);
        buttonList.Add(button);
        Debug.Log("Button with ID " + button.id + " added. Total buttons: " + buttonDict.Count + ". Total buttons with same ID: " + buttonList.Count);
    }

    public void ButtonActivated()
    {
        if (++buttonActivationCount == 2)
        {
            buttonActivationCount = 0;
            List<int> ids = new List<int>(buttonDict.Keys);
            int id = ids[Random.Range(0, ids.Count)];
            List<Button> buttonList;
            buttonDict.TryGetValue(id, out buttonList);
            foreach (Button b in buttonList)
            {
                b.remainingReloadTime = buttonActivationInterval;
                b.remainingGlowTime = buttonActivationTime;
                b.canGlow = true;
            }
        }
    }

    public void RegisterProjectile(Projectile projectile)
    {
        projectiles.Add(projectile);
        //Debug.Log("Projectile added. Total projectiles: " + projectiles.Count);
    }

    public void UnregisterProjectile(Projectile projectile)
    {
        if (projectiles.Remove(projectile))
        {
            //Debug.Log("Projectile removed. Total projectiles: " + projectiles.Count);
        }
        else {
            //Debug.Log("Failed to remove projectile.");
        }
    }

    public void RegisterTurret(Turret turret)
    {
        turrets.Add(turret);
        Debug.Log("Turret added. Total turrets: " + turrets.Count);
    }

    public void TurretLaunched()
    {
        Turret t = turrets[Random.Range(0, turrets.Count)];
        t.totalReloadTime = t.remainingReloadTime = Random.Range(minimumReloadTime, maximumReloadTime);
        t.canLaunch = true;
    }
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Try loading the 'Resources/GlobalSettings' game object prefab
                GameObject globalsObject = (GameObject)Resources.Load("GameManager", typeof(GameObject));
                if (globalsObject == null)
                    Debug.LogError("Failed to load the gameobject 'Resources/GameManager'");

                // Try using the prefab's GlobalSettings script component as the instance
                _instance = globalsObject.GetComponent<GameManager>();
                if (_instance == null)
                    Debug.LogError("Failed to get the GameManager component from the gameobject 'Resources/GameManager'");

                _instance.buttonDict = new Dictionary<int, List<Button>>();
                _instance.projectiles = new List<Projectile>();
                _instance.turrets = new List<Turret>();
                _instance.score = 0;
                _instance.shieldPerc = 100;
            }
            return _instance;
        }
    }

    public void Awake()
    {
        Debug.LogError("Tried to instantiate a gameobject with the 'GlobalSettings' MonoBehaviour in '" +
                        gameObject.name + "', which is not allowed.\n" +
                        "Instead, use GlobalSettings.Instance to access the GlobalSettings' members.");
    }

    private static GameManager _instance = null;
}
