using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerButton : MonoBehaviour
{
    [SerializeField] private GameObject window;
    private void Start()
    {
        LowerButtonController.TurnOffButtons += DeactivateWindow;
    }
    public void ActivateWindow()
    {
        LowerButtonController.InvokeTurnOffButtons();
        try
        {
            window.SetActive(true);
        }
        catch
        {

        }
    }

    public void DeactivateWindow()
    {
        window.gameObject.SetActive(false);
    }
}
