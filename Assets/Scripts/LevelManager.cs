using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public List<Node> edges;
    public int x;
    public int y;

    public Node()
    {
        edges = new List<Node>();
    }

    public float DistanceTo(Node n)
    {
        return Vector2.Distance(new Vector2(x, y), new Vector2(n.x, n.y));
    }
}

public class LevelManager : MonoBehaviour {

    protected static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject userPlayerPrefab;
    public GameObject currentPlayer;

    public int mapSizeX;
    public int mapSizeY;

    public TileType[] tileTypes;

    public int[,] tiles;
    public Node[,] graph;

    // Use this for initialization
    void Start () {

        if (_instance == null)
        {
            _instance = this;
        }

        generateMap();
        generatePathfindingGraph();
        generateVisualMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateMap()
    {
        tiles = new int[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
        /*
        //Test for blocked tiles
        tiles[0, 0] = 2; //Player Spawn

        tiles[4, 4] = 1; //Unwalkable Tiles
        tiles[5, 4] = 1;
        tiles[6, 4] = 1;
        tiles[7, 4] = 1;
        tiles[8, 4] = 1;

        tiles[4, 5] = 1;
        tiles[4, 6] = 1;
        tiles[8, 5] = 1;
        tiles[8, 6] = 1;
        */
    }

    void generatePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                //find neighboring nodes
                if (x > 0)
                {
                    graph[x, y].edges.Add(graph[x - 1, y]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].edges.Add(graph[x + 1, y]);
                }
                if (y > 0)
                {
                    graph[x, y].edges.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].edges.Add(graph[x, y + 1]);
                }
                if (x > 0 && y > 0)
                {
                    graph[x, y].edges.Add(graph[x - 1, y - 1]);
                }
                if (x < mapSizeX - 1 && y < mapSizeY - 1)
                {
                    graph[x, y].edges.Add(graph[x + 1, y + 1]);
                }
                if (x > 0 && y < mapSizeY - 1)
                {
                    graph[x, y].edges.Add(graph[x - 1, y + 1]);
                }
                if (x < mapSizeX - 1 && y > 0)
                {
                    graph[x, y].edges.Add(graph[x + 1, y - 1]);
                }
            }
        }
    }

    void generateVisualMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisual, new Vector3(x, 0, y), Quaternion.identity);

                TileUIHandler ui = go.GetComponent<TileUIHandler>();
                ui.tileX = x;
                ui.tileY = y;
            }
        }
    }

    public float costToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];

        float cost = tt.movementCost;

        //Favor straight lines in pathfinding
        if (sourceX != targetX && sourceY != targetY)
        {
            cost += 0.001f;
        }

        return cost;
    }

}
