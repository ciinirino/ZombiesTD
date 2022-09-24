using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableAndAttackableObject : AttackableObject
{
    //the tile used to instantiate this object
    public GameObject parentTile;

    //!! this returns the error "GameObjects can not be made active when they are being destroyed."
    //!!however i am not destroying a gameobject that i am activating, and it works
    //!! i think this is only returning this when i end play, because the parent tile is being destroyed at the end of the scene??
    public void OnDestroy()
    {
        parentTile.SetActive(true);
    }
}
