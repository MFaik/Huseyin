using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    Vector2 _velocity;
    bool _firstTurn = true;

    // Update is called once per frame
    public Tween Init(Vector2 spawnPos, Vector2 vel) {
        _velocity = vel;

        Vector2 temp = MapController.TilemapToWorldPoint((int)spawnPos.x, (int)spawnPos.y); ;
        Vector3 targetPosition = new Vector3(temp.x, 1f, temp.y);

        return transform.DOMove(targetPosition, 0.1f);
    }

    public Tween NextStep() {
        if(_firstTurn){
            _firstTurn = false;
            return null;
        }
        Vector2 currentIndex = MapController.WorldToTilemapPoint(transform.position.x, transform.position.z);

        Vector2 targetIndex = currentIndex + _velocity;
        
        Vector2 temp = MapController.TilemapToWorldPoint((int)targetIndex.x, (int)targetIndex.y); ;
        Vector3 targetPosition = new Vector3(temp.x, 1, temp.y);

        return transform.DOMove(targetPosition, 0.3f);
    }
}
