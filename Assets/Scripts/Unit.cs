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

    void Start()
    {
        currentHealth = maxHealth;
        currentDefense = initalDefense;
        currentAttack = initialAttack;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        GameObject text = Instantiate(textPrefab, 
            transform.position + new Vector3(UnityEngine.Random.Range(-.5f,.5f ),5,0), Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
        text.transform.DOJump(text.transform.position, .5f,1,.25f).OnComplete(
            ()=> Destroy(text)
        );

        transform.DOPunchScale(new Vector3(.2f,-.2f,0),.25f,2).SetRelative(true);

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Attack(Unit target, Action onEndAttack = null)
    {
        target.TakeDamage(currentAttack);
        transform.DOJump(BattleManager.instance.transform.position 
        + new Vector3(-1f,0,0),1,1,.25f).OnComplete(()=> onEndAttack?.Invoke());

    }
    public void Die()
    {
        Destroy(gameObject);
    }

}
