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
        SpawnEnemy(new Vector2(5,5));
    }

    public static void SpawnEnemy(Vector2 SpawnIndex) {
        Vector2 temp = MapController.TilemapToWorldPoint((int)SpawnIndex.x, (int)SpawnIndex.y);

        Vector3 pos = new Vector3(temp.x, 1, temp.y);
        var enemy = Instantiate(Instance.EnemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>();

        MapController.SpawnEntity(enemy.gameObject, SpawnIndex);
        Instance.enemies.Add(enemy);
    }

    public static IEnumerator NextStep() {
        foreach (Enemy enemy in Instance.enemies) {
            yield return enemy.NextStep();
        }
    }
}
