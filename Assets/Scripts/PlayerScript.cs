using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Vector3 moveDestination;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        moveDestination = transform.position;
    }

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {   

	}
    
    public virtual void TurnUpdate()
    {

    }

}
