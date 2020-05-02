using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meniKretnja : MonoBehaviour
{
    private float vrijemeUbrzavanja = 1f;
    private float maxBrzina = 1.5f;
    private Vector2 vektorKretanja;
    private float preostaloVrijeme;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        preostaloVrijeme -= Time.deltaTime;
        if (preostaloVrijeme <= 0)
        {
            vektorKretanja = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            preostaloVrijeme += vrijemeUbrzavanja;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(vektorKretanja * maxBrzina);
    }
}
