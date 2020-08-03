using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{


    public int totalGold { get; set; }

    public LevelManager levelManager;


    public Dictionary<string,int> roninValues { get; set; } = new Dictionary<string, int>
    {
        ["towerspeed"] = (int)TowerSpeed.tier1,
        ["towerdamage"] = (int)TowerDamage.tier1,
        ["healthremaining"] = 3
    };

    public Dictionary<string, int> sageValues { get; set; } = new Dictionary<string, int>
    {
        ["towerspeed"] = (int)TowerSpeed.tier1,
        ["towerdamage"] = (int)TowerDamage.tier1,
        ["healthremaining"] = 3
    };

    public Dictionary<string, int> servoValues { get; set; } = new Dictionary<string, int>
    {
        ["towerspeed"] = (int)TowerSpeed.tier1,
        ["towerdamage"] = (int)TowerDamage.tier1,
        ["healthremaining"] = 3
    };

    public Dictionary<string, int> tankValues { get; set; } = new Dictionary<string, int>
    {
        ["towerspeed"] = (int)TowerSpeed.tier1,
        ["towerdamage"] = (int)TowerDamage.tier1,
        ["healthremaining"] = 3
    };







    private static PersistentManager _instance;

    public static PersistentManager Instance
    {
        get
        {
            return _instance;
        }
    }


    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check for number of enemies here, then change scene using level manager
    }
}


public enum TowerDamage
{
    tier1 = 10,
    tier2 = 30,
    tier3 = 50
}


public enum TowerSpeed
{
    tier1 = 10,
    tier2 = 30,
    tier3 = 50
}