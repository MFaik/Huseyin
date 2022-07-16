using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum ItemEnum {
        Pistol,
        Shotgun,
        Knife,
    }
    
    public Material material;

    public ItemEnum item;

    public IEnumerator Shoot(Vector3 position, Vector2 direction){
        switch(item){
            case ItemEnum.Pistol:
                yield return BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + direction.x, position.z + direction.y),direction).WaitForCompletion();
            break;
            case ItemEnum.Shotgun:
                Sequence shotgunSequence = DOTween.Sequence();

                shotgunSequence.Join(BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + direction.x, position.z + direction.y),direction));

                Vector2 leftDirection = direction;
                Vector2 rightDirection = direction;
                if(direction.x == 0){
                    leftDirection.x = -1;
                    rightDirection.x = 1;
                } else {
                    leftDirection.y = -1;
                    rightDirection.y = 1;
                }
                shotgunSequence.Join(BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + leftDirection.x, position.z + leftDirection.y),leftDirection));
                shotgunSequence.Join(BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + rightDirection.x, position.z + rightDirection.y),rightDirection));

                yield return shotgunSequence.WaitForCompletion();
            break;
        }
    }
}
