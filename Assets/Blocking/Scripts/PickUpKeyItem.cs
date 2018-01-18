using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeyItem : MonoBehaviour {



    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            //Debug.Log(hit.transform.gameObject.name);
            GameManager.instance.addCollectedItem(hit.transform.gameObject);
        }
        
    }
}
