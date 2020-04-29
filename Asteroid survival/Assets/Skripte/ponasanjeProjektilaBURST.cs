using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponasanjeProjektilaBURST : MonoBehaviour
{
    private float brzinaProjektila = 10f;
    private float TTK = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;
        //napraviti da baca 3

        //IDEJA - Kontrola.cs stvori novi GO sa svojom skriptom
        //      - ona stvara svakih 0.1 sekundi jedan projektil, svaki put provjeravajuci di puca
        //      - nakon ispucanih 3 sama sebe unisti
        //      - ne dopustiti da se stvori novi GO ako postoji vec jedan tako da se izbjegne spamanje i da se ceka novo pucanje tek kad trenutno zavrsi
        //      - GO mora biti child igraca
        //      - ili GO za svaki tip pucanja ili jedan za sve - provjeriti kako prosjljeđivati skripti parametre, smanjila bi se kolicina skripti i dodatno je moguce u istome staviti upgradeove napada
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

    private void UnistiProjektil()
    {
        Destroy(gameObject);
    }
}
