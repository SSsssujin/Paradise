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
        private const string _groundTileName = "Ground";
        private const string _roadTileName = "Road";
        private const string _obstacleTileName = "Obstacle";

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
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int y = 0; y < bounds.size.y; y++)
            {
                for (int x = 0; x < bounds.size.x; x++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];

                    if (tile is null || 
                        tile.name.Equals(_obstacleTileName) ||
                        tile.name.Equals("Gold") ||
                        tile.name.Equals("Sliver")) continue;
                    
                    Vector3Int localPosition = new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0);
                    Vector3 worldPosition = tilemap.CellToWorld(localPosition) + tilemap.cellSize / 2.0f;

                    GameObject tileObject = GameManager.Resource.Instantiate(tile.name, TileViewer);
                    tileObject.transform.position = worldPosition;
                }
            }

            return Task.CompletedTask;
        }
    }
}