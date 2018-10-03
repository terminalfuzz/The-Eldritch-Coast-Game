using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayerScript : PlayerScript {

    public int tileX;
    public int tileY;

    public List<Node> currentPath = null;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseUp()
    {
        GameManager.Instance.moveNextTile(3);
    }


}
