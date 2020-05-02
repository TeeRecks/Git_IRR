using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pozadinaParallax : MonoBehaviour
{
    private float sirina, visina, startPozicijaX, startPozicijaY;
    public GameObject kamera;
    public float parallaxEfekt;

    void Start()
    {
        startPozicijaX = transform.position.x;
        startPozicijaY = transform.position.y;
        sirina = GetComponent<SpriteRenderer>().bounds.size.x;
        visina = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float rel_X = (kamera.transform.position.x * (1 - parallaxEfekt));
        float rel_Y = (kamera.transform.position.y * (1 - parallaxEfekt));

        float udaljenost_X = (kamera.transform.position.x * parallaxEfekt);
        float udaljenost_Y = (kamera.transform.position.y * parallaxEfekt);

        transform.position = new Vector2(startPozicijaX + udaljenost_X, startPozicijaY + udaljenost_Y);
        //Debug.Log(udaljenost_X);

        if (rel_X > startPozicijaX + sirina) startPozicijaX += sirina;
        else if (rel_X < startPozicijaX - sirina) startPozicijaX -= sirina;
        else if (rel_Y > startPozicijaY + sirina) startPozicijaY += visina;
        else if (rel_Y < startPozicijaY - sirina) startPozicijaY -= visina;
    }
}
