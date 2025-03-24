using UnityEngine;
using UnityEngine.EventSystems;

public class PropertyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Property property;
    public Engine engine; 

    private void Start()
    {
        property = GetComponent<Property>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on: property");
        engine.updateTile_s(property);
    }
}
