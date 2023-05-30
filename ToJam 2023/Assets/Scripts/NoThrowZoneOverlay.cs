using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoThrowZoneOverlay : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private LimbDetatchMenu limbMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();

        limbMenuScript = GameObject.Find("Limb Manager").gameObject.GetComponent<LimbDetatchMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        tilemapRenderer.enabled = limbMenuScript.inThrowMode;
    }
}
