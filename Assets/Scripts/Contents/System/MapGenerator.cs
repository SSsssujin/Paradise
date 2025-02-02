using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Paradise.Battle
{
    public class MapGenerator : MonoBehaviour
    {
        // Temp
        [SerializeField] private Tilemap TileMap;
        [SerializeField] private Transform TileViewer;

        private async void Start()
        {
            await _CreateMap();
        }

        private Task _CreateMap()
        {
            Tilemap tilemap = TileMap;
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] tiles = tilemap.GetTilesBlock(bounds);

            // 1. 타일 로드
            // 2. 타일 생성
            // 3. Initialize (-> 리스너 등록, 알맞은 위치에 배치 등) 분리해보기
            
            for (int y = 0; y < bounds.size.y; y++)
            {
                for (int x = 0; x < bounds.size.x; x++)
                {
                    TileBase tile = tiles[x + y * bounds.size.x];

                    if (tile is null || 
                        tile.name.Equals(TileName.Obstacle) ||
                        tile.name.Equals(TileName.Gold) ||
                        tile.name.Equals(TileName.Sliver)) continue;
                    
                    Vector3Int localPosition = new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0);
                    Vector3 worldPosition = tilemap.CellToWorld(localPosition) + tilemap.cellSize / 2.0f;

                    GameObject tileObject = GameManager.Resource.Instantiate(tile.name, TileViewer);
                    tileObject.name = $"[{x}, {y}]";
                    tileObject.transform.position = worldPosition;
                }
            }
            return Task.CompletedTask;
        }
    }
}