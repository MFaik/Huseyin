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
    void Start()
    {
        SpawnBullet(SpawnLocation.transform.position, new Vector2(5, 5), new Vector2(1, 1));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            foreach(Bullet bul in bullets) {
                StartCoroutine(bul.NextStep());
            }
        }
    }

    public void SpawnBullet(Vector3 SpawnPosition, Vector2 SpawnIndex, Vector2 velocity) {
        var bullet = Instantiate(BulletPrefab, SpawnPosition, Quaternion.identity).GetComponent<Bullet>();

        bullet.Init(SpawnIndex, velocity);

        bullets.Add(bullet);
    }
}
