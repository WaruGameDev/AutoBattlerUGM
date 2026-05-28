using DG.Tweening;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public Unit playerUnit;
    public Unit enemyUnit;

    Vector3 posOriginPlayer;
    Vector3 posOriginEnemy;    

    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        Clash();
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
