using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform RendererChild;

    bool Turning = false;

    enum Direction {
        ZP,
        ZM,
        XP,
        XM
    }

    void Update() {
        if(Turning)
            return;
        if(Input.GetAxisRaw("Horizontal") > 0.1f){
            StartCoroutine(PlayerTurn(Direction.ZP));
        }  
        else if(Input.GetAxisRaw("Horizontal") < -0.1f){
            StartCoroutine(PlayerTurn(Direction.ZM));
        } 
        else if(Input.GetAxisRaw("Vertical") < -0.1f){
            StartCoroutine(PlayerTurn(Direction.XP));
        } 
        else if(Input.GetAxisRaw("Vertical") > 0.1f){
            StartCoroutine(PlayerTurn(Direction.XM));
        }   
    }

    Vector3 ZP = new Vector3(0,0,0.5f);
    Vector3 ZM = new Vector3(0,0,-0.5f);
    Vector3 XP = new Vector3(0.5f,0,0);
    Vector3 XM = new Vector3(-0.5f,0,0);

    IEnumerator PlayerTurn(Direction direction) {
        Turning = true;
        switch(direction){
            case Direction.ZP:
                transform.position += ZP;
                RendererChild.position -= ZP;
                yield return transform.DORotate(new Vector3(90,0,0),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += ZP;
                RendererChild.position -= ZP;
            break;
            case Direction.XP:
                transform.position += XP;
                RendererChild.position -= XP;
                yield return transform.DORotate(new Vector3(0,0,-90),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += XP;
                RendererChild.position -= XP;
            break;
            case Direction.ZM:
                transform.position += ZM;
                RendererChild.position -= ZM;
                yield return transform.DORotate(new Vector3(-90,0,0),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += ZM;
                RendererChild.position -= ZM;
            break;
            case Direction.XM:
                transform.position += XM;
                RendererChild.position -= XM;
                yield return transform.DORotate(new Vector3(0,0,90),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += XM;
                RendererChild.position -= XM;
            break;
        }
        Turning = false;
        yield break;
    }
}
