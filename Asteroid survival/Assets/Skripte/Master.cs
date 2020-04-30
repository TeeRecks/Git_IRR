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

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Igra")
        {
            plocaBodova = GameObject.Find("SCORE").GetComponent<Text>();
            plocaBodova.text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        vrijeme += Time.deltaTime;
    }

    public void UvecajBodove(int dobiveni_bodovi)
    {
        bodovi += dobiveni_bodovi;
        plocaBodova.text = bodovi.ToString();
    }

    public void StvoriPickup(int pickup)
    {

    }

    public void Meni()
    {
        PlayerPrefs.SetInt("bodovi", 0);
        SceneManager.LoadScene("Meni");
    }

    public void Igra()
    {
        SceneManager.LoadScene("Igra");
    }

    public void Kraj()
    {
        int konacniBodovi = (Mathf.FloorToInt(vrijeme) * 5) + bodovi;
        PlayerPrefs.SetInt("bodovi", konacniBodovi);
        PlayerPrefs.SetInt("vrijeme", Mathf.FloorToInt(vrijeme));
        SceneManager.LoadScene("Kraj");
    }

    public void Izlaz()
    {
        Application.Quit();
    }
}
