using UnityEngine;

public class Slot : MonoBehaviour
{
    public SelectorUnit currentSelectedUnit;
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Unit"))
        {
            if(currentSelectedUnit == null)
            {
                currentSelectedUnit = collision.GetComponent<SelectorUnit>();
                collision.GetComponent<SelectorUnit>().currentSlot = this;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(currentSelectedUnit == null)  return;
        if(collision.gameObject.CompareTag("Unit") && 
        collision.gameObject == currentSelectedUnit.gameObject)
        {
            collision.GetComponent<SelectorUnit>().currentSlot = null;
            currentSelectedUnit = null;
        }
    }
}
