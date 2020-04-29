using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeStita : MonoBehaviour
{
    private Renderer rend;

    private int maxZdravlje = 4;
    private int trenutnoZdravlje = 4;

    private bool ukljucenRegeneracija = false;

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
        //testni gumb
        if (Input.GetKeyDown(KeyCode.Q) && trenutnoZdravlje > 0)
        {
            wave = false;
            trenutnoZdravlje--;
            OsvjeziStit();
        }

        //testni gumb
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (wave)
            {
                wave = false;
                trenutnoZdravlje = maxZdravlje;
                gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                OsvjeziStit();
            }
            else
            {
                wave = true;
                gameObject.GetComponent<EdgeCollider2D>().enabled = true;
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

    public void SmanjiStit()
    {
        trenutnoZdravlje--;
        OsvjeziStit();
        if (trenutnoZdravlje == 0)
        {
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }

    //on trigger ili collision ubaciti iz Update()
    //koristiti Color.Lerp(Color.)
}
