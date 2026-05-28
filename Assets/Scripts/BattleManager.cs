using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public List<UnitData> playerUnitsData;
    public List<UnitData> enemyUnitsData;

    public List<Transform> playerUnitPos;
    public List<Transform> enemyUnitPos;

    public List<Unit> playerUnitGO;
    public List<Unit> enemyUnitGO;


    public GameObject unitPrefab;

    public Unit playerUnit;
    public Unit enemyUnit;


    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        GeneratePlayerUnit();
        GenerateEnemyUnit();
        playerUnit = playerUnitGO[0];
        enemyUnit = enemyUnitGO[0];
        Clash();
    }
    public void GeneratePlayerUnit()
    {
        for(int i = 0; i< playerUnitsData.Count; i++)
        {
            GameObject u = Instantiate(unitPrefab, playerUnitPos[i].position, Quaternion.identity);
            Unit playerU = u.GetComponent<Unit>();
            playerU.unitData = playerUnitsData[i];
            playerU.playerUnit = true;
            playerU.Initialize();
            playerUnitGO.Add(playerU);
        }
    }
    public void GenerateEnemyUnit()
    {
        for(int i = 0; i< enemyUnitsData.Count; i++)
        {
            GameObject u = Instantiate(unitPrefab, enemyUnitPos[i].position, Quaternion.identity);
            Unit playerU = u.GetComponent<Unit>();
            playerU.unitData = enemyUnitsData[i];
            playerU.playerUnit = false;
            playerU.Initialize();
            enemyUnitGO.Add(playerU);
        }
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Clash();
    //     }
    // }
    public void Clash()
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 posOriginPlayer = playerUnit.transform.position;
        Vector3 posOriginEnemy = enemyUnit.transform.position;        
             
        sequence.AppendCallback(AttackUnits);        
        sequence.AppendInterval(.5f);
        sequence.AppendCallback(BackToPlaceUnits);         
        
    }
    public void AttackUnits()
    {
        playerUnit.Attack(enemyUnit, true);
        enemyUnit.Attack(playerUnit,  false);
    }
    public void BackToPlaceUnits()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(playerUnit.BackUnit);
        sequence.JoinCallback(enemyUnit.BackUnit);
        sequence.AppendInterval(.5f);
        sequence.AppendCallback(()=>
        {
            if(playerUnit != null && enemyUnit != null)
            {
                Clash();
            }
            else
            {
                Debug.Log("Se acabo la pelea");
            }       
        });
        
    }


    public void CheckUnit()
    {
        
    }
}
