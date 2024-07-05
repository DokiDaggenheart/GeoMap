using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestSystem : MonoBehaviour
{
    [Inject] private InventorySystem _inventorySystem;
    [SerializeField] private Button foodButton;
    [SerializeField] private Button bedButton;
    private bool foodUsed;
    private bool bedUsed;

    public void BedButtonListener()
    {
        if (!bedUsed)
        {
            bedButton.gameObject.GetComponent<Image>().color = Color.white;
            bedButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            bedUsed = true;
        }
        if (bedUsed)
        {
            bedButton.gameObject.GetComponent<Image>().color = Color.black;
            bedButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            bedUsed = false;
        }
    }

    public void FoodButtonListener()
    {
        if (!foodUsed)
        {
            foodButton.gameObject.GetComponent<Image>().color = Color.white;
            foodButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            foodUsed = true;
        }
        if (foodUsed)
        {
            foodButton.gameObject.GetComponent<Image>().color = Color.black;
            foodButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            foodUsed = false;
        }
    }
}
