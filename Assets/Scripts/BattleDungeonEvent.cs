using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Battle", menuName ="Dungeon/Battle")]
public class BattleDungeonEvent : DungeonEvent
{
    public List<UnitData> enemyPartyData;

    public override void TriggerEvent()
    {
        BattleManager.instance.enemyUnitsData.Clear();
        BattleManager.instance.enemyUnitsData.AddRange(enemyPartyData);
        BattleManager.instance.StartBattle();
    }
}
