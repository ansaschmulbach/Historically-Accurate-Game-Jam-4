using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float m_MinYValue;
    [SerializeField] private float m_MaxYValue;
    [SerializeField] private float m_MinZValue;
    [SerializeField] private float m_MaxZValue;

    [NonSerialized] public static FurnitureManager instance;
    
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        GameObject[] furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (GameObject furniture in furnitures)
        {
            float z = furniture.transform.position.y;
            Collider2D furnitureCollider = furniture.GetComponent<Collider2D>();
            float offset = furnitureCollider.offset.y;
            z += offset;
            z -= furnitureCollider.bounds.size.y / 2;
            Vector3 pos = furniture.transform.position;
            furniture.transform.position = new Vector3(pos.x, pos.y, normalize(z));
        }
    }

    public float normalize(float z)
    {
        float yNorm = (z - m_MinYValue) / (m_MaxYValue - m_MinYValue);
        float zNorm = yNorm * (m_MaxZValue - m_MinZValue) + m_MinZValue;
        return zNorm;
    }
    
    

}
