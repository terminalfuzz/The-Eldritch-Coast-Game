using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    protected static TurnManager _instance;

    public static TurnManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<PlayerScript> units;
    public GameObject currentPlayer;
    public int currentPlayerIndex;

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

    public void nextTurn()
    {
        if (units.Count > 0)
        {
            if (currentPlayerIndex + 1 < units.Count)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 0;
            }
        }
    }
}
