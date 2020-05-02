using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeProjektila : MonoBehaviour
{
    private float TTK = 2f;
    private int zdravlje = 1;


    void Update()
    {
        TTK -= Time.deltaTime;
        if (TTK <= 0)
        {
            UnistiProjektil();
        }
    }

    public void SmanjiZdravlje()
    {
        zdravlje--;
        if (zdravlje == 0)
        {
            UnistiProjektil();
        }
    }

    public void UnistiProjektil()
    {
        Destroy(gameObject);
    }

    public void PromjeniZdravlje(int novoZdravlje)
    {
        zdravlje = novoZdravlje;
    }
}
