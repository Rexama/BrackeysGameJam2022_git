using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class SelectableObject : MonoBehaviour
{
    [Header("Settings")]
    private float colorChange = 0.2f;
    
    private Color _originalColor;
    private Color _selectedColor;
    private SpriteRenderer _spriteRenderer;

    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _selectedColor = new Color(_originalColor.r-colorChange, _originalColor.g-colorChange, _originalColor.b-colorChange, _originalColor.a);
    }

    public virtual void OnAction(PlayerMove player)
    {
        Debug.Log("Object Selected");
    }
    public virtual void OnFinishAction()
    {
        Debug.Log("Object Released");
    }

    public void SelectObject(Collider2D other)
    {
        var playerSelectManager = other.gameObject.GetComponentInParent<PlayerSelectManager>();
        _spriteRenderer.color = _selectedColor;
        playerSelectManager.AddPossibleSelect(this);
    }
    public void DeSelectObject(Collider2D other)
    {
        var playerSelectManager = other.GetComponentInParent<PlayerSelectManager>();
        _spriteRenderer.color = _originalColor;
        playerSelectManager.DeletePossibleSelect(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SelectObject(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DeSelectObject(other);
        }
    }

    

}
