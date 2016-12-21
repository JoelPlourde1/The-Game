using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantGrowing : MonoBehaviour
{
    public float maxSize;
    public float growFactor;
    public float waitTime;

    bool isHarvestable = false;

    void Start()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }

            isHarvestable = true;
            // reset the timer

            yield return new WaitForSeconds(waitTime);
        }
    }

    public bool Harvest()
    {
        return isHarvestable;
    }
}