using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
    private Text plocaBodova;
    private int bodovi;

    // Start is called before the first frame update
    void Start()
    {
        plocaBodova = GameObject.Find("SCORE").GetComponent<Text>();
        plocaBodova.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UvecajBodove(int dobiveni_bodovi)
    {
        bodovi += dobiveni_bodovi;
        plocaBodova.text = bodovi.ToString();
    }

    public void Meni()
    {
        SceneManager.LoadScene("Meni");
    }

    public void Igra()
    {
        SceneManager.LoadScene("Igra");
    }

    public void Kraj()
    {
        SceneManager.LoadScene("Kraj");
    }

    public void Izlaz()
    {
        Application.Quit();
    }
}
