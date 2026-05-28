using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class Unit : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 10;

    public int currentDefense;
    public int initalDefense = 5;

    public int currentAttack;
    public int initialAttack;
    public GameObject textPrefab;

    private Vector3 currentPos;


    void Start()
    {
        currentHealth = maxHealth;
        currentDefense = initalDefense;
        currentAttack = initialAttack;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        transform.DOPunchScale(new Vector3(.2f,-.2f,0),.25f,2).SetRelative(true);
        GameObject text = Instantiate(textPrefab, 
            transform.position + new Vector3(UnityEngine.Random.Range(-.5f,.5f ),5,0), Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
        text.transform.DOJump(text.transform.position, .5f,1,.25f).OnComplete(
            ()=> Destroy(text)
        ); 

       
    }
    public void Attack(Unit target, bool isPlayer = false)
    {
        //posicionamiento
        currentPos = transform.position;              
        Vector3 clashOffset = new Vector3(0,0,0);
        if(isPlayer)
        {
            clashOffset.x = -1;
        }
        else
        {
            clashOffset.x = 1;
        }
        //el ataque
        transform.DOJump(BattleManager.instance.transform.position 
        + clashOffset,1,1,.25f).OnComplete(()=> 
        {           
            target.TakeDamage(currentAttack);
        });

    }
    public void BackUnit()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
           
            transform.DOJump(currentPos,1,1,.2f);
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }

}
