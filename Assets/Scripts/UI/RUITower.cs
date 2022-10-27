using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*SUMMARY (Updated oct 26 2022)
 * 
 * this panel displays information for a tower in the level
 * that is clicked by the player
 * displays:
 *      tower type
 *      all tower attributes (names and current values)
 *          updated in real time
 *       a button for selling the tower
 * 
 * FUNCTIONS
 * Update()
 *      updates the msg of all stat texts if they are not being reset
 *          reset is when old texts are being destroyed and new ones being made
 *      updates the tower type text
 * InitializeElements()
 *      creates stat texts and stores associated TowerAttribute in it's script
 *          adds each text to cache list
 *       assigns the sell tower button to the SellTower function of the clicked towers TowerBaseClass
 * ResetElements()
 *      destroys all texts
 *      clears text cache list
 *      removes function from sell tower button
 */
public class RUITower : MonoBehaviour
{
    #region Variable Declerations
    public GameObject clickedTower;

    [Header("UI Prefabs")]
    public TMP_Text statsTextPrefab;

    [Header("Elements of RUITower")]
    public GameObject statsContent;
    public TMP_Text towerTypeText;
    public Button sellTowerButton;

    //cached list of stat text objects
    public List<TMP_Text> textList = new List<TMP_Text> ();

    //halts updating of stat texts if they are being destroyed and remade
    public bool resetting = false;
    #endregion

    private void Update()
    {
        if (!resetting)
        {
            UpdateStatsText();
        }
        UpdateTypeText();
    }

    public void UpdateTypeText()
    {
        towerTypeText.text = clickedTower.GetComponent<TowerBaseClass>().towerType;
    }

    public void InitializeElements()
    {
        foreach (TowerAttribute attribute in clickedTower.GetComponent<TowerBaseClass>().attributes)
        {
            var newText = Instantiate(statsTextPrefab, statsContent.transform);
            newText.text = attribute.name + ": " + Mathf.Round(attribute.current * 10.0f) * 0.1f;
            newText.GetComponent<StatText>().attribute = attribute;
            textList.Add(newText);
            newText.name = attribute.name + " Text";
        }
        sellTowerButton.onClick.AddListener(delegate { clickedTower.GetComponent<TowerBaseClass>().SellTower(); });
    }

    public void UpdateStatsText()
    {
        foreach(var text in textList)
        {
            text.text = text.GetComponent<StatText>().attribute.name + ": " + Mathf.Round(text.GetComponent<StatText>().attribute.current * 10.0f) * 0.1f;
        }
    }

    public void ResetElements()
    {
        foreach(TMP_Text text in textList)
        {
            Destroy(text.gameObject);
        }
        textList.Clear();

        sellTowerButton.onClick.RemoveAllListeners();
    }
}
