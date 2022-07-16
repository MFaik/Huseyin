using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    GameObject MapObject;
    [SerializeField]
    public GameObject PlayerObject;

    GameObject[][] _tileset;
    static Vector2 mapSize;

    public static MapController Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        MapInit();
        SpawnEntity(PlayerObject, new Vector2(0, 0));
        Vector2 playerpos = CalculatePositionFromIndex(0,0);
        PlayerObject.transform.position = new Vector3(playerpos.x, 1, playerpos.y);
    }

    void MapInit() {
        mapSize = new Vector2(MapObject.transform.localScale.x, MapObject.transform.localScale.z);

        _tileset = new GameObject[(int)mapSize.x][];

        for(int i = 0; i < mapSize.x; i++) {
            _tileset[i] = new GameObject[(int)mapSize.y];
        }
    }

    public bool MoveTo(Vector2 from, Vector2 to) {
        if(CheckEntity(from) && CheckValidPosition(to) && !CheckEntity(to)) {
            _tileset[(int)to.x][(int)to.y] = _tileset[(int)from.x][(int)from.y];
            _tileset[(int)from.x][(int)from.y] = null;
            return true;
        }

        return false;
    }

    public void SpawnEntity(GameObject objectRef, Vector2 pos) {
        if (!CheckEntity(pos)) {
            _tileset[(int)pos.x][(int)pos.y] = objectRef;
        } else {
            Debug.LogError("Entity Cannot Spawn At Position : " + pos.x + ":" + pos.y);
        }
    }

    public bool CheckEntity(Vector2 pos) {
        return CheckValidPosition(pos) && _tileset[(int)pos.x][(int)pos.y] != null;
    }

    public bool CheckValidPosition(Vector2 pos) {
        return pos.x >= 0 && pos.y >= 0 && pos.x < mapSize.x && pos.y < mapSize.y;
    }

    static public Vector2 CalculatePositionFromIndex(int x, int y) {
        return new Vector2(mapSize.x * -1 / 2f + 0.5f + x,
            mapSize.y * -1 / 2f + 0.5f + y);
    }

    static public Vector2 CalculatePositionFromLocation(float x, float y) {
        return new Vector2(x - mapSize.x * -1 / 2f - 0.5f,
            y - mapSize.y * -1 / 2f - 0.5f);
    }
}
