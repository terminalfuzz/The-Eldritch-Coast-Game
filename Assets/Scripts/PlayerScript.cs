using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int hitPoints;
    public int defense;

    public int tileX;
    public int tileY;

    public List<Node> currentPath = null;

    public SpriteRenderer spriteRenderer;
    public int speed;

    /*public PlayerScript(int hitPoints, int defense, int speed, int x, int y)
    {
        this.hitPoints = hitPoints;
        this.defense = defense;
        this.speed = hitPoints;
        tileX = x;
        tileY = y;
    }
    */
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

    public void basicAttack(GameObject target)
    {

    }

}
