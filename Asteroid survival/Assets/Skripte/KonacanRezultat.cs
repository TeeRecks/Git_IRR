using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KonacanRezultat : MonoBehaviour
{
    void Start()
    {
        int konacniBodovi = GlobalneVarijable.Bodovi + (GlobalneVarijable.Vrijeme * 5);
        GameObject.Find("VrijednostRezultata").GetComponent<Text>().text = GlobalneVarijable.Bodovi.ToString();
        GameObject.Find("VrijemeVrijednost").GetComponent<Text>().text = GlobalneVarijable.Vrijeme.ToString();
        GameObject.Find("UnistenoAsteroidaVrijednost").GetComponent<Text>().text = GlobalneVarijable.Asteroidi.ToString();
    }

    void Update()
    {
        //mozda da se mijenjaju boje?
    }
}
