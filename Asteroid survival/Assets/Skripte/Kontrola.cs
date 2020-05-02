using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kontrola : MonoBehaviour
{
    public float brzinaKretanja = 3f;

    public GameObject prijateljskiProjektil;
    private float brzinaProjektila = 10f;

    public Sprite[] mod_UI;
    private int modPucanja = 0;
    private int snagaPucanja = 1;

    public int maxEnergija = 20;
    private int trenutnaEnergija;
    public float brzinaPunjenjaEnergije = 1f;
    private Image energijaI;
    private Text energijaT;

    private int maxZdravlje = 4;
    private int trenutnoZdravlje;
    private Image zdravljeI;
    private Text zdravljeT;

    private int maxBombi = 5;
    private int trenutnoBombi = 1;
    private Text bombeT;

    private float vrijeme = 0;

    private Master master;

    private Image bljesakBombe;

    // Start is called before the first frame update
    void Start()
    {
        trenutnaEnergija = maxEnergija;
        trenutnoZdravlje = maxZdravlje;

        bljesakBombe = GameObject.Find("BljesakBombe").GetComponent<Image>();

        energijaI = GameObject.Find("ENERGYbar").GetComponent<Image>();
        energijaT = GameObject.Find("ENERGYvrijednost").GetComponent<Text>();
        OsvjeziEnergiju();

        zdravljeI = GameObject.Find("HPbar").GetComponent<Image>();
        zdravljeT = GameObject.Find("HPvrijednost").GetComponent<Text>();
        OsvjeziZdravlje();

        bombeT = GameObject.Find("BOMBvrijednost").GetComponent<Text>();
        OsvjeziBombe();

        master = GameObject.Find("Master").GetComponent<Master>();

        GameObject.Find("TYPEslika").GetComponent<Image>().sprite = mod_UI[0];

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && trenutnaEnergija >= snagaPucanja && GameObject.Find("Pucanje") == null)
        {

            trenutnaEnergija -= snagaPucanja;
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
                        PucajSacmu();
                        break;
                    }
                case 3:
                    {
                        StartCoroutine(PucajRepetitor());
                        break;
                    }
                default:
                    {
                        Debug.LogError("Nemoguce pucati - krivi mod pucanja");
                        break;
                    }
            }
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

    private void NapuniZdravlje()
    {
        trenutnoZdravlje = maxZdravlje;
        OsvjeziZdravlje();
    }

    private void OsvjeziBombe()
    {
        bombeT.text = trenutnoBombi.ToString();
    }

    private void PucajBombu()
    {
        //da li treba biti gameObject u sredini?
        Kontroler_Igre direktor = GameObject.Find("Direktor").gameObject.GetComponent<Kontroler_Igre>();

        //unisti sve meteore
        foreach (Transform child in direktor.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        StartCoroutine(Bljesak());
        direktor.PauzirajMeteore();

        if (trenutnoBombi > 0) trenutnoBombi--;
        OsvjeziBombe();
    }

    private void DodajBombu()
    {
        if (trenutnoBombi < maxBombi)
        {
            trenutnoBombi++;
            OsvjeziBombe();
        }
    }

    private IEnumerator Bljesak()
    {
        bljesakBombe.color = new Color(1, 1, 1, 1);
        float alpha = bljesakBombe.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 3.5f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, t));
            bljesakBombe.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    public void SmanjiZdravlje()
    {
        trenutnoZdravlje--;
        OsvjeziZdravlje();
        if (trenutnoZdravlje == 0)
        {
            master.Kraj();
        }
    }

    private void PucajKuglu()
    {
        GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
        projektil.transform.localScale = new Vector2(2 * snagaPucanja, 2 * snagaPucanja);
        Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        projektil.GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;

        projektil.GetComponent<ponasanjeProjektila>().PromjeniZdravlje(snagaPucanja * 2);

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

        for (int i = 0; i < (2 * snagaPucanja); i++)
        {
            GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
            Vector2 ciljnik = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            projektil.GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void PucajSacmu()
    {
        InstancirajSacmu(0);

        InstancirajSacmu(7);
        InstancirajSacmu(-7);

        if (snagaPucanja > 1)
        {
            InstancirajSacmu(14);
            InstancirajSacmu(-14);
        }

        if (snagaPucanja > 2)
        {
            InstancirajSacmu(21);
            InstancirajSacmu(-21);
        }
    }

    private void InstancirajSacmu(int kut)
    {
        Vector2 ciljnik = Quaternion.AngleAxis(kut, new Vector3(transform.position.x, transform.position.y, 1)) * Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject projektil = Instantiate(prijateljskiProjektil, transform.position, Quaternion.identity);
        projektil.GetComponent<Rigidbody2D>().velocity = (ciljnik - new Vector2(transform.position.x, transform.position.y)).normalized * brzinaProjektila;
        Debug.Log(ciljnik + "##################" + new Vector2(transform.position.x, transform.position.y));
    }

    private void PucajZraku()
    {

    }

    public void PromjeniMod(int mod)
    {
        //0-3 - mod pucanja
        if (mod >= 0 && mod <=3)
        {
            if (modPucanja == mod && snagaPucanja < 3)
            {
                snagaPucanja++;
            }
            else if (snagaPucanja == 3)
            {
                trenutnaEnergija = maxEnergija;
            }
            else if (modPucanja != mod)
            {
                modPucanja = mod;
                snagaPucanja = 1;
                GameObject.Find("TYPEslika").GetComponent<Image>().sprite = mod_UI[mod];
            }
        }
        else if (mod == 4)
        {
            //4 - recharge
            for(int indeks = 0; indeks < 4; indeks++)
            {
                transform.GetChild(indeks).GetComponent<ponasanjeStita>().NapuniStit();
            }
            trenutnaEnergija = maxEnergija;
        }
        else if (mod == 5)
        {
            //5 - invulnerability
            for (int indeks = 0; indeks < 4; indeks++)
            {
                ponasanjeStita stit = transform.GetChild(indeks).GetComponent<ponasanjeStita>();
                stit.NapuniStit();
                stit.StaviNeunistivost();
            }
        }
        else if (mod == 6)
        {
            //6 - hp
            NapuniZdravlje();
        }
        else if (mod == 7)
        {
            //7 - bombe
            DodajBombu();
        }
    }
}
