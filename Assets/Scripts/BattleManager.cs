using DG.Tweening;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public Unit playerUnit;
    public Unit enemyUnit;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Clash();
        }
    }
    public void Clash()
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 posOriginPlayer = playerUnit.transform.position;
        Vector3 posOriginEnemy = enemyUnit.transform.position;
        
        playerUnit.Attack(enemyUnit,CheckUnit );


        sequence.Join(enemyUnit.transform.DOJump(transform.position
        + new Vector3(.5f,0,0)
        ,1,1,.25f));
        sequence.AppendCallback(()=> playerUnit.Attack(enemyUnit));
        sequence.JoinCallback(()=> enemyUnit.Attack(playerUnit));
        sequence.AppendInterval(.5f);
        sequence.Append(playerUnit.transform.DOJump(posOriginPlayer,1,1,.25f));
        sequence.Join(enemyUnit.transform.DOJump(posOriginEnemy,1,1,.25f));        
        
    }
    public void CheckUnit()
    {
        
    }
}
