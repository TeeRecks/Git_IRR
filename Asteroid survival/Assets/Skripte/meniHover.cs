using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class meniHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text opis;

    private GameObject horizontalnaCrta;
    private RectTransform horizontalnaCrtaRect;
    private Image horizontalnaCrtaBoja, vertikalnaCrtaBoja;
    private string gumbNaziv;


    void Start()
    {
        gumbNaziv = transform.GetComponent<Text>().name;

        vertikalnaCrtaBoja = GameObject.Find("CrtaUzTekst").GetComponent<Image>();

        horizontalnaCrta = GameObject.Find("CrtaOdGumba");
        horizontalnaCrtaRect = horizontalnaCrta.GetComponent<RectTransform>();
        horizontalnaCrtaBoja = horizontalnaCrta.GetComponent<Image>();

        horizontalnaCrtaBoja.color = vertikalnaCrtaBoja.color = new Color(1, 1, 1, 0);
        opis.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (gumbNaziv)
        {
            case "Igraj":
                {
                    opis.fontSize = 45;
                    opis.text = "<b>Pokreni novu igru</b>\n\n\n" +
                        "Pucaj po meteorima\n\n" +
                        "i pokušaj preživjeti što dulje";

                    horizontalnaCrta.transform.localPosition = new Vector2(-120, 0);
                    horizontalnaCrtaRect.sizeDelta = new Vector2(182, horizontalnaCrtaRect.sizeDelta.y);
                    horizontalnaCrtaBoja.color = vertikalnaCrtaBoja.color = new Color(1,1,1,1);
                    break;
                }
            case "NacinIgranja":
                {
                    opis.fontSize = 32;
                    opis.text = "<b><i>M1 (LMB)</i></b> - primarni napad\n\n" +
                        "<b><i>M2 (RMB)</i></b> - iskoristi bombu ako postane prenapeto\n\n" +
                        "<b><i>ESC</i></b> - izlaz iz igre\n\n" +
                        "Pucaj u power-up da ga aktiviraš\n\n" +
                        "<b><i>+100</i></b> - direktni pogodak\n\n" +
                        "<b><i>+15</i></b> - pogodak sa štitom\n\n" +
                        "<b><i>+5</i></b> - svake sekunde\n\n" +
                        "<b><i><color=red>Preživi!</color></i></b>";

                    horizontalnaCrta.transform.localPosition = new Vector2(-110, -100);
                    horizontalnaCrtaRect.sizeDelta = new Vector2(162, horizontalnaCrtaRect.sizeDelta.y);
                    horizontalnaCrtaBoja.color = vertikalnaCrtaBoja.color = new Color(1, 1, 1, 1);
                    break;
                }
            case "Izlaz":
                {
                    opis.fontSize = 60;
                    opis.text = "Izlaz iz igre";

                    horizontalnaCrta.transform.localPosition = new Vector2(-120, -200);
                    horizontalnaCrtaRect.sizeDelta = new Vector2(182, horizontalnaCrtaRect.sizeDelta.y);
                    horizontalnaCrtaBoja.color = vertikalnaCrtaBoja.color = new Color(1, 1, 1, 1);
                    break;
                }
            default:
                {
                    opis.text = "Greška u meniHover.cs";
                    break;
                }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        opis.text = "";
        horizontalnaCrtaBoja.color = vertikalnaCrtaBoja.color = new Color(1, 1, 1, 0);
    }
}
