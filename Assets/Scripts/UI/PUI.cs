using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*SUMMARY (Updated oct 26 2022)
 * 
 *  Displays buttons that are linked to the upgrade function of the TowerBaseClass
 *  for each of a tower's attributes
 *      displays a unique "repair" button for the health attribute
 *      toggles interactible and text when the upgrade limit of the attribute has been reached
 *  displayed in a scrollview next to the selected tower
 *      selected tower sent from UIMain
 * 
 * FUNCTIONS
 * Update()
 *      sets the position of the panel to the right of the selected tower
 *          !!this causes issues when the tower is on the edge of the game view
 *      updates the text of the buttons to reflect the current variables of each TowerAttribute
 * CreateUpgradeButtons()
 *      creates a button for each of the tower's attributes
 *      links the button the upgrade function of that tower (or repair)
 *      adds the button and the button's text to a list to cache list
 * UpdateExistingButtons()
 *      updates the text of each button according to the variables in it's linked TowerAttribute
 *      toggles interactable if upgrade limit has been reached
 * DestroyButtons()
 *      destroy every button object in cached list
 *      clear text and button cache lists
 *      (should be called before creating new buttons for a different selected tower)
 *      !Possibly add functionality that this is not called if new tower is of the same type for efficiency
 */
public class PUI : MonoBehaviour
{
    #region Variable Decleration
    public GameObject puiContent;
    public Button buttonPrefab;

    public GameObject clickedTower;
    public bool resetting = false;

    public List<TMP_Text> textList  = new List<TMP_Text>();
    public List<Button> buttonList = new List<Button>();
    #endregion

    public void Update()
    {
        if(!resetting)
        {
            UpdateExistingButtons();
            //set position is called in update so that if the camera moves the panel moves with it
            SetPosition();
        }
    }

    public void SetPosition()
    {
        //set the position of PUI to 100 units right of selected tower
        //!!potential problems when tower is close to an edge
        transform.position =Camera.main.WorldToScreenPoint( clickedTower.transform.position) + new Vector3(120f, 0f);

    }

    public void CreateUpgradeButtons()
    {
        

        foreach(TowerAttribute attribute in clickedTower.GetComponent<TowerBaseClass>().attributes)
        {
            if (attribute.name == "Health")
            {
                var newButton = Instantiate(buttonPrefab, puiContent.transform);

                //if on the health attribute create a unique button for repairing
                if (attribute.current == attribute.start)
                {
                    newButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Repair (" + attribute.start + "/" + attribute.current + ")\n Full Health";
                }
                else
                {

                    newButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Repair (" + attribute.start + "/" + attribute.current + "\nCost: " + 1;
                }
                newButton.GetComponentInChildren<StatText>().attribute = attribute;
                newButton.onClick.AddListener(delegate { clickedTower.GetComponent<TowerBaseClass>().UpdateHp(-1); });
                textList.Add(newButton.GetComponentInChildren<TMP_Text>());
                buttonList.Add(newButton);
            }
            else
            {
                var newButton = Instantiate(buttonPrefab, puiContent.transform);
                newButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Upgrade " + attribute.name + "\nCost: " + (attribute.upgradeCount+1);
                newButton.GetComponentInChildren<StatText>().attribute = attribute;
                newButton.onClick.AddListener(delegate { clickedTower.GetComponent<TowerBaseClass>().UpgradeAttribute(attribute, (attribute.upgradeCount+1)); });
                textList.Add(newButton.gameObject.GetComponentInChildren<TMP_Text>());
                buttonList.Add(newButton);
            }
        }
    }

    public void UpdateExistingButtons()
    {
        foreach (TMP_Text text in textList)
        {
            if(text.GetComponent<StatText>().attribute.name == "Health")
            {
                if (text.GetComponent<StatText>().attribute.current == text.GetComponent<StatText>().attribute.start)
                {
                    text.text = "Repair (" + text.GetComponent<StatText>().attribute.start + "/" + text.GetComponent<StatText>().attribute.current + ")\n Full Health";
                }
                else
                {
                    text.text = "Repair (" + text.GetComponent<StatText>().attribute.start + "/" + text.GetComponent<StatText>().attribute.current + "\nCost: " + 1;
                }
            }
            else
            {
                if(text.GetComponent<StatText>().attribute.upgradeCount == text.GetComponent<StatText>().attribute.upgradeLimit)
                {
                    text.text = "Upgrade " + text.GetComponent<StatText>().attribute.name + "\nMAXED";
                }
                else
                {
                    text.text = "Upgrade " + text.GetComponent<StatText>().attribute.name + "\nCost: " + (text.GetComponent<StatText>().attribute.upgradeCount+1);
                }
            }
        }
        foreach(Button button in buttonList)
        {
            if(button.GetComponentInChildren<StatText>().attribute.name == "Health")
            {
                if(button.GetComponentInChildren<StatText>().attribute.start == button.GetComponentInChildren<StatText>().attribute.current)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
            else
            {
                if(button.GetComponentInChildren<StatText>().attribute.upgradeCount == button.GetComponentInChildren<StatText>().attribute.upgradeLimit)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
    }

    public void DestroyButtons()
    {
        foreach(Button button in buttonList)
        {
            Destroy(button.gameObject);
        }
        textList.Clear();
        buttonList.Clear();
    }
}
