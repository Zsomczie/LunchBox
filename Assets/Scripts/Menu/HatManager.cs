using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class HatManager : MonoBehaviour
{
    [SerializeField]string hatType;
    [SerializeField] SpriteResolver sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name=="Character")
        {
            hatType = sprite.GetLabel();
        }

    }
    private void OnLevelWasLoaded(int level)
    {
        if (level==1)
        {
            gameObject.SetActive(true);
            GameObject.Find("Player").GetComponentInChildren<SpriteResolver>().SetCategoryAndLabel("Hat", hatType);
            Destroy(gameObject);
        }
        
    }
}
