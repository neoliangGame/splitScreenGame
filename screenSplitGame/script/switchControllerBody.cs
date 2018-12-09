using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 触发character切换屏幕控制器
/// </summary>
public class switchControllerBody : MonoBehaviour {

    public int index = 0;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (character.instance.IsCharacterCollider(collision.gameObject))
        {
            character.instance.ChangeSide(index);
        }
    }
}
