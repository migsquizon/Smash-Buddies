using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPicker : MonoBehaviour
{
    bool showPicker = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePicker()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Deactivate TowerPicker");
        }
        else
        {
            this.gameObject.SetActive(true);
            Debug.Log("Activate TowerPicker");
        }
        //showPicker = true;
    }
}
