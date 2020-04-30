using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeProjektila : MonoBehaviour
{
    private float TTK = 2f;

    // Update is called once per frame
    void Update()
    {
        TTK -= Time.deltaTime;
        if (TTK <= 0)
        {
            UnistiProjektil();
        }
    }

    public void UnistiProjektil()
    {
        Destroy(gameObject);
    }
}
