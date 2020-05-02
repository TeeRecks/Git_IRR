using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KonacanRezultat : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("bodovi").ToString();
    }

    void Update()
    {
        //mozda da se mijenja boje?
    }
}
