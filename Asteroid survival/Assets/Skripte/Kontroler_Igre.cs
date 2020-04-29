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
    private float udaljenost = 11;

    // Start is called before the first frame update
    void Start()
    {
        // postavljanje brzine spawnanja asteroida
        igrac = GameObject.Find("Igrac");
        brzinaSpawnanjaAsteroida = 1 / brzinaSpawnanjaAsteroida;

        // postavljanje početnog HP-a igrača

    }

    // Update is called once per frame
    void Update()
    {
        brojac += Time.deltaTime;
        if (brojac > brzinaSpawnanjaAsteroida)
        {
            brojac = 0;
            pozicija = igrac.transform.position;

            //pokusaj 1
            //random četvrtina kruga (gornja, -45 do 45 stupnjeva)
            //float x = Random.Range(-11.0f, 11.0f);
            //float y = Mathf.Sqrt(121 - Mathf.Pow(x, 2));

            //if (Random.value > 0.5f) x *= -1;
            //if (Random.value > 0.5f) y *= -1;

            //pokusaj 2
            float kut = Random.Range(0f, 360f);
            //float x = Random.Range(-11f, 11f);
            //Mathf.Cos(kut) = x / 11;
            float b = Mathf.Cos(kut) * udaljenost;
            //121 = Mathf.Pow(a, 2) + Mathf.Pow(b, 2);
            //121 - Mathf.Pow(b, 2) = Mathf.Pow(a, 2);
            float a = Mathf.Sqrt(udaljenost * udaljenost - Mathf.Pow(b, 2));
            if (Random.value > 0.5f) a *= -1;
            Instantiate(asteroid_prefab, pozicija + new Vector3(a, b, 0), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        }
    }
}
