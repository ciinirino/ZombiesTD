using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
/*Summary (Updated Oct 26 2022)
 * 
 * this script handles:
 *      the toggling of UI elements
 *          based on player clicks
 *      storing and displaying the player's score
 *      displaying alerts
 * 
 * FUNCTIONS
 * Awake()
 *      create a static singleton of this script
 *      add towers from "read" object to a list
 * Update()
 *      update and display the player's score
 *      detect clicks and toggle UI elements according to what is clicked
 *          also unhighlights last clicked object and highlights new clicked object
 *          the default state is UIBuild
 *              if anything that is not a tower is clicked UIbuild will be the panel displayed
 *          Tower clicked
 *              if a tower is clicked this UITower will be the panel displayed
 *              as well as PUI being activated
 *          If the cursor is over UI elements this script will stop detecting clicks
 *          this panel also has a "Build Mode" in which it will stop detecting clicks
 *           
 * SetAlertText(sting)
 *      sets the alert text to the given string
 *      resets the alpha of the text to full
 * Highlight(bool)
 *      called when an object is clicked
 *      detects if clicked object has a highlight script
 *          if it does, highlight or unhighlight the object based on given bool
 */
public class UIMain : MonoBehaviour
{
    #region Variable Decleration
    //Initializing this script as a singleton
    public static UIMain UI;

    [Header("Main UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text alertText;

    [Header("Panels")]
    public GameObject rui;
    public GameObject ruiBuild;
    public GameObject ruiTower;
    public GameObject pui;

    public enum RUIStateEnum { RUIbuild, RUItower, Off }
    [Header("States For Toggling UI Elements")]
    public RUIStateEnum RUIstate = RUIStateEnum.RUIbuild;
    public bool puiState = false;
    public bool buildMode;

    [Header("Variables For Detecting Object Clicks")]
    public GameObject clickedObject;
    Ray rayFromMouse;
    RaycastHit hitFromMouse;

    [Header("Object Holding One Of Each Tower Variant Prefab")]
    public GameObject towerPrefabsForReading;
    public List<GameObject> towerPrefabs = new List<GameObject>();
    #endregion

    private void Awake()
    {
        //Initializing this script as a singleton
        if (UI != null)
        {
            GameObject.Destroy(UI);
        }
        else
        {
            UI = this;
        }
        DontDestroyOnLoad(this);
        //this is called in awake so the list is made before the UI reads it
        AddTowerPrefabsToList();
    }
    void AddTowerPrefabsToList()
    {
        //create the list of buildable tower prefabs based on the children in the "to read" gameobject
        foreach (TowerBaseClass tower in towerPrefabsForReading.GetComponentsInChildren<TowerBaseClass>())
        {
            towerPrefabs.Add(tower.gameObject);
        }
    }

    public void Update()
    {
        scoreText.text = "Score: " + GameControllerScript.GC.score;

        //setting default panel states, and handling for if clickedobject is set to destroyed
        if(clickedObject == null)
        {
            RUIstate = RUIStateEnum.RUIbuild;
            puiState = false;
            ToggleUI();
        }

        //detecting mouse clicks
        rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayFromMouse, out hitFromMouse))
        {
            //this detects if the pointer is over a GUI element, and breaks if it is
            if (EventSystem.current.IsPointerOverGameObject() || buildMode)
            {
                return;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //unhighlighting old clicked object
                    if (clickedObject != null) { Highlight(false, clickedObject); }

                    clickedObject = hitFromMouse.collider.gameObject;

                    //highlight new clicked object
                    Highlight(true, clickedObject);
                    if (hitFromMouse.collider.CompareTag("Tower"))
                    {

                        if (ruiTower.GetComponent<RUITower>().clickedTower != clickedObject)
                        {
                            //set RUITower to resetting before changing elements so destroyed objects are not updated
                            ruiTower.GetComponent<RUITower>().resetting = true;
                            ruiTower.GetComponent<RUITower>().ResetElements();
                            ruiTower.GetComponent<RUITower>().clickedTower = clickedObject;
                            ruiTower.GetComponent<RUITower>().InitializeElements();
                            ruiTower.GetComponent<RUITower>().resetting = false;

                            //set PUI to resetting before changing elements so destroyed objects are not updated
                            pui.GetComponent<PUI>().resetting = true;
                            pui.GetComponent<PUI>().DestroyButtons();
                            pui.GetComponent<PUI>().clickedTower = clickedObject;
                            //this script sets the position of the panel so that the PUI does not appear and then move
                            pui.GetComponent<PUI>().SetPosition();
                            pui.GetComponent<PUI>().CreateUpgradeButtons();
                            pui.GetComponent<PUI>().resetting = false;
                        }
                        RUIstate = RUIStateEnum.RUItower;
                        puiState = true;
                    }
                    else
                    {
                        RUIstate = RUIStateEnum.RUIbuild;
                        puiState = false;
                    }

                }

            }

            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        switch (RUIstate)
        {
            case RUIStateEnum.RUIbuild:
                ruiTower.SetActive(false);

                ruiBuild.SetActive(true);
                break;
            case RUIStateEnum.RUItower:
                ruiBuild.SetActive(false);

                ruiTower.SetActive(true);
                break;
        }
        pui.SetActive(puiState);
    }

    public void SetAlertText(string msg)
    {
        alertText.text = msg;
        alertText.GetComponent<FadingText>().ResetFade();
    }

    public void Highlight(bool setTo, GameObject highlightObj)
    {
        Highlight highlight = highlightObj.GetComponent<Highlight>();
        if(highlight != null)
        {
            highlight.ToggleHighlight(setTo);
        }
    }
}
