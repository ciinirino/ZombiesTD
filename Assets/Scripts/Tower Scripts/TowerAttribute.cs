using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Summary (Updated oct 26 2022)
 * 
 * this class is for holding all data relevent to a specific tower attribute
 */
public class TowerAttribute 
{
    public string name;
    public float start;
    public float current;
    public float upgradeInc;
    public int upgradeCount = 0;
    public int upgradeLimit;

    public TowerAttribute(string attributeName, float start, float current, float upgradeInc, int upgradeCount, int upgradeLimit)
    {
        //Name of the attribute
        this.name = attributeName;

        //Starting value of the attribute
        this.start = start;

        //Current value of the attribute
        this.current = current;

        //The increment that the attribute should be increased by when upgraded
        this.upgradeInc = upgradeInc;

        //How many times the attribute has been upgraded
        this.upgradeCount = upgradeCount;

        //How many times this attribute can be upgraded
        this.upgradeLimit = upgradeLimit;
    }
}
