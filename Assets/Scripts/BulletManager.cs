using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;

    List<Bullet> bullets = new List<Bullet>();

    public static BulletManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

<<<<<<< HEAD
    void Start() {
        SpawnBullet(SpawnLocation.transform.position, new Vector2(5, 5), new Vector2(1, 1));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            //NextStep();
        }
    }

    public static void SpawnBullet(Vector3 SpawnPosition, Vector2 SpawnIndex, Vector2 velocity) {
=======
    public static Tween SpawnBullet(Vector3 SpawnPosition, Vector2 SpawnIndex, Vector2 velocity) {
>>>>>>> c2a50ea37ba8e36bb0cc67f8efc388b7b2e9dd3d
        var bullet = Instantiate(Instance.BulletPrefab, SpawnPosition, Quaternion.identity).GetComponent<Bullet>();

        Instance.bullets.Add(bullet);

        return bullet.Init(SpawnIndex, velocity);
    }

    public static IEnumerator NextStep() {
        Sequence bulletSequence = DOTween.Sequence();
        foreach (Bullet bullet in Instance.bullets) {
            var bulletTween = bullet.NextStep();
            if(bulletTween != null)
                bulletSequence.Join(bulletTween);
        }
        bulletSequence.Play();
        yield return bulletSequence.WaitForCompletion();
    }
}
