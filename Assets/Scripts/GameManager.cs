using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    protected static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject userPlayerPrefab;

    List<Node> currentPath = null;

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {/*
        currentPlayer = turnOrder[currentPlayerIndex];

        LevelManager.Instance.resetTileColors();
        //Highlight Tiles
        if (currentPlayer.GetComponent<PlayerScript>().currentPath != null)
        {
            int currNode = 0;
            //Highlight path with yellow tiles
            while (currNode < currentPlayer.GetComponent<PlayerScript>().currentPath.Count - 1)
            {
                LevelManager.Instance.graph[currentPlayer.GetComponent<PlayerScript>().currentPath[currNode + 1].x, currentPlayer.GetComponent<PlayerScript>().currentPath[currNode + 1].y].tileUI.meshRenderer.material.color 
                    = Color.yellow;
                currNode++;
            }
        }   
        */
    }

    public static Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, 0, y);
    }

    public void findBestRouteTo(int x, int y)
    {
        //Tile x,y will not always be the transforms actual x,y position in the world

        TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().tileX = (int)TurnManager.Instance.currentPlayer.transform.position.x;
        TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().tileY = (int)TurnManager.Instance.currentPlayer.transform.position.z;

        Debug.Log(TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().tileX + ", " + TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().tileY);

        currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = LevelManager.Instance.graph[TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().tileX, 
            TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().tileY];
        Node target = LevelManager.Instance.graph[x, y];

        //Pathfinding via Dijkstra
        dist[source] = 0;
        prev[source] = null;

        foreach (Node vertex in LevelManager.Instance.graph)
        {
            if (vertex != source)
            {
                dist[vertex] = Mathf.Infinity;
                prev[vertex] = null;
            }

            unvisited.Add(vertex);

        }

        while (unvisited.Count > 0)
        {
            //u is the unvisited node with the smallest distance
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.edges)
            {
                float alt = dist[u] + LevelManager.Instance.costToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        
        if (dist[target] == Mathf.Infinity)
        {
            return;
        }

        currentPath = new List<Node>();
        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();
        
        if (currentPath.Count <= TurnManager.Instance.currentPlayer.GetComponent<UserPlayerScript>().speed + 1)
        {
            TurnManager.Instance.currentPlayer.GetComponent<UserPlayerScript>().currentPath = currentPath;
        }
        else
        {
            TurnManager.Instance.currentPlayer.GetComponent<UserPlayerScript>().currentPath = null;
        }
    } // Dijkstra

    public void moveNextTile()
    {
        //for (int i = 0; i < currentPlayer.GetComponent<PlayerScript>().currentPath.Count + 1; i++)
        try
        {
            int i = 0;
            while (i < TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().currentPath.Count + 1)
            {

                if (TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().currentPath.Count == 1)
                {
                    break;
                }

                TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().currentPath.RemoveAt(0);
                TurnManager.Instance.currentPlayer.transform.position = 
                    TileCoordToWorldCoord(
                        TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().currentPath[0].x, 
                        TurnManager.Instance.currentPlayer.GetComponent<PlayerScript>().currentPath[0].y
                        );
                Debug.Log("Moving");
            }
        }
        catch
        {
            Debug.Log("Unreachable Tile");
        }
    }

}
