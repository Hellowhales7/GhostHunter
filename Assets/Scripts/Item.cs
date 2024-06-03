using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int part;
    public void OnObjectTouched()
    {
        // 오브젝트 획득 로직을 여기에 작성
        Debug.Log($"{gameObject.name} 획득!");
        // 오브젝트를 삭제하거나 비활성화
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
