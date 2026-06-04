using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorManager : MonoBehaviour
{
    public static SelectorManager instance;
    public GameObject prefabUnit;
    public List<Slot> slots;

    void Awake()
    {
        instance = this;
    }

    public void SetBattlers()
    {
        DataManager.selectedUnits.Clear();
        foreach(Slot s in slots)
        {
            DataManager.selectedUnits.Add(s.currentSelectedUnit.unitData);
        }
        SceneManager.LoadScene("SampleScene");
    }
 
    
}
