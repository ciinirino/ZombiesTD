using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * SEPT 23 2022
 * 
 * this script is for storing the buildibleTile that instantiated this object
 *      and for reactivating the tile when this object is destroyed
 */
public class BuildibleObject : MonoBehaviour
{
    public GameObject parentTile;
    //!! this returns the error "GameObjects can not be made active when they are being destroyed."
    //!! however i am not destroying a gameobject that i am activating, and it works
    //!! i think this is only returning this when i end play, because the parent tile is being destroyed at the end of the scene??
    public void OnDestroy()
    {
        parentTile.SetActive(true);
    }
}
