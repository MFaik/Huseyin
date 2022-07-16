using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField]
    GameObject EnemyPrefab;

    List<Enemy> enemies = new List<Enemy>();

    public static EnemyManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start() {
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnEnemy(new Vector2(5, 5));
        }
    }

    public static void SpawnEnemy(Vector2 SpawnIndex) {
        if(MapController.CheckValidPosition(SpawnIndex) && !MapController.CheckEntity(SpawnIndex)) {
            Vector2 temp = MapController.TilemapToWorldPoint((int)SpawnIndex.x, (int)SpawnIndex.y);

            Vector3 pos = new Vector3(temp.x, 1, temp.y);
            var enemy = Instantiate(Instance.EnemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>();

            MapController.SpawnEntity(enemy.gameObject, SpawnIndex);
            Instance.enemies.Add(enemy);
        } else {
            Debug.LogError("Entity Cannot Spawn At Position : " + SpawnIndex.x + ":" + SpawnIndex.y);
        }
    }

    public static IEnumerator NextStep() {
        foreach (Enemy enemy in Instance.enemies) {
            yield return enemy.NextStep();
        }
    }
}
