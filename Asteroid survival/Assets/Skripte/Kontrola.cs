using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kontrola : MonoBehaviour
{
    public float brzinaKretanja = 3f;

    public GameObject prijateljskiProjektil;
    private float brzinaProjektila = 10f;

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
    private int snagaPucanja = 1;
    private int cijenaPucanja = 1;

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

        //testiranje mijenjanje boje
        //GameObject.Find("testP").GetComponent<Renderer>().material.color = Color.yellow;
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && trenutnaEnergija >= cijenaPucanja && GameObject.Find("Pucanje") == null)
        {
            
            trenutnaEnergija -= cijenaPucanja;
            OsvjeziEnergiju();
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
                        StartCoroutine(PucajRepetitor());
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

    private void PucajKuglu()
    {
        GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
        Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        projektil.GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;

        //test scatter vektora za sacmu
        /*
        Debug.DrawLine(GameObject.Find("Igrac").transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.white, 2);
        Vector3 novi_vektor = Quaternion.AngleAxis(15, Vector3.forward) * Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(GameObject.Find("Igrac").transform.position, novi_vektor, Color.red, 2);
        */
    }

    IEnumerator PucajRepetitor()
    {
        Vector2 igrac = GameObject.Find("Igrac").transform.position;

        for (int i = 0; i < 2; i++)
        {
            GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
            Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            projektil.GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void PucajSacmu()
    {
        GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
        Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        projektil.GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;

        Vector2 novi_vektor = Quaternion.AngleAxis(15, Vector3.forward) * Camera.main.ScreenToWorldPoint(Input.mousePosition);
        projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
        projektil.GetComponent<Rigidbody2D>().velocity = (novi_vektor - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;

        novi_vektor = Quaternion.AngleAxis(-15, Vector3.forward) * Camera.main.ScreenToWorldPoint(Input.mousePosition);
        projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
        projektil.GetComponent<Rigidbody2D>().velocity = (novi_vektor - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;
    }

    private void PucajZraku()
    {

    }

    public void PromjeniMod(int mod)
    {
        if (modPucanja == mod && snagaPucanja < 3)
        {
            snagaPucanja++;
        }
        else if (modPucanja != mod)
        {
            modPucanja = mod;
            snagaPucanja = 1;
            trenutnaEnergija = maxEnergija;
        }
    }
}
