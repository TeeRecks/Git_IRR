using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kontrola : MonoBehaviour
{
    public float brzinaKretanja = 3f;

    public GameObject prijateljskiProjektil;

    public int maxEnergija = 10; //koliko energije igrac ukupno ima dostupno, velicina spremnika
    public int trenutnaEnergija = 10; //koliko trenutno energije ima igrac
    public float brzinaPunjenjaEnergije = 1f; //koliko sekundi da se napuni jedna energija (u sekundama)
    private Image energijaI; //slika energije ili "bara" koji se smanjuje ili povecava ovisno o dostupnoj energiji
    private Text energijaT;

    private int modPucanja = 0;
    private int snagaPucanja = 0;

    private int maxZdravlje = 10;
    private int trenutnoZdravlje = 4;
    private Image zdravljeI;
    private Text zdravljeT;

    private int maxBombi = 5;
    private int brojBombi = 1;

    private float vrijeme = 0;

    // Start is called before the first frame update
    void Start()
    {
        //----treba updateati bar sa hp-om, bombama... energija je napravljen
        energijaI = GameObject.Find("ENERGYbar").GetComponent<Image>();
        energijaT = GameObject.Find("ENERGYvrijednost").GetComponent<Text>();
        OsvjeziEnergiju();

        zdravljeI = GameObject.Find("ENERGYbar").GetComponent<Image>();
        zdravljeT = GameObject.Find("ENERGYvrijednost").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        vrijeme += Time.deltaTime;
        if (vrijeme > 1f)
        {
            vrijeme = 0;
            if (trenutnaEnergija < maxEnergija)
            {
                trenutnaEnergija++;
                OsvjeziEnergiju();
            }
        }

        //kretnja WASD i strelice
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * brzinaKretanja * Time.deltaTime;
        }
        if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * brzinaKretanja * Time.deltaTime;
        }
        if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * brzinaKretanja * Time.deltaTime;
        }
        if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * brzinaKretanja * Time.deltaTime;
        }

        //m1 pucanje
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (modPucanja)
            {
                case 0:
                    {
                        PucajKuglu();
                        break;
                    }
                case 1:
                    {
                        PucajZaku();
                        break;
                    }
                case 2:
                    {
                        PucajRepetitor();
                        break;
                    }
                case 3:
                    {
                        PucajSacmu();
                        break;
                    }
                default:
                    {
                        Debug.LogError("Nemoguce pucati - krivi mod pucanja");
                        break;
                    }
            }




            //dodati vise vrsta napada..ball, x second beam, burst, shotgun (spread)
            //mozda svaki sa vise (oko 3) jacine ovisno koliko se istih upgradeova pokupi
            //ball - pocetna?, mijenja se velicina ili mozda koliko asteroida moze unistiti prije nego nestane (2, 4, 6)? ili kombinacija oba?
            //x second beam - 0.2, 0.5, 1.0 sekunda
            //burst - 2, 4, 6 loptica jedna za drugom
            //shotgun - 3, 5, 7 loptica jedna do druge

        }

        //m2 pucanje
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PucajBombu();
        }

        //ESC exit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OsvjeziEnergiju()
    {
        energijaI.fillAmount = (float)trenutnaEnergija / maxEnergija;
        energijaT.text = trenutnaEnergija.ToString();
    }

    private void OsvjeziZdravlje()
    {

    }

    //mod 0
    private void PucajKuglu()
    {
        if (trenutnaEnergija > 0)
        {
            GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
            projektil.name = "Projektil";
            trenutnaEnergija--;
            OsvjeziEnergiju();
        }
    }

    //mod 1
    private void PucajZaku()
    {

    }

    //mod 2
    private void PucajRepetitor()
    {

    }

    //mod 3
    private void PucajSacmu()
    {

    }

    private void PucajBombu()
    {

    }
}
