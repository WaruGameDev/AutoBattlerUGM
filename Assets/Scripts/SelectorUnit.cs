using UnityEngine;

public class SelectorUnit : MonoBehaviour
{
    private Vector3 originPos;
    public Slot currentSlot;

    public UnitData unitData;

    void Start()
    {
        originPos = transform.position;
        GameObject unitGO = 
        Instantiate(SelectorManager.instance.prefabUnit, transform);
        unitGO.GetComponent<Unit>().unitData = unitData;
        unitGO.GetComponent<Unit>().Initialize();
    }

    public void OnDrop()
    {
        if(currentSlot != null)
        {
            transform.position = currentSlot.transform.position;
        }
        else
        {
            transform.position = originPos;
        }
    }
}
