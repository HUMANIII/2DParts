using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugTilemap : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        // 검사할 셀 좌표
        Vector3Int cellPosition = new Vector3Int(0, 0, 0);

        // Tilemap 정보 디버깅
        if (tilemap != null)
        {
            Debug.Log("타일맵 이름: " + tilemap.name);

            // 해당 좌표의 타일 존재 여부 확인
            if (tilemap.HasTile(cellPosition))
            {
                Debug.Log($"좌표 {cellPosition}에 타일이 존재합니다.");
            }
            else
            {
                Debug.Log($"좌표 {cellPosition}에 타일이 없습니다.");
            }

            // 타일 정보 출력
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null)
            {
                Debug.Log($"타일 데이터: {tile.name}");
            }
            else
            {
                Debug.Log("해당 위치에 타일 데이터 없음.");
            }
        }
        else
        {
            Debug.LogError("Tilemap을 참조할 수 없습니다.");
        }
    }
}