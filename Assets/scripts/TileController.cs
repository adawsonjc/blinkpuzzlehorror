using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{

    public Tilemap tileMap;
    public Vector3Int pos;  // Given
    public GameObject testTile;



    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3Int(19, -5, 0);

        //tileMap = GetComponent<Tilemap>();
        Debug.Log("TileMap:" + tileMap);
        GameObject gameObjectAtPosition = tileMap.GetInstantiatedObject(pos);
        testTile = gameObjectAtPosition;
       


        //Debug.Log(tileMap.GetTile)
        Debug.Log("TileMap Size: " + tileMap.size);
}

    // Update is called once per frame
    void Update()
    {
        //testTile.transform;
    }
}
