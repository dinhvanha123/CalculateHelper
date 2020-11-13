using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputAbs : MonoBehaviour
{
    public static string VALUE_KEY;
    public Text textResult;
    public TMP_InputField tmpText;
    protected CultureInfo cultureInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void SetValueBasic()
    {
        cultureInfo = new CultureInfo("en-US");
        tmpText = GetComponent<TMP_InputField>();
        if (textResult == null)
        {
            textResult = GameObject.FindWithTag("ResultValue").GetComponent<Text>();
        }
    }

    public virtual void UpdateResult()
    {
        if (!string.IsNullOrEmpty(tmpText.text))
        {

            textResult.text = string.Format(cultureInfo, "{0:C}", tmpText.text);
        }
    }
}
