using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;
    [SerializeField]
    GameObject SpawnLocation;

    List<Bullet> bullets = new List<Bullet>();

    public static BulletManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start() {
        SpawnBullet(SpawnLocation.transform.position, new Vector2(5, 5), new Vector2(1, 1));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            NextStep();
        }
    }

    public static void SpawnBullet(Vector3 SpawnPosition, Vector2 SpawnIndex, Vector2 velocity) {
        var bullet = Instantiate(Instance.BulletPrefab, SpawnPosition, Quaternion.identity).GetComponent<Bullet>();

        bullet.Init(SpawnIndex, velocity);

        Instance.bullets.Add(bullet);
    }

    public static void NextStep() {
        foreach (Bullet bul in Instance.bullets) {
            Instance.StartCoroutine(bul.NextStep());
        }
    }
}
