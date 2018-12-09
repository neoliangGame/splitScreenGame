using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// character控制脚本
/// 控制其左右走动和跳跃
/// 控制其根据屏幕位置切换控制体
/// </summary>
public class character : MonoBehaviour {

    static character _instance = null;
    public static character instance{
        get { return _instance; }
    }

    int inSide = -1;//-1-left;0-none;1-right

    GameObject collider;
    [SerializeField]
    GameObject colliderLeft;
    [SerializeField]
    GameObject colliderRight;

    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject anima;

    [SerializeField]
    GameObject leftCamera;
    [SerializeField]
    GameObject rightCamera;

    [SerializeField]
    KeyCode moveLeft = KeyCode.A;
    [SerializeField]
    KeyCode moveRight = KeyCode.D;
    [SerializeField]
    KeyCode jump = KeyCode.W;

	void Start () {
        collider = colliderLeft;
        _instance = this;

	}
	
	void Update () {
        Move();

        if(collider.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            anima.GetComponent<Animator>().SetBool("move",true);
        }
        else
        {
            anima.GetComponent<Animator>().SetBool("move", false);
        }
	}
    
    #region 本身控制
    void Move()
    {
        if (Input.GetKeyDown(moveLeft))
        {
            collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100,0), ForceMode2D.Force);
        }
        else if (Input.GetKeyDown(moveRight))
        {
            collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0), ForceMode2D.Force);
        }
        else if (Input.GetKeyDown(jump))
        {
            collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100), ForceMode2D.Force);
        }
    }

    #endregion

    #region 对外接口
    public bool IsCharacterCollider(GameObject collision)
    {
        return collision.gameObject == collider;
    }

    public int CharacterSide()
    {
        return inSide;
    }

    public void ChangeSide(int side)
    {
        if(inSide != side)
        {
            GameObject fromObj = (side == 1) ? colliderLeft : colliderRight;
            GameObject toObj = (side == 1) ? colliderRight : colliderLeft;

            Transform fromCam = (side == 1) ? leftCamera.transform : rightCamera.transform;
            Transform toCam = (side == 1) ? rightCamera.transform : leftCamera.transform;

            float yDiff = toCam.position.y - fromCam.position.y;
            toObj.transform.position = new Vector3(fromObj.transform.position.x, fromObj.transform.position.y + yDiff, fromObj.transform.position.z);
            toObj.gameObject.SetActive(true);
            body.transform.parent = toObj.transform;

            fromObj.SetActive(false);
            collider = toObj;
        }
        inSide = side;
    }
    #endregion
}
