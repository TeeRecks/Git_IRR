using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KonacanRezultat : MonoBehaviour
{
    private float preostaloVrijeme;
    private Color boja;
    Text rend;

    void Start()
    {
        GameObject.Find("VrijednostRezultata").GetComponent<Text>().text = GlobalneVarijable.Bodovi.ToString();
        GameObject.Find("VrijemeVrijednost").GetComponent<Text>().text = GlobalneVarijable.Vrijeme.ToString();
        GameObject.Find("UnistenoAsteroidaVrijednost").GetComponent<Text>().text = GlobalneVarijable.Asteroidi.ToString();

        rend = GetComponent<Text>();
    }

    void Update()
    {
        if (preostaloVrijeme <= Time.deltaTime)
        {
            rend.color = boja;

            // start a new transition
            boja = new Color(Random.value, Random.value, Random.value);
            preostaloVrijeme = 0.5f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            rend.color = Color.Lerp(rend.color, boja, Time.deltaTime / preostaloVrijeme);

            // update the timer
            preostaloVrijeme -= Time.deltaTime;
        }
    }
}
