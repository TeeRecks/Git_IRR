using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonasanjeUnistenogBroda : MonoBehaviour
{
    public Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.rotation = Random.Range(30f, 60f);

        rigid.velocity = (Vector2.down + Vector2.right).normalized * 5;
    }

    void Update()
    {
        rigid.rotation -= 1.5f;
    }
}
