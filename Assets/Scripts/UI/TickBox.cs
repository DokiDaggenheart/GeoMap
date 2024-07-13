using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TickBox : MonoBehaviour
{
    public bool isActive = false;
    public GameObject appearingWindow;
    public string appearingWindowText;

    public void Activate()
    {
        isActive = !isActive;
        if (isActive)
            GetComponent<Image>().color = Color.green;
        if (!isActive)
            GetComponent<Image>().color = Color.grey;
    }

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
