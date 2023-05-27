using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCallOnHitMax;
    [SerializeField] private string[] functionsToCallOnHitMax;

    private int totalCount;
    private bool hasHitMax = false;

    public int maxCount;

    private void Update()
    {
        if (totalCount == maxCount)
        {
            hasHitMax = true;
            OnHitMax();
        }
        if (hasHitMax)
        {
            if (totalCount < maxCount)
            {
                OnLeaveMax();
            }
        }
    }

    private void Add()
    {
        totalCount += 1;
    }
    private void Subtract()
    {
        if (totalCount > -1)
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
            objectsToCallOnHitMax[i].SendMessage(functionsToCallOnHitMax[i]);
        }
    }
}
