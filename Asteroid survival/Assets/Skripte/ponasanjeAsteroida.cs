using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeAsteroida : MonoBehaviour
{
    private Vector2 pozicija;
    private float brzinaAsteroida = 5f;
    private float TTK = 7f;

    public float dropChance = 0.1f;
    public GameObject pickup;

    private Master master;

    public AudioClip asteroidUnisten;


    void Start()
    {
        pozicija = GameObject.Find("Igrac").transform.position;
        GetComponent<Rigidbody2D>().velocity = (pozicija - VratiVector2(transform.position)).normalized * brzinaAsteroida;
        master = GameObject.Find("Master").GetComponent<Master>();
    }

    void Update()
    {
        TTK -= Time.deltaTime;
        if (TTK <= 0)
        {
            UnistiAsteroid();
        }

        //transform.position += pozicija * brzinaAsteroida * Time.deltaTime;                                        KRIVO
        //transform.position += (pozicija - transform.position).normalized * brzinaAsteroida * Time.deltaTime;      KRIVO
        //GetComponent<Rigidbody2D>().velocity = (pozicija).normalized * 5;                                         SKORO - ne radi ako igrac stoji na 0,0 - ne baca u dobru stranu
        //  komentar---- ne koristiti rigidbody .velocity u update...
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("projektil"))
        {
            ponasanjeProjektila projektil = collision.gameObject.GetComponent<ponasanjeProjektila>();

            //šansa drop powerup
            if (Random.value <= dropChance)
            {
                Instantiate(pickup, transform.position, Quaternion.identity);
            }

            //dodaj 100
            master.UvecajBodove(100);
            projektil.SmanjiZdravlje();
        }
        if (collision.gameObject.CompareTag("stit"))
        {
            ponasanjeStita stit = collision.gameObject.GetComponent<ponasanjeStita>();
            //dodaj 15
            master.UvecajBodove(15);
            stit.SmanjiStit();
        }
        if (collision.gameObject.CompareTag("igrac"))
        {
            Kontrola igrac = collision.gameObject.GetComponent<Kontrola>();
            igrac.SmanjiZdravlje();
        }
        //vrlo vjerovatno nije točno kad se odjednom 2 asteroida unište, ali dovoljno dobro
        GlobalneVarijable.Asteroidi++;
        UnistiAsteroid();
    }

    private void UnistiAsteroid()
    {
        AsteroidAudio();
        Destroy(gameObject);
    }

    private Vector2 VratiVector2(Vector3 vektor)
    {
        return new Vector2(vektor.x, vektor.y);
    }

    private void AsteroidAudio()
    {
        AudioSource.PlayClipAtPoint(asteroidUnisten, transform.position);
    }
}
