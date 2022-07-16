using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + direction.x, position.z + direction.y),direction);
            break;
            case ItemEnum.Shotgun:
                BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + direction.x, position.z + direction.y),direction);
                Vector2 leftDirection = direction;
                Vector2 rightDirection = direction;
                if(direction.x == 0){
                    leftDirection.x = -1;
                    rightDirection.x = 1;
                } else {
                    leftDirection.y = -1;
                    rightDirection.y = -1;
                }
                BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + leftDirection.x, position.z + leftDirection.y),leftDirection);
                BulletManager.SpawnBullet(position,
                                            MapController.WorldToTilemapPoint(position.x + rightDirection.x, position.z + rightDirection.y),rightDirection);
            break;
        }

        yield break;
    }
}
