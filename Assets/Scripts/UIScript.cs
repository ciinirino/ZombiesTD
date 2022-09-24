using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
 * SEPT 23 2022 
 * this class handles all updates and interactions with UI
 * 
 * 
 *BUILDTOWERPANEL
 *the build tower panel is for building new towers, updated when a buildible tile is clicked
 *the dropdown menu is a list of buildible towers, added in the start function of this class
 *      the list of towers is a list of typ BuildTowerInfo
 *          currently stores the towername, cost to build, the prefab of the tower, and what postion in the list it is!!this can probably be removes
 *          !!adding items causes a warning, detailed in start()
 *each tower requires a certain amount of points to build (score is stored in GameController)
 *this script calls a function in a selected buildableTile, which then instantiates the tower and deactivates itself
 * 
 * 
 * 
 * 
 */
public class UIScript : MonoBehaviour
{
    //PRIVATE VARIABLES

    //list of towers that are able to be build by the player
    List<BuildTowerInfo> towersList = new List<BuildTowerInfo>();

    //used in detecting mouse clicks
    Ray rayFromMouse;
    RaycastHit hitFromMouse;
    //if the above ray contacts a buildableTile, the tile object is stored here
    GameObject selectedTile;


    //PUBLIC VARIABLES

    //elemnts of the MainUI;
    public TMP_Text scoreText;
    
    //elements of the bulding tower panel
    [Header("Elemnts of the Build Tower Panel")]

    public TMP_Text selectedTileText;
    public Button buildTowerButton;
    public TMP_Dropdown buildTowerDropdown;
    public TMP_Text errorText;
    public TMP_Text towerCostText;

    //prefabs of buildible towers
    [Header("Prefabs of towers for the Buildable Tower List")]
    public GameObject basicTowerPrefab;
    public GameObject barricadePrefab;




    private void Awake()
    {
        //setting the building buttons to disabled by default untill a tile has been selected
        ToggleBuildPanelElements(false);
        errorText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //list of all buildible towers to display
        //add new towers here        
        //!!this gives a warning saying "new" cant be used, as this is creating a class that is not attached to anything.
        //!!it still seems to work and is the way shown in the unit api. im not sure what the alternative is
        towersList.Add(new BuildTowerInfo("basicTower", 3, basicTowerPrefab, 0));
        towersList.Add(new BuildTowerInfo("Barricade", 1, barricadePrefab, 1));

        //creating a dropdown datalist is required to add items to a dropdown menu
        List<TMP_Dropdown.OptionData> towerDropdownListItem = new List<TMP_Dropdown.OptionData>();
        buildTowerDropdown.ClearOptions();
        //this creates a dropdown item for each tower in towersList
        foreach(BuildTowerInfo tower in towersList)
        {
            TMP_Dropdown.OptionData towerData = new TMP_Dropdown.OptionData();
            towerData.text = tower.towerName;
            towerDropdownListItem.Add(towerData);
        }
        buildTowerDropdown.AddOptions(towerDropdownListItem);
    }

    // Update is called once per frame
    void Update()
    {
        //casts rays continually from mouse position
        //if the tag corrisponds to a buildable tile enable the buildTowerPanel elements, or disable them if not
        rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayFromMouse, out hitFromMouse))
        {
            if (Input.GetMouseButtonDown(0))
            { 
                if (hitFromMouse.collider.CompareTag("Buildable"))
                {
                    //stores the clicked tile
                    selectedTile = hitFromMouse.collider.gameObject;
                    ToggleBuildPanelElements(true);

                }
            }               
        }


        //updates for dynamic text elements
        scoreText.text = ("Score: " + GameControllerScript.GC.score);
        towerCostText.text = ("Tower Cost:" + towersList[buildTowerDropdown.value].towerCost);
    }

    //linked to the Build Tower Button on the Build Tower Panel
    public void BuildTowerClicked()
    {
        //display an error if there is not enough points to buy a tower
        if(GameControllerScript.GC.score < towersList[buildTowerDropdown.value].towerCost)
        {
            errorText.text = "not enough points to buy";
            errorText.gameObject.SetActive(true);
        }
        //if there are enough points, create a tower and reset the buildTowerPanel elements
        //the type of tower to create is base on the current value of the buildTowerDropdown
        else if (GameControllerScript.GC.score >= towersList[buildTowerDropdown.value].towerCost)
        {
            GameControllerScript.GC.score -= towersList[buildTowerDropdown.value].towerCost;
            selectedTile.GetComponent<BuildableTileScript>().BuildTower(towersList[buildTowerDropdown.value].towerPrefab);
            errorText.gameObject.SetActive(false);
            ToggleBuildPanelElements(false);

        }

    }

    //function to toggle buildTowerPanel elements
    //this does not handle the error text, as it has special conditions
    public void ToggleBuildPanelElements(bool toSet)
    {
        if (toSet)
        {
            selectedTileText.text = "Empty Tile";
        }
        else
        {
            selectedTile = null;
            selectedTileText.text = "No Tile Selected";
        }
        buildTowerDropdown.gameObject.SetActive(toSet);
        buildTowerButton.gameObject.SetActive(toSet);
        towerCostText.gameObject.SetActive(toSet);
    }
}
