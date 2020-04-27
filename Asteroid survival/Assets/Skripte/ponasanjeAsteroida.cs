using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeAsteroida : MonoBehaviour
{
    private Vector2 pozicija;
    private float brzinaAsteroida = 5f;
    private float TTK = 7f;

    // Start is called before the first frame update
    void Start()
    {
        //----pronaci vektor i kretati se u smjeru pozicije igraca kada se asteroid stvoren----
        pozicija = GameObject.Find("Igrac").transform.position;
        GetComponent<Rigidbody2D>().velocity = (pozicija - VratiVector2(transform.position)).normalized * brzinaAsteroida;
    }

    // Update is called once per frame
    void Update()
    {
        //----unistiti asteroid kada mu istekne vrijeme... 5-6 sekundi?----
        TTK -= Time.deltaTime;
        if (TTK <= 0)
        {
            UnistiAsteroid();
        }

        //----updateati poziciju sve dok ne istekne vrijeme asteroidu----
        //transform.position += pozicija * brzinaAsteroida * Time.deltaTime;                                        KRIVO
        //transform.position += (pozicija - transform.position).normalized * brzinaAsteroida * Time.deltaTime;      KRIVO
        //GetComponent<Rigidbody2D>().velocity = (pozicija).normalized * 5;                                         SKORO - ne radi ako igrac stoji na 0,0 - ne baca u dobru stranu
        //  komentar---- ne koristiti rigidbody .velocity u update...

        
        
    }

    private void UnistiAsteroid()
    {
        Destroy(gameObject);
        //----stvori efekt eksplozije ili prah *puf*----
    }

    private Vector2 VratiVector2(Vector3 vektor)
    {
        return new Vector2(vektor.x, vektor.y);
    }
}
