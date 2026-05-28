using UnityEngine;
[CreateAssetMenu(fileName ="Unit", menuName ="AutoBattler/Unit")]
public class UnitData : ScriptableObject
{ 
    public string nameUnit;
    public int maxHealth = 10;    
    public int initalDefense = 5;    
    public int initialAttack = 2;
    public Sprite unitSprite;
}
