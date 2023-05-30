using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCallOnHitMax;
    [SerializeField] private string[] functionsToCallOnHitMax;

    [SerializeField] private GameObject[] objectsToCallOnLeaveMax;
    [SerializeField] private string[] functionsToCallOnLeaveMax;

    private int totalCount;
    private bool hasHitMax = false;

    public int maxCount;

    private void Update()
    {
        if (totalCount == maxCount && !hasHitMax)
        {
            hasHitMax = true;
            OnHitMax();
        }

        if (totalCount < maxCount && hasHitMax)
        {
            hasHitMax = false;
            OnLeaveMax();
        }

    }

    private void Add()
    {
        totalCount += 1;
    }
    private void Subtract()
    {
        if (totalCount > -0)
            totalCount -= 1;
    }
    private void OnHitMax()
    {
        for (int i = 0; i < objectsToCallOnHitMax.Length; i++)
        {
            objectsToCallOnHitMax[i].SendMessage(functionsToCallOnHitMax[i]);
        }
    }
    private void OnLeaveMax()
    {
        for (int i = 0; i < objectsToCallOnHitMax.Length; i++)
        {
            objectsToCallOnHitMax[i].SendMessage(functionsToCallOnLeaveMax[i]);
        }
    }
}
