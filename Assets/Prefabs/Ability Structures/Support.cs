using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AOEPrefab;
    public int aoeSize = 0;

    public int aoeSlow = 5;

    public int aoeheal = 0;


    public int aoedamage = 5;
    bool effect = false;
    public float dur =5;
    void Awake()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        GameObject AOE = Instantiate(AOEPrefab, transform.position, transform.rotation);
        AOE.GetComponent<AoeEffect>().aoeSlow = aoeSlow;
        AOE.GetComponent<AoeEffect>().scale = aoeSize;
        AOE.GetComponent<AoeEffect>().aoeheal = aoeheal;
        AOE.GetComponent<AoeEffect>().aoedamage = aoedamage;
        Destroy(gameObject,dur);
    }
}

// Update is called once per frame
/*{
 GameObject newObject = Instantiate(prefab) as GameObject;
 YourComponent yourObject = newObject.GetComponent<YourComponent>();
 //do additional initialization steps here
 return yourObject;
}*/


