using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PathView : MonoBehaviour
{
    [Inject] private PathModel _pathModel;

    private void Start()
    {
        for (int i = 0; i < _pathModel.pathList.Count; i++)
        {
            DrawDottedLine(_pathModel.pathList[i].start, _pathModel.pathList[i].end, _pathModel.spacingLength, _pathModel.dotPrefab);
        }
    }

    private void DrawDottedLine(Transform startObj, Transform endObj, float spacing, GameObject dotPrefab)
    {
        Vector3 startPos = startObj.position;
        Vector3 endPos = endObj.position;
        Vector3 lineDirection = endPos - startPos;
        float lineLength = lineDirection.magnitude;
        int numOfDots = Mathf.FloorToInt(lineLength / spacing);

        lineDirection.Normalize();

        for (int i = 0; i <= numOfDots; i++)
        {
            Vector3 dotPosition = startPos + (lineDirection * spacing * i);
            GameObject dot = Instantiate(dotPrefab, dotPosition, Quaternion.identity);
        }
    }
}
