using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*SUMMARY (Updated oct 26 2022)
 * 
 * this panel displays:
 *      Scrollview of buttons for each tower variant in list
 *          list aquired from UIMain
 *          the tower each button is built for is stored in the button's script
 *      each button is linked to two functions based on the tower prefab stored in the button's script
 *          building the texts for each tower attribute
 *          setting the selected tower text's text
 *      a button to build the selected tower
 * 
 * FUNCTIONS
 * Start()
 *      creates buttons for each tower prefab in list
 *      sets tower type text to blank
 * Update()
 *      toggles interactable of build button
 * BuildStatsText()
 *      destroys old texts and builds new ones for each TowerAttribute in new selected tower
 * BuildTowerButton()
 *      checks if the tower cost is greater than GC score
 *          if it is send msg to alert text
 *          if not instantiate a prefab of the selected tower type
 *              set the tower to build mode
 *              set the UIMain to build mode
 */
public class RUIBuild : MonoBehaviour
{
    #region Variable Decleration
    [Header("Prefabs")]
    public Button towerSelectButtonPrefab;
    public TMP_Text towerStatTextPrefab;

    [Header("Elements of RUI Build")]
    public GameObject towerSelectContent;
    public GameObject towerStatsContent;
    public TMP_Text towerTypeText;
    public Button buildTowerButton;

    [Header("Build Mode Variables")]
    public bool buildmode = false;
    public GameObject selectedTowerType;
    public GameObject towerParent;

    //used for detecting if the stat texts need to be destroyed and remade
    bool needToClear = false;
    #endregion

    private void Start()
    {
        towerTypeText.text = "";
        CreateTowerSelectButtons();
    }
    private void Update()
    {
        if(selectedTowerType == null)
        {
            buildTowerButton.interactable = false;
        }
        else
        {
            buildTowerButton.interactable = true;
        }
    }

    #region Functions For Updating / Creating Text Objects
    public void CreateTowerSelectButtons()
    {
        foreach (GameObject tower in UIMain.UI.towerPrefabs)
        {
            var newButton = Instantiate(towerSelectButtonPrefab, towerSelectContent.transform);
            newButton.image.sprite = tower.GetComponent<TowerBaseClass>().buttonSprite;
            newButton.GetComponent<RUITowerSelectButton>().tower = tower;
            newButton.onClick.AddListener(delegate { BuildStatsTexts(newButton.GetComponent<RUITowerSelectButton>().tower); });
            newButton.onClick.AddListener(delegate { SetSelectedTowerType(newButton.GetComponent<RUITowerSelectButton>().tower); });
        }

    }

    public void BuildStatsTexts(GameObject tower)
    {
        if(needToClear == false)
        {
            CreateStatsText(tower);
            SetTowerTypeText(tower);
            needToClear = true;
        }
        else
        {
            DestroyStatsText();
            CreateStatsText(tower);
            SetTowerTypeText(tower);
        }
    }

    public void CreateStatsText(GameObject tower)
    {
        var costText = Instantiate(towerStatTextPrefab, towerStatsContent.transform);
        costText.text = "Cost: " + tower.GetComponent<TowerBaseClass>().cost;
        foreach(TowerAttribute attribute in tower.GetComponent<TowerBaseClass>().attributes)
        {
            var newText = Instantiate(towerStatTextPrefab, towerStatsContent.transform);
            newText.text = attribute.name + ": " + attribute.current;
        }
    }
    
    public void SetTowerTypeText(GameObject tower)
    {
        towerTypeText.text = tower.GetComponent<TowerBaseClass>().towerType;
    }

    public void DestroyStatsText()
    {
        TMP_Text[] listText = towerStatsContent.GetComponentsInChildren<TMP_Text>();
        foreach(TMP_Text text in listText)
        {
            Destroy(text.gameObject);
        }
    }
    #endregion

    public void SetSelectedTowerType(GameObject tower)
    {
        selectedTowerType = tower;
    }
    public void BuildTower()
    {
        if(GameControllerScript.GC.score < selectedTowerType.GetComponent<TowerBaseClass>().cost)
        {
            UIMain.UI.SetAlertText("Not Enough Points To Build Tower");
        }
        else
        {
            if (selectedTowerType != null)
            {
                var newTower = (Instantiate(selectedTowerType, towerParent.transform));
                newTower.GetComponent<TowerBaseClass>().buildModeTower = true;
                UIMain.UI.buildMode = true;
            }
            else
            {
                Debug.Log("No Tower Type Selected");
            }
        }
    }
}
