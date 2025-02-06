using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    [SerializeField] private int _height, _width;
    [SerializeField] private Dictionary<Vector2, Tile> tiles;
    protected int variant;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _tilePrefab2;
    [SerializeField] private Tile _tilePrefab3;
    [SerializeField] private Transform _camera;
    [SerializeField] private PlayerController _playerPrefab;
    private GameObject player;
    public Tile getTileAtPos(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public void generatePuzzle(int variant)
    {
        //create dictionary for background tiles
        tiles = new Dictionary<Vector2, Tile>();
        //generate background tiles

                switch (variant)
                {
                    case 1:
                        break;
                    default:
                for (int x = 0; x < _width; x++)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        if(x == 3)
                        {
                            var spawnedTile = Instantiate(_tilePrefab2, new Vector3(x, y), Quaternion.identity);
                            spawnedTile.name = $"Tile {x} {y}";
                            spawnedTile.Init(x, y);
                            tiles[new Vector2(x, y)] = spawnedTile;
                        }
                        else if(y == 3)
                        {
                            var spawnedTile = Instantiate(_tilePrefab3, new Vector3(x, y), Quaternion.identity);
                            spawnedTile.name = $"Tile {x} {y}";
                            spawnedTile.Init(x, y);
                            tiles[new Vector2(x, y)] = spawnedTile;
                        }
                        else
                        {
                            var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                            spawnedTile.name = $"Tile {x} {y}";
                            spawnedTile.Init(x, y);
                            tiles[new Vector2(x, y)] = spawnedTile;
                        }

                    }
                }
                //spawning player on a tile, player knows which tile it's on and tiles know what they have on them
                var spawnpoint = new Vector2(_width / 2, 4);
                var spawntile = GridManager.instance.getTileAtPos(spawnpoint);
                PlayerController spawnedPlayer = Instantiate(_playerPrefab, spawntile.transform.position, Quaternion.identity);
                spawnedPlayer.name = $"pelaaja";
                spawntile.occupiedUnit = spawnedPlayer;
                spawnedPlayer.setOccupiedTile(spawntile);
                spawnedPlayer.setAdjacencies(true);
                break;


                }

        GameManager.Instance.UpdateGameState(GameState.WaitForInput);
    }
    public int getVariant()
    {
        return variant;
    }
    private void GridManagerOnOnGameStateChanged(GameState state)
    {
    }
    private void Awake()
    {
        instance = this;
        variant = 0;
        _camera.transform.position = new Vector3((float)_width/2 - 0.5f, (float)_height/2 -0.5f, -10);
    }
    private void OnEnable()
    {
        //not currently used
        GameManager.OnGameStateChanged += GridManagerOnOnGameStateChanged;
    }
    private void OnDisable()
    {
        //not currently used
        GameManager.OnGameStateChanged -= GridManagerOnOnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
