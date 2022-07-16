using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    [SerializeField] float MoveTime = 1f;

    public MapController controller;
    float t = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = MapController.CalculatePositionFromLocation(transform.position.x, transform.position.z);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 tar = pos;

        if (x > 0) tar.x++;
        else if (x < 0) tar.x--;
        else if (y > 0) tar.y++;
        else if (y < 0) tar.y--;

        t += Time.deltaTime;

        if (pos != tar && t < MoveTime) return;
        t = 0;

        

        if (controller.MoveTo(pos, tar)) {
            Vector2 targetLocation = MapController.CalculatePositionFromIndex((int)tar.x, (int)tar.y);
            transform.position = new Vector3(targetLocation.x, 1, targetLocation.y);
        }
    }
}
