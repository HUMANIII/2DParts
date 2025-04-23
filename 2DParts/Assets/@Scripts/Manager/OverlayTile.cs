using System;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;

    public int F
    {
        get { return G + H; }
    }

    public bool isBlocked = false;

    public OverlayTile Previous;
    public Vector3Int gridLocation;
    public Vector2Int grid2DLocation => new(gridLocation.x, gridLocation.y);

    public List<Sprite> arrows;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] subSpriteRenderers;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        subSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideTile();
        }
    }

    public void HideTile()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    public void ShowTile()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void SetSprite(ArrowTranslator.ArrowDirection d)
    {
        if (d == ArrowTranslator.ArrowDirection.None)
            subSpriteRenderers[1].color = new Color(1, 1, 1, 0);
        else
        {
            subSpriteRenderers[1].color = new Color(1, 1, 1, 1);
            subSpriteRenderers[1].sprite = arrows[(int)d];
            subSpriteRenderers[1].sortingOrder = spriteRenderer.sortingOrder;
        }
    }
}