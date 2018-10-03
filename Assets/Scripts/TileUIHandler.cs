using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUIHandler : MonoBehaviour {
   
    public int tileX;
    public int tileY;

    private MeshRenderer meshRenderer;
    private bool isSelected;
    private Color myColor;


	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        myColor = meshRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        if (!LevelEditor.Instance.editorMode)
        {
            meshRenderer.material.color = Color.red;
            //GameManager.Instance.findBestRouteTo(tileX, tileY);
        } else
        {
            meshRenderer.material.color = Color.blue;
            //LevelManager.Instance.tileTypes[LevelEditor.Instance.currentTileType].tileVisual.GetComponent<TileUIHandler>().myColor;
            LevelEditor.Instance.hoveredTile = LevelManager.Instance.graph[tileX, tileY];
            Debug.Log("Current Hovered: " + LevelEditor.Instance.hoveredTile.x + ", " + LevelEditor.Instance.hoveredTile.y);
            
        }
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = myColor;        
    }

    private void OnMouseDown()
    {
        
    }

}
