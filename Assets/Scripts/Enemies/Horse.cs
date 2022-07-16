using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Horse : Enemy {
    int _shootCounter = 0;

    int _shootDelay = 2;
    public override IEnumerator NextStep() {
        if (_shootCounter > 0) _shootCounter--;

        Vector2 currentPos = MapController.WorldToTilemapPoint(transform.position.x, transform.position.z);
        /*Debug.Log(MapController.PlayerPosition.x + " : "+ MapController.PlayerPosition.y + "  " + 
            currentPos.x + " : " + currentPos.y);*/

        if (currentPos.x == MapController.PlayerPosition.x) {
            if (_shootCounter == 0) {
                Vector2 angle = new Vector2(0, (MapController.PlayerPosition.y < currentPos.y ? -1 : 1));
                Shoot(currentPos + angle, angle);
            }
        } else if (currentPos.y == MapController.PlayerPosition.y) {
            if (_shootCounter == 0) {
                Vector2 angle = new Vector2((MapController.PlayerPosition.x < currentPos.x ? -1 : 1), 0);
                Shoot(currentPos + angle, angle);
            }
        } else {
            Vector2 dif = new Vector2(Mathf.Abs(currentPos.x - MapController.PlayerPosition.x),
                                      Mathf.Abs(currentPos.y - MapController.PlayerPosition.y));

            if (dif.x < dif.y) {
                //Move In X
                yield return Move(currentPos + new Vector2((MapController.PlayerPosition.x < currentPos.x ?
                    Mathf.Max(-2, MapController.PlayerPosition.x - currentPos.x) :
                    Mathf.Min(2, MapController.PlayerPosition.x - currentPos.x)), 0));
            } else {
                //Move In Y
                yield return Move(currentPos + new Vector2(0, (MapController.PlayerPosition.y < currentPos.y ?
                    Mathf.Max(-2, MapController.PlayerPosition.y - currentPos.y) :
                    Mathf.Min(2, currentPos.y - MapController.PlayerPosition.y))));
            }
        }

        yield return null;
    }

    public override IEnumerator Move(Vector2 tar) {
        if (MapController.MoveTo(MapController.WorldToTilemapPoint(transform.position.x, transform.position.z), tar)) {
            Vector2 temp = MapController.TilemapToWorldPoint((int)tar.x, (int)tar.y);

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
