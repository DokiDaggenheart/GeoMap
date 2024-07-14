using System.Collections;
using UnityEngine;

public class WindowSettings : MonoBehaviour
{
    private int targetWidth = 580;
    private int targetHeight = 1000;

    void Start()
    {
        Screen.SetResolution(targetWidth, targetHeight, false);
        StartCoroutine(EnforceResolution());
    }

    IEnumerator EnforceResolution()
    {
        while (true)
        {
            if (Screen.width != targetWidth || Screen.height != targetHeight)
            {
                Screen.SetResolution(targetWidth, targetHeight, false);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}