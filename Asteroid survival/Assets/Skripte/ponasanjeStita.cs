using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeStita : MonoBehaviour
{
    private Renderer rend;

    private int maxZdravlje = 4;
    private int trenutnoZdravlje = 4;

    private bool neunistivost = false;
    private float TTK_neunistivost = 8f;
    private float TTK = 0f;


    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = Color.green;
    }

    void Update()
    {
        if (neunistivost)
        {
            rend.material.color = Color.Lerp(new Color(0, 1, 1), new Color(0, 0, 1), Mathf.PingPong(Time.time, 1));
            TTK += Time.deltaTime;
            if (TTK >= TTK_neunistivost)
            {
                neunistivost = false;
                NapuniStit();
            }
        }

        //testni gumb
        //if (Input.GetKeyDown(KeyCode.Q) && trenutnoZdravlje > 0)
        //{
        //    wave = false;
        //    trenutnoZdravlje--;
        //    OsvjeziStit();
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (wave)
        //    {
        //        wave = false;
        //        trenutnoZdravlje = maxZdravlje;
        //        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        //        OsvjeziStit();
        //    }
        //    else
        //    {
        //        wave = true;
        //        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        //        trenutnoZdravlje = maxZdravlje;
        //    }
        //}

        //if (wave)
        //{
        //    rend.material.color = Color.Lerp(new Color(0, 1, 1), new Color(0, 0, 1), Mathf.PingPong(Time.time, 1));
        //}
    }

    public void StaviNeunistivost()
    {
        if (!neunistivost)
        {
            neunistivost = true;
        }
    }

    private void OsvjeziStit()
    {
        if (!neunistivost)
        {
            float x = (float)trenutnoZdravlje / maxZdravlje;
            rend.material.color = Color.Lerp(Color.red, Color.green, x);
            if (x == 0)
            {
                rend.material.color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void SmanjiStit()
    {
        if (!neunistivost)
        {
            trenutnoZdravlje--;
            OsvjeziStit();
            if (trenutnoZdravlje == 0)
            {
                gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            }
        }
    }

    public void NapuniStit()
    {
        trenutnoZdravlje = maxZdravlje;
        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        OsvjeziStit();
    }
}
