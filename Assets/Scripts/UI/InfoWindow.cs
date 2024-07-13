using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    public bool isActive = false;
    public GameObject appearingWindow;
    public string appearingWindowText;

    public void FalseActivate()
    {
        StartCoroutine(ShowWindow());
    }

    public IEnumerator ShowWindow()
    {
        appearingWindow.SetActive(true);
        appearingWindow.GetComponentInChildren<TextMeshProUGUI>().text = appearingWindowText;
        yield return new WaitForSeconds(2.0f);
        appearingWindow.SetActive(false);
    }
}
