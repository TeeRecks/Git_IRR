using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ponasanjePickup : MonoBehaviour
{
    private int tipPickup;
    private float TTK = 7f;
    private Renderer rend;

    public AudioClip pokupi;

    public Sprite[] slike;
    //0 - kugla
    //1 - zraka / laser
    //2 - sacma / 3-shot
    //3 - repetitor / burst
    //4 - recharge
    //5 - invulnerability
    //6 - hp / repair
    //7 - bomb


    void Start()
    {
        rend = GetComponent<Renderer>();
        tipPickup = Random.Range(0, 8);

        //jer nije implementirana zraka, ponovno generiraj broj sve dok ne dobije mod koji nije zraka (broj 1)
        while (tipPickup == 1)
        {
            tipPickup = Random.Range(0, 7);
        }

        GetComponent<SpriteRenderer>().sprite = slike[tipPickup];
        GetComponent<Collider2D>().enabled = false;

        //Debug.Log("PICKUP - " + transform.position.x + ", " + transform.position.y);
    }

    void Update()
    {
        TTK -= Time.deltaTime;
        if (TTK <= 6.5f)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        if (TTK <= 2)
        {
            rend.material.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, -4), Mathf.PingPong(Time.time, 0.25f));
        }
        if (TTK <= 0)
        {
            UnistiPickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnistiPickup();
        PokupiAudio();
        Kontrola igrac = GameObject.Find("Igrac").GetComponent<Kontrola>();
        igrac.PromjeniMod(tipPickup);
        ponasanjeProjektila projektil = collision.gameObject.GetComponent<ponasanjeProjektila>();
        projektil.SmanjiZdravlje();
    }

    private void UnistiPickup()
    {
        Destroy(gameObject);
    }

    private void PokupiAudio()
    {
        AudioSource.PlayClipAtPoint(pokupi, transform.position);
    }
}
