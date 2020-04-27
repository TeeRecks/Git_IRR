using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeProjektila : MonoBehaviour
{
    private Vector2 pozicija;
    private float brzinaProjektila = 10f;
    private float TTK = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pozicija = GameObject.Find("Igrac").transform.position;
        GetComponent<Rigidbody2D>().velocity = (ciljnik - VratiVector2(transform.position)).normalized * brzinaProjektila;
    }

    // Update is called once per frame
    void Update()
    {
        TTK -= Time.deltaTime;
        if (TTK <= 0)
        {
            UnistiProjektil();
        }
    }

    private Vector2 VratiVector2(Vector3 vektor)
    {
        return new Vector2(vektor.x, vektor.y);
    }

    private void UnistiProjektil()
    {
        Destroy(gameObject);
    }
}
