using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ponasanjePickup : MonoBehaviour
{
    private int tipPickup;
    private float TTK = 5f;
    private Renderer rend;

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

        //Debug.Log("PICKUP - " + transform.position.x + ", " + transform.position.y);
    }

    void Update()
    {
        TTK -= Time.deltaTime;
        if (TTK <= 0)
        {
            UnistiPickup();
        }

        if (TTK <= 2)
        {
            rend.material.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 1), Mathf.PingPong(Time.time, 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnistiPickup();
        Kontrola igrac = GameObject.Find("Igrac").GetComponent<Kontrola>();
        igrac.PromjeniMod(tipPickup);
        ponasanjeProjektila projektil = collision.gameObject.GetComponent<ponasanjeProjektila>();
        projektil.SmanjiZdravlje();
    }

    private void UnistiPickup()
    {
        Destroy(gameObject);
    }
}
