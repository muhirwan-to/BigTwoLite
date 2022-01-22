using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public enum EAreaID
    {
        HandArea,
        LowArea,
        MidArea,
        HighArea
    }

    [SerializeField]
    private EAreaID     m_areaID;

    public EAreaID      AreaID => m_areaID;
    public Transform    LinkedCardParent { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
