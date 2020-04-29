using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeProjektila : MonoBehaviour
{
    private float brzinaProjektila = 10f;
    private float TTK = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;
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

    public void UnistiProjektil()
    {
        Destroy(gameObject);
    }
}
