using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUIHandler : MonoBehaviour {
   
    public int tileX;
    public int tileY;
    public bool isOccupied = false ;


    public MeshRenderer meshRenderer;
    private bool isSelected;
    public Color myColor;

    private bool editor;

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        myColor = meshRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject gameObject in GameManager.Instance.turnOrder)
        {
            if (gameObject.GetComponent<PlayerScript>().tileX == tileX && gameObject.GetComponent<PlayerScript>().tileY == tileY)
            {
                isOccupied = true;
            }
            else
            {
                isOccupied = false;
            }
        }
	}

    private void OnMouseEnter()
    {
        try
        {
            editor = LevelEditor.Instance.editorMode;
            meshRenderer.material.color = Color.blue;
            //LevelManager.Instance.tileTypes[LevelEditor.Instance.currentTileType].tileVisual.GetComponent<TileUIHandler>().myColor;
            LevelEditor.Instance.hoveredTile = LevelManager.Instance.graph[tileX, tileY];
            Debug.Log("Current Hovered: " + LevelEditor.Instance.hoveredTile.x + ", " + LevelEditor.Instance.hoveredTile.y);
        }
        catch
        {
            meshRenderer.material.color = Color.red;
            if (LevelManager.Instance.tiles[tileX, tileY] != 1 && !LevelManager.Instance.graph[tileX, tileY].tileUI.isOccupied) {
                GameManager.Instance.findBestRouteTo(tileX, tileY);
            }
            else
            {
                GameManager.Instance.currentPlayer.GetComponent<PlayerScript>().currentPath = null;
            }
        }
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = myColor;        
    }

    private void OnMouseDown()
    {
        GameManager.Instance.moveNextTile();
    }

}
