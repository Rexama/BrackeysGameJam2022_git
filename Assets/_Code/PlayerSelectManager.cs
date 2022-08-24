using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectManager : MonoBehaviour
{
    private SelectableObject _possibleSelect;
    private List<SelectableObject> _selectableObjects = new List<SelectableObject>();
    
    public void Select()
    {
        if(_selectableObjects.Count > 0)
        {
            _selectableObjects[0].OnSelected();
        }
    }
    public void AddPossibleSelect(SelectableObject select)
    {
        _selectableObjects.Add(select);
    }
    public void DeletePossibleSelect(SelectableObject select)
    {
        _selectableObjects.Remove(select);
    }
    
}
