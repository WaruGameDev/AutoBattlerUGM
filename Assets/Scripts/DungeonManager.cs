using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public List<DungeonEvent> dungeonEvents; 

    public static DungeonManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        BattleManager.instance.Initialize();
        TriggerEventDungeon();
    }

    public void TriggerEventDungeon()
    {
        dungeonEvents[0].TriggerEvent();
    }
    public void NextDungeonEvent()
    {
        dungeonEvents.RemoveAt(0);
        if(dungeonEvents.Count>0)
        {
            TriggerEventDungeon();
        }
        else
        {
            Debug.Log("completaste el dungeon");
        }
    }
}
