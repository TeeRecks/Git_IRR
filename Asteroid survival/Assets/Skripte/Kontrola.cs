using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kontrola : MonoBehaviour
{
    public float brzinaKretanja = 3f;

    public GameObject prijateljskiProjektil;

    public Image kuglaSprite;
    public Image zrakaSprite;
    public Image repetitorSprite;
    public Image sacmaSprite;

    public int maxEnergija = 10;
    public int trenutnaEnergija = 10;
    public float brzinaPunjenjaEnergije = 1f;
    private Image energijaI;
    private Text energijaT;

    private int modPucanja = 0;
    private int snagaPucanja = 0;

    private int maxZdravlje = 4;
    private int trenutnoZdravlje = 4;
    private Image zdravljeI;
    private Text zdravljeT;

    private int maxBombi = 5;
    private int brojBombi = 1;
    private Text bombeT;

    private float vrijeme = 0;

    private Master master;

    // Start is called before the first frame update
    void Start()
    {
        //----treba updateati bar sa hp-om, bombama... energija je napravljen
        energijaI = GameObject.Find("ENERGYbar").GetComponent<Image>();
        energijaT = GameObject.Find("ENERGYvrijednost").GetComponent<Text>();
        OsvjeziEnergiju();

        zdravljeI = GameObject.Find("HPbar").GetComponent<Image>();
        zdravljeT = GameObject.Find("HPvrijednost").GetComponent<Text>();
        OsvjeziZdravlje();

        bombeT = GameObject.Find("BOMBvrijednost").GetComponent<Text>();
        OsvjeziBombe();

        master = GameObject.Find("Master").GetComponent<Master>();
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
                        PucajZraku();
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
            master.Izlaz();
        }
    }

    private void OsvjeziEnergiju()
    {
        energijaI.fillAmount = (float)trenutnaEnergija / maxEnergija;
        energijaT.text = trenutnaEnergija.ToString();
    }

    private void OsvjeziZdravlje()
    {
        zdravljeI.fillAmount = (float)trenutnoZdravlje / maxZdravlje;
        zdravljeT.text = trenutnoZdravlje.ToString();
    }

    private void OsvjeziBombe()
    {
        bombeT.text = brojBombi.ToString();
    }

    //mod 0
    private void PucajKuglu()
    {
        if (trenutnaEnergija > 0)
        {
            Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);

            //test scatter vektora
            Debug.DrawLine(GameObject.Find("Igrac").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.white, 2);
            Vector3 novi_vektor = Quaternion.AngleAxis(15, Vector3.forward) * Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(GameObject.Find("Igrac").transform.position, novi_vektor, Color.red, 2);


            trenutnaEnergija--;
            OsvjeziEnergiju();
        }
    }

    //mod 1
    private void PucajZraku()
    {
        //do stuff make zap
        OsvjeziEnergiju();
    }

    //mod 2
    private void PucajRepetitor()
    {
        //mozda se stvori gameobject sa skriptom koja puca burst i unisti se kada zavrsi
        //ovdje bi se samo provjeravalo da li postoji gameobject koji puca taj određeni tip oruzja (u ovom slucaju burst - repetitor)
        if (trenutnaEnergija > 2)
        {
            GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
            projektil.name = "Projektil-repetitor";
            trenutnaEnergija -= 2;
            OsvjeziEnergiju();
        }
    }

    //mod 3
    private void PucajSacmu()
    {
        //do stuff make kerblam
        OsvjeziEnergiju();
    }

    private void PucajBombu()
    {
        //do stuff make skadsoosh
        brojBombi--;
        OsvjeziBombe();
    }

    public void SmanjiZdravlje()
    {
        trenutnoZdravlje--;
        OsvjeziZdravlje();
        if (trenutnoZdravlje == 0)
        {
            master.Kraj();
            //da li treba biti gameObject u sredini?
            //Kontroler_Igre direktor = GameObject.Find("Direktor").gameObject.GetComponent<Kontroler_Igre>();

        }
    }
}
