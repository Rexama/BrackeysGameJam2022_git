using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectManager : MonoBehaviour
{
    private List<SelectableObject> _selectableObjects = new List<SelectableObject>();
    private SelectableObject _selectedObject;
    
    public void Action()
    {
        if(_selectableObjects.Count > 0)
        {
            _selectableObjects[0].OnAction();
            _selectedObject = _selectableObjects[0];
        }
    }
    
    
    public void FinishHold()
    {
        if(_selectedObject!= null)
        {
            _selectedObject.OnFinishAction();
        }
    }
    public void AddPossibleSelect(SelectableObject select)
    {
        _selectableObjects.Add(select);
    }
    public void DeletePossibleSelect(SelectableObject select)
    {
        if(_selectedObject == select)
        {
            _selectedObject.OnFinishAction();
            _selectedObject = null;
        }
        _selectableObjects.Remove(select);
    }
    
}
