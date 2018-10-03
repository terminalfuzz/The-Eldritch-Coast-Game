using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickManager : MonoBehaviour {

    public GameObject player;

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //Set a default value of -1
            Vector3 clickPosition = -Vector3.one;

            //Cast a ray to determine click location
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
            }

            //Turns results into grid like coordinates
            clickPosition.x = Mathf.Floor(clickPosition.x);
            clickPosition.z = Mathf.Floor(clickPosition.z);

            
            Debug.Log(clickPosition);

        }

	}
}
