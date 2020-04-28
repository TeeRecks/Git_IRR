using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeStita : MonoBehaviour
{
    private Renderer rend;

    private int maxZdravlje = 4;
    private int trenutnoZdravlje = 4;

    //testni
    private bool wave = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            wave = false;
            trenutnoZdravlje--;
            OsvjeziStit();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (wave)
            {
                wave = false;
                trenutnoZdravlje = maxZdravlje;
                OsvjeziStit();
            }
            else
            {
                wave = true;
                trenutnoZdravlje = maxZdravlje;
            }
        }

        if (wave)
        {
            //za invulnerability? ide iz cyan u plavo svakih 0.5 sekundi
            rend.material.color = Color.Lerp(new Color(0, 1, 1), new Color(0, 0, 1), Mathf.PingPong(Time.time, 1));
        }
    }

    private void OsvjeziStit()
    {
        float x = (float)trenutnoZdravlje / maxZdravlje;
        rend.material.color = Color.Lerp(Color.red, Color.green, x);
        if (x == 0)
        {
            rend.material.color = new Color(0,0,0,0);
        }
    }

    //on trigger ili collision ubaciti iz Update()
    //koristiti Color.Lerp(Color.)
}
