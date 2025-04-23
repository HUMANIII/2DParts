using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Tilemap tileMaps;

    [SerializeField] private GameObject overlayPrefab;
    [SerializeField] private GameObject overlayContainer;

    public Dictionary<Vector2Int, OverlayTile> map { get; private set; }

    void Start()
    {
        //var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
        map = new Dictionary<Vector2Int, OverlayTile>();
        BoundsInt bounds = tileMaps.cellBounds;

        for (int z = bounds.max.z - 1; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    var cellPosition = new Vector3Int(x, y, z);
                    //Debug.Log($"셀 좌표: {cellPosition}"); // 좌표 확인
                    //Debug.Log(tileMaps.GetTile(cellPosition) ? $"셀 좌표: {cellPosition}" : $" 없음 {x}, {y}, {z}"); // 해당 위치의 타일 정보
                    if (tileMaps.GetTile(cellPosition) && !map.ContainsKey(new Vector2Int(x, y)))
                    {
                        var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMaps.GetCellCenterWorld(new Vector3Int(x, y, z));
                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y,
                            cellWorldPosition.z + 1);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder =
                            tileMaps.GetComponent<TilemapRenderer>().sortingOrder;
                        var tileComp = overlayTile.gameObject.GetComponent<OverlayTile>();
                        tileComp.gridLocation = new Vector3Int(x, y, z);

                        map.Add(new Vector2Int(x, y), tileComp);
                    }
                }
            }
        }
    }

    public List<OverlayTile> GetSurroundingTiles(Vector2Int originTile)
    {
        var surroundingTiles = new List<OverlayTile>();


        Vector2Int TileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        TileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
        if (map.ContainsKey(TileToCheck))
        {
            if (Mathf.Abs(map[TileToCheck].transform.position.z - map[originTile].transform.position.z) <= 1)
                surroundingTiles.Add(map[TileToCheck]);
        }

        return surroundingTiles;
    }
}