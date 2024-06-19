using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestSystem : MonoBehaviour
{
    [Inject]private InventorySystem _inventorySystem;
    [SerializeField] private Button foodButton;
    [SerializeField] private Button bedButton;
    private bool foodUsed;
    private bool bedUsed;

    public void UseFood() 
    {
        foodUsed = !foodUsed;

    }
}
