using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionsAndNotesV3 : MonoBehaviour
{
    //https://unityatscale.com/unity-version-control-guide/how-to-setup-unity-project-on-github/

    #region Phases
    //PHASE ONE GOALS - GAMEOBJECT BASICS
    /*
     *  $make zombies move towards towers (pathfinding) $
     *  $create bullets $
     *  $make towers fire at closest zombie in range $
     *     $ make bullets be destroyed if colliding with obstacle $
     *      $make bullets be destroyed if leaving game area $ (obstacles around game area
     *      $make bullets kill zombies $
     * $ make zombies kill towers $
    */

    //PHASE TWO GOALS - UI 
    /*
     * $make a score for killing zombies
     * $make clickable tiles for building towers
     * $make purchasing towers cost points
     * $make zombies spawn continuosly
     * $make zombie impassible walls but can shoot through
     * $add barricades
     * !!add generating boarders and floors
     * !!add generating buildable spaces
     * $make buildible spaces return after tower / wall is destroyed
     * $add begin round button
     * $add moving camera 
     * $add zombie objective
     * $add life for zombie objective
     * $hp can be adjusted in inspector, remove the individual script's hp assigning
     * $add tower life
    */

    //PHASE 3 ZOMBIE AND TOWER INTELLIGENCE
    /*
    * 
    * $$add calculating if zombie can reach tower / objectie, if not remove from list
    * add making closest target for zombies and towers be based on pathfinding
    * $add sell /upgrade panel
    * $add selling towers
    * !!add tower / objective / barricade hp bar (make a function that updates the hpbar in attackable class, call in inheriting class's update)
    * !!clicking on anything not tower or tile disables panels
    *      clicking on ui elements not detected consistantly, so seems impossible to disable panels if anything not "panelable" clicked
    * !!add camera bounds
    *      adding bounds not fesable untill creating levels is implemented
    * 
    * $add highlighting selected objects
   */

    //PHASE 3 ZOMBIE AND TOWER INTELLIGENCE
    /*
     * 
     * $$add calculating if zombie can reach tower / objectie, if not remove from list
     * add making closest target for zombies and towers be based on pathfinding
     * $add sell /upgrade panel
     * $add selling towers
     * !!add tower / objective / barricade hp bar (make a function that updates the hpbar in attackable class, call in inheriting class's update)
     * !!clicking on anything not tower or tile disables panels
     *      clicking on ui elements not detected consistantly, so seems impossible to disable panels if anything not "panelable" clicked
     * !!add camera bounds
     *      adding bounds not fesable untill creating levels is implemented
     * 
     * $add highlighting selected objects
     * */

    // PHASE 4 UPGRADING TOWERS & SPECIAL ZOMBIES
    /*
     * %add zombie hp
     *      %change zombie color based on hp
     * add fast lowhp zombies
     * add slow highhp zombies     
     * 
     * $upgrades for towers
     *     $shooting speed
     *      $damage
     *      $shooting range
     *      $bullet speed
     * change tower stats/ upgradables to a list
     *      create a class for tower stats
     *      
     * change upgrades to have specific upgrades and limits
     * 
     * $make new BaseZombie class and BaseTower class
     * $put stats into functions that can be overidden by inheriting classes
     * %show tower stats in upgrade and buy panels
     */

    //PHASE 5 UPGRADING AND UI REHAUL, ADDING MODELS
    /*
     * $New structure for UI
     *      UI split into two major components - panel on the right side of the screen (RUI) and popup window next to selected towers (PUI)
     * 
     * RUI
     *      $Mode 1 Build:
     *      $when a build tile is selected, display all possible towers to build, some locked and some unlocked
     *          $picture of tower on respective button
     *          greyed out and inactive when locked
     *      $show the stats of a tower that is selected to build
     *      $ability to create selected tower
     *         $ need to have enough money to buy tower
     *      !!update build tower button text(not needed, button interaction is toggled instead)
     *      
     *      Mode 2 Tower:
     *     $Show all current stats of selected tower
     *      show all upgrade paths of selected tower, locked untill prerequisites are met
     *      selling tower
     *      
     * PUI
     *      when a tower is selected:
     *      $pop up next to tower
     *      $buttons for upgrading that tower's stats
     *          $exclude HP
     *          $show "upgrade " + attribute name + "/n Cost: " upgrade cost
     *          %upgrade function
     *          %show "MAXED" when max upgrade has been reached and disable button
     *      $button for repairing that tower
     *          $show total/current health
     *          $show cost to repair
     *          $change cost to = for one point of health
     *          $repair tower function
     *         $ add cost aspect to buttion function
     *     $ add check for having enough points to upgrade
     * 
     * Rehaul Building towers
     *      $enter build mode button
     *      $right click to exit build mode and destroy instantiated tower(refund cost)
     *      $chosen tower follows mouse
     *      $if collider of chosen tower is not inside another collider
     *          $build the tower
     * 
     * 
     * EXTRA
     * $add highlighting clicked objects
     * $override on towers with less than basic stats (barricades) to remove unused stats
     * 
     * 
     * CLEANUP
     * $add making towers subtract from score
     * $add upgrading towers subtract from score
     * $make stat text round floats to 2 decimal points
     * 
     * 
     */

    //INTERMISSION: TIDY CODE AND COMMENTS
    //GIT PUSH

    //PHASE 6 GENERATED LEVELS ATTEMPT 2
    /*
     * on scene start generate level
     * add temp button to generate level
     * COMPONENTS
     *      create floor tiles for entire area
     *          all tiles added to scene for level generation put into temp list
     *          add script to floor tiles that hold information about what is built on them
     *          build boundry walls with a few gaps
     *          go through list and randomly generate walls
     *          
     *          possibly generate a random path to zombie objectives (holes in wall of last level or start base)
     *          and assign tiles along path as to have no walls
     * 
     * 
     * 
     * 
     * 
     */

    #endregion


    //LONG TERM GOALS
    /*
     * ability to create level in scene
     * raycasting from towers to targets to determine if they are valid targets
     * add being unable to build something that has a zombie in the space
     *      taking territory/ needing to have a tower built near a space to build on it
     *      tower taking time to build  
     * add zombie spawning limit
     * add tower / objective / barricade hp bar (make a function that updates the hpbar in attackable class, call in inheriting class's update)
     *      possibly change the model itself rather than hp bar
     * zooming camera in and out
     * clean up UI
     * repairing towers
     * show tower range when selected
     * when upgrading range draw the new range
     * remove zombie collision with each other 
     * adjust sell value of towers as they are upgraded
     * add clicking on and destroying walls at cost
     * add rotating for towers while building
     * different types of target selection (most hp, least hp, closest, etc)
     */

}
