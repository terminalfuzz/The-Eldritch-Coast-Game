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
    public GameObject currentPlayer;
    public List<GameObject> turnOrder = new List<GameObject>();

    public int currentPlayerIndex;

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
    {
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

    }

    public void nextTurn()
    {
        if (turnOrder.Count > 0)
        {
            if (currentPlayerIndex + 1 < turnOrder.Count)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 0;
            }
        }
    }

    public static Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, 0, y);
    }

    public void findBestRouteTo(int x, int y)
    {
        //Tile x,y will not always be the transforms actual x,y position in the world

        currentPlayer.GetComponent<PlayerScript>().tileX = (int)currentPlayer.transform.position.x;
        currentPlayer.GetComponent<PlayerScript>().tileY = (int)currentPlayer.transform.position.z;

        Debug.Log(currentPlayer.GetComponent<PlayerScript>().tileX + ", " + currentPlayer.GetComponent<PlayerScript>().tileY);

        currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = LevelManager.Instance.graph[currentPlayer.GetComponent<PlayerScript>().tileX, currentPlayer.GetComponent<PlayerScript>().tileY];
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
        
        if (currentPath.Count <= currentPlayer.GetComponent<UserPlayerScript>().speed)
        {
            currentPlayer.GetComponent<UserPlayerScript>().currentPath = currentPath;
        }
        else
        {
            currentPlayer.GetComponent<UserPlayerScript>().currentPath = null;
        }
    }

    public void moveNextTile()
    {
        //for (int i = 0; i < currentPlayer.GetComponent<PlayerScript>().currentPath.Count + 1; i++)
        try
        {
            int i = 0;
            while (i < currentPlayer.GetComponent<PlayerScript>().currentPath.Count + 1)
            {

                if (currentPlayer.GetComponent<PlayerScript>().currentPath.Count == 1)
                {
                    break;
                }

                currentPlayer.GetComponent<PlayerScript>().currentPath.RemoveAt(0);
                currentPlayer.transform.position = TileCoordToWorldCoord(currentPlayer.GetComponent<PlayerScript>().currentPath[0].x, currentPlayer.GetComponent<PlayerScript>().currentPath[0].y);
                Debug.Log("Moving");
            }
        }
        catch
        {
            Debug.Log("Unreachable Tile");
        }
    }

}
