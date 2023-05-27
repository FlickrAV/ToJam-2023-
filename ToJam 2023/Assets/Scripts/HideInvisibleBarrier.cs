using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideInvisibleBarrier : MonoBehaviour
{
    private TilemapRenderer tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles= GetComponent<TilemapRenderer>();

        tiles.enabled = false;
    }
}
