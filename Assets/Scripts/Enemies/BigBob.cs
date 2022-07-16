using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigBob : Enemy
{
    int _shootCounter = 0;

    int _shootDelay = 2;
    public override IEnumerator NextStep() {
        if (_shootCounter > 0) _shootCounter--;
        Vector2 currentPos = MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z);
        
        if (currentPos.x == MapManager.PlayerPosition.x) {
            if (_shootCounter == 0) {
                Vector2 angle = new Vector2(0, (MapManager.PlayerPosition.y < currentPos.y ? -1 : 1));
                Shoot(currentPos + angle, angle);
            }
        }else if (currentPos.y == MapManager.PlayerPosition.y) {
            if (_shootCounter == 0) {
                Vector2 angle = new Vector2((MapManager.PlayerPosition.x < currentPos.x ? -1 : 1), 0);
                Shoot(currentPos + angle, angle);
            }       
        } else {
            Vector2 dif = new Vector2(Mathf.Abs(currentPos.x - MapManager.PlayerPosition.x),
                                      Mathf.Abs(currentPos.y - MapManager.PlayerPosition.y));
            Vector2 tar;
            if(dif.x < dif.y) {
                //Move In X
                tar = new Vector2((MapManager.PlayerPosition.x < currentPos.x ? -1 : 1), 0);
                yield return Move(currentPos + tar);
            } else {
                //Move In Y
                tar = new Vector2(0, (MapManager.PlayerPosition.y < currentPos.y ? -1 : 1));
                yield return Move(currentPos + tar);
            }

            currentPos += tar;

            if (currentPos.x == MapManager.PlayerPosition.x) {
                if (_shootCounter == 0) {
                    Vector2 angle = new Vector2(0, (MapManager.PlayerPosition.y < currentPos.y ? -1 : 1));
                    Shoot(currentPos + angle, angle);
                }
            } else if (currentPos.y == MapManager.PlayerPosition.y) {
                if (_shootCounter == 0) {
                    Vector2 angle = new Vector2((MapManager.PlayerPosition.x < currentPos.x ? -1 : 1), 0);
                    Shoot(currentPos + angle, angle);
                }
            }
        }

        yield return null;
    }

    public override IEnumerator Move(Vector2 tar) {
        if (MapManager.MoveTo(MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z), tar)) {
            Vector2 temp = MapManager.TilemapToWorldPoint((int)tar.x, (int)tar.y);

            Vector3 pos = new Vector3(temp.x, 1, temp.y);
            yield return transform.DOMove(pos, 0.3f).WaitForCompletion();
        } else {
            yield return null;
        }
    }

    public override void Shoot(Vector2 spawn, Vector2 vel) {
        BulletManager.SpawnBullet(transform.position, spawn, vel);
        _shootCounter = _shootDelay;
    }
}
