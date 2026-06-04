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
        playerUnitsData.Clear();
        playerUnitsData.AddRange(DataManager.selectedUnits);
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
        if(playerUnit != null) 
        {
            sequence.AppendCallback(playerUnit.BackUnit);
        }
        if(enemyUnit != null) 
        {
            sequence.JoinCallback(enemyUnit.BackUnit);
        }
        sequence.AppendInterval(.5f);
        sequence.AppendCallback(()=>
        {
            if(playerUnit != null && enemyUnit != null)
            {
                Clash();
            }
            else
            {
                ReorderUnit();
            }       
        });  
        //sequence.AppendCallback(ReorderUnit);      
    }

    public void EndOfTurn()
    {
        playerUnitGO.RemoveAll(u => u == null);
        enemyUnitGO.RemoveAll(u => u == null);
    }
    public void ReorderUnit()
    {
        // Limpiar listas ANTES de iterar, con RemoveAll (seguro)
        playerUnitGO.RemoveAll(p => p == null);
        enemyUnitGO.RemoveAll(p => p == null);

        Sequence sequence = DOTween.Sequence();

        // Reposicionar con índice directo, sin IndexOf sobre lista sucia
        for (int i = 0; i < playerUnitGO.Count; i++)
        {
            sequence.Append(playerUnitGO[i].transform.DOJump(playerUnitPos[i].position, 1, 1, .25f));
        }
        for (int i = 0; i < enemyUnitGO.Count; i++)
        {
            sequence.Join(enemyUnitGO[i].transform.DOJump(enemyUnitPos[i].position, 1, 1, .25f));
        }

        sequence.AppendCallback(()=>
        {
            // Actualizar referencias frontales y continuar si quedan unidades
            if (playerUnitGO.Count > 0 && enemyUnitGO.Count > 0)
            {
                playerUnit = playerUnitGO[0];
                enemyUnit = enemyUnitGO[0];
                Clash();
            }
            else
            {
                Debug.Log(playerUnitGO.Count == 0 ? "Enemy wins!" : "Player wins!");
            }
        });

        
    }


    public void CheckUnit()
    {
        
    }
}
