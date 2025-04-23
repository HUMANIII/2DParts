using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChecker : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        // 타일맵 전체 범위 가져오기
        BoundsInt bounds = tilemap.cellBounds;

        // 전체 타일 확인
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    Debug.Log($"Tile found at {cellPosition}: {tile.name}");
                }
                else
                {
                    Debug.Log($"No tile at {cellPosition}");
                }
            }
        }
    }
}