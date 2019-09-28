using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text pointsText;
    public Text textQuantityType1;
    public Text textQuantityType2;
    public Text textQuantityType3;

    public Text buttonType1Text;
    public Text buttonType2Text;
    public Text buttonType3Text;
    
    public void SelectMonster1ToSpawn()
    {
        GameManager.instance.SelectTypeToSpawn(0);
    }    
    
    public void SelectMonster2ToSpawn()
    {
        GameManager.instance.SelectTypeToSpawn(1);
    }    
    
    public void SelectMonster3ToSpawn()
    {
        GameManager.instance.SelectTypeToSpawn(2);
    }

    public void UpdateMonsterType1Qty(int qty)
    {
        textQuantityType1.text = qty + " Restantes";
    }    
    
    public void UpdateMonsterType2Qty(int qty)
    {
        textQuantityType2.text = qty + " Restantes";
    }    
    
    public void UpdateMonsterType3Qty(int qty)
    {
        textQuantityType3.text = qty + " Restantes";
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = points.ToString(); 
    }    
    
    public void SetButtonType1Text(string name)
    {
        buttonType1Text.text = name;
    }    
    
    public void SetButtonType2Text(string name)
    {
        buttonType2Text.text = name;
    }    
    public void SetButtonType3Text(string name)
    {
        buttonType3Text.text = name;
    }
}