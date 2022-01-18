using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionUI : MonoBehaviour
{
    [SerializeField]
    private Button      m_goButton;
    [SerializeField]
    private GameObject  m_shouldSelectText;
    [SerializeField]
    private GameObject  m_avatarListContainer;

    public Button       ButtonGo => m_goButton;
    public GameObject   TextShouldSelect => m_shouldSelectText;
    public GameObject   AvatarListContainer => m_avatarListContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
