using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseName : MonoBehaviour
{
    private GameObject InputField;
    // Start is called before the first frame update
    void Start()
    {
        InputField = GameObject.FindGameObjectWithTag("AskInput");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clicked()
    {
        string selftext = transform.GetChild(0).GetComponent<TMP_Text>().text;
        InputField.GetComponent<TMP_InputField>().text = selftext;
    }
}
