using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreatorScript : MonoBehaviour
{
    public float levelWidth;
    public float levelHeight;

    [Header("Game Objects To Instantiate")]
    public GameObject floorPrefab;
    public GameObject boarderPrefab;
    public GameObject buildableTilePrefab;

    [Header("Parent Game Objects")]
    public GameObject floorAndBoarderParent;
    public GameObject buildableTileParent;

    public void CreateBoard()
    {

        //height boarders
        for (int i = 0; i < levelWidth; i++)
        {
            Instantiate(floorPrefab, new Vector3(i, 0, levelWidth), Quaternion.identity, floorAndBoarderParent.transform);
            Instantiate(floorPrefab, new Vector3(i, 0, 0), Quaternion.identity, floorAndBoarderParent.transform);
        }
    }
}
