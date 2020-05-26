using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
    private Text plocaBodova;
    private int bodovi;

    private float vrijeme;

    public AudioClip gameMuzika;
    private AudioSource music;

    public GameObject unisteniIgrac;


    void Start()
    {
        string scena = SceneManager.GetActiveScene().name;
        if (scena == "Igra")
        {
            plocaBodova = GameObject.Find("SCORE").GetComponent<Text>();
            plocaBodova.text = "0";

            music = GetComponent<AudioSource>();
            music.clip = gameMuzika;
            music.loop = true;
            music.volume = GlobalneVarijable.Volumen;
            music.Play();
        }
        else if (scena == "Kraj")
        {
            //instanciraj i promjeni mu smjer
            Instantiate(unisteniIgrac, new Vector3(-10, 10, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        vrijeme += Time.deltaTime;
    }

    public void UvecajBodove(int dobiveni_bodovi)
    {
        bodovi += dobiveni_bodovi;
        plocaBodova.text = bodovi.ToString();
    }

    public void Meni()
    {
        GlobalneVarijable.Bodovi = 0;
        SceneManager.LoadScene("Meni");
    }

    public void Igra()
    {
        SceneManager.LoadScene("Igra");
    }

    public void Kraj()
    {
        music.Stop();
        int konacniBodovi = (Mathf.FloorToInt(vrijeme) * 5) + bodovi;
        GlobalneVarijable.Bodovi = konacniBodovi;
        GlobalneVarijable.Vrijeme = Mathf.FloorToInt(vrijeme);

        SceneManager.LoadScene("Kraj");
    }

    public void Izlaz()
    {
        Application.Quit();
    }

    public void PromjeniVolumen()
    {

    }
}
