using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 屏幕切割控制
/// </summary>
public class screenSpliter : MonoBehaviour {

    public Camera leftCamera = null;
    public Camera rightCamera = null;

    public GameObject leftArea = null;
    public GameObject rightArea = null;

    float yThreshold = 5f;

    //mouse info
    Vector2 lastPosition;
    int choosedSide = 0;//0-none;-1-left;1-right
	
	void Update () {
        ComputeDrag();
    }

    void ComputeDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x,worldPos.y), new Vector2(worldPos.x, worldPos.y),1000f, 1 << LayerMask.NameToLayer("click"));
            choosedSide = 0;
            lastPosition = new Vector2(worldPos.x, worldPos.y);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == leftArea)
                {
                    choosedSide = -1;
                }
                else if(hit.collider.gameObject == rightArea)
                {
                    choosedSide = 1;
                }
            }
        }else if (Input.GetMouseButton(0))
        {
            if (choosedSide == 0 || character.instance.CharacterSide() == choosedSide)
                return;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Transform moveTran = leftCamera.transform;
            if(choosedSide == 1)
            {
                moveTran = rightCamera.transform;
            }
            float yDiff = (lastPosition.y - worldPos.y);
            float nextY = moveTran.position.y + yDiff;
            nextY = (nextY < -yThreshold) ? -yThreshold : nextY;
            nextY = (nextY > yThreshold) ? yThreshold : nextY;
            moveTran.position = new Vector3(0, nextY, -100);
            lastPosition = new Vector2(worldPos.x, worldPos.y);
        }
    }
}
