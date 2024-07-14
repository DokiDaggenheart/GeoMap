using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LowerButton : MonoBehaviour
{
    public void PressTheButton()
    {
        StartCoroutine(ChangeButtonsColor());
    }

    private IEnumerator ChangeButtonsColor()
    {
        yield return new WaitForSeconds(0.1f);

        Color color;
        ColorUtility.TryParseHtmlString("#717171", out color);
        GetComponent<Image>().color = color;
        GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }
}
