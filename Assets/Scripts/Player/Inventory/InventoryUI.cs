using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Inject] InventorySystem _inventorySystem;
    public TextMeshProUGUI foodText;

    private void Update()
    {
        try
        {
            foodText.text = "x" + _inventorySystem.food.ToString();
        }
        catch
        {

        }
    }
}
