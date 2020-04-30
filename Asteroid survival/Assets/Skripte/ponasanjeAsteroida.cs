using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeAsteroida : MonoBehaviour
{
    private Vector2 pozicija;
    private float brzinaAsteroida = 5f;
    private float TTK = 7f;
    private float dropChance = 0.03f;

    private Master master;

    // Start is called before the first frame update
    void Start()
    {
        //----pronaci vektor i kretati se u smjeru pozicije igraca kada se asteroid stvoren----
        pozicija = GameObject.Find("Igrac").transform.position;
        GetComponent<Rigidbody2D>().velocity = (pozicija - VratiVector2(transform.position)).normalized * brzinaAsteroida;
        master = GameObject.Find("Master").GetComponent<Master>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        //+bodovi


        //šansa drop powerup
        if (Random.value < dropChance)
        {
            int odabirPickup = Random.Range(0, 7);
            master.StvoriPickup(odabirPickup);
        }

        if (collision.GetComponent<ponasanjeProjektila>() != null)
        {
            ponasanjeProjektila projektil = collision.gameObject.GetComponent<ponasanjeProjektila>();
            //dodaj 20
            master.UvecajBodove(100);
            projektil.UnistiProjektil();
        }
        if (collision.GetComponent<ponasanjeStita>() != null)
        {
            ponasanjeStita stit = collision.gameObject.GetComponent<ponasanjeStita>();
            //dodaj 5
            master.UvecajBodove(15);
            stit.SmanjiStit();
        }
        if (collision.GetComponent<Kontrola>() != null)
        {
            Kontrola igrac = collision.gameObject.GetComponent<Kontrola>();
            igrac.SmanjiZdravlje();
        }
        UnistiAsteroid();
    }

    private void UnistiAsteroid()
    {

        Destroy(gameObject);
        //----stvori efekt eksplozije ili prah *puf* + zvuk----
    }

    private Vector2 VratiVector2(Vector3 vektor)
    {
        return new Vector2(vektor.x, vektor.y);
    }
}
