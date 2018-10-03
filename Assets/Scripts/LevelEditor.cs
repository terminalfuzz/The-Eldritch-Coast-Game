using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {

    protected static LevelEditor _instance;

    public static LevelEditor Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject currentTileType;
    public Node hoveredTile = null;
    public bool editorMode = true;

	// Use this for initialization
	void Start () {
        if (_instance == null)
        {
            _instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeCurrentTile(int tile)
    {
        currentTileType = LevelManager.Instance.tileTypes[tile].tileVisual;
    }

    public void replaceTile()
    {

    }

    public void saveLevel()
    {

    }

}
