using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AskName : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LastUsedName;
    public void startshow()
    {
        
        
        for(int i = GameHandler.Playernames.Count; i > GameHandler.Playernames.Count-5; i--)
        {
           
            var NewName = Instantiate(LastUsedName, new Vector3(345, 370 - ((GameHandler.Playernames.Count-i) * 150), 0), Quaternion.identity);
            NewName.transform.SetParent(GameObject.FindGameObjectWithTag("AskName").transform, false);
            TextMeshProUGUI Position = NewName.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Position.text = GameHandler.Playernames[i-1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetName() { 
        
    
    }
}