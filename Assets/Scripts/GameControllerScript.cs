using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*SUMMARY (Updated oct 26 2022)
 * 
 * this object is a "singleton", having only one in the scene and the "dontDestroyOnLoad" property
 * this object is for holding global variables and data
 *      the only action this object takes is to remove destroyed gameobjects from objectivesList
 *      
 *VARIABLES
 * player's score
 *      towers and upgrades cost score to build
 *      points should be earned from killing zombies
 *      displayed by UIMain
 * List of zombie objectives (currently nonfunctional; towers do not add themselves to list)
 *      this list holds all targets that zombies will path to
 *      also clears targets from list when they are destroyed
 */
public class GameControllerScript : MonoBehaviour
{
    //this is the forum i used for setting up a "singleton" https://answers.unity.com/questions/323195/how-can-i-have-a-static-class-i-can-access-from-an.html
    public static GameControllerScript GC;

    //curently score is updated from bullets colliding with zombies, and from UIscript creating towers
    public int score = 0;

    //a list of all objects that zombies will path to
    public List<GameObject> objectivesList = new List<GameObject>();

    private void Awake()
    {
        // creating a singleton
        if (GC != null)
        {
            GameObject.Destroy(GC);
        }
        else
        {
            GC = this;
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //handles removing destroyed objects from the objectiveList
        foreach (GameObject objective in objectivesList)
        {
            if (objective == null)
            {
                objectivesList.Remove(objective);
                return;
            }
        }
    }
}
