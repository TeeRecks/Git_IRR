using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontroler_Igre : MonoBehaviour
{
    private float brojac = 0f;
    public GameObject asteroid_prefab;
    private GameObject igrac;
    private Vector3 pozicija;
    private float brzinaSpawnanjaAsteroida = 2f; //po sekundi
    private float udaljenost = 12;

    private bool pauzirano = false;


    void Start()
    {
        igrac = GameObject.Find("Igrac");
        brzinaSpawnanjaAsteroida = 1 / brzinaSpawnanjaAsteroida;
    }

    void Update()
    {
        brojac += Time.deltaTime;
        if (!pauzirano)
        {
            if (brojac > brzinaSpawnanjaAsteroida)
            {
                brojac = 0;
                pozicija = igrac.transform.position;
                float kut = Random.Range(0f, 360f);
                float b = Mathf.Cos(kut) * udaljenost;
                float a = Mathf.Sqrt(udaljenost * udaljenost - Mathf.Pow(b, 2));
                if (Random.value > 0.5f) a *= -1;
                Instantiate(asteroid_prefab, pozicija + new Vector3(a, b, 0), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)), transform);
            }
        }
        else if (brojac >= 4f) pauzirano = false;
    }

    public void PauzirajMeteore()
    {
        pauzirano = true;
    }
}
