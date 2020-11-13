using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
public class InputMono : MonoBehaviour
{
    public static string RATIO_KEY = "RATIO_KEY";
    public Text textResult;
    public TMP_InputField tmpTextValue;
    public TMP_InputField tmpTextRatio;
    protected CultureInfo cultureInfo;

    // Start is called before the first frame update
    void Start()
    {
        cultureInfo = new CultureInfo("en-US");
        tmpTextValue = GetComponent<TMP_InputField>();
        if (textResult == null)
        {
           textResult = GameObject.FindWithTag("ResultValue").GetComponent<Text>();
        }
        if (tmpTextValue == null)
        {
            tmpTextValue = GameObject.FindWithTag("InputValue").GetComponent<TMP_InputField>();
            if (tmpTextValue == null)
            {
                tmpTextValue = GameObject.Find("InputValue").GetComponent<TMP_InputField>();
            }
        }

        if (tmpTextValue == null)
        {
            tmpTextRatio = GameObject.FindWithTag("InputRatio").GetComponent<TMP_InputField>();
            if (tmpTextValue == null)
            {
                tmpTextRatio = GameObject.Find("InputRatio").GetComponent<TMP_InputField>();
            }
        }

        if (string.IsNullOrEmpty(tmpTextValue.text))
        {
            tmpTextValue.text = "0";
            //SavePrefs();
        }

        tmpTextRatio.text = PlayerPrefs.GetString(RATIO_KEY);
        Debug.LogError("1. " + tmpTextRatio.text);
        if (string.IsNullOrEmpty(tmpTextRatio.text))
        {
            tmpTextRatio.text = "1.5";
            SavePrefs();
        }

        UpdateTextResult();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!string.IsNullOrEmpty(tmpText.text))
        //{
        //    textResult.text = tmpText.text;
        //}
    }

    public void UpdateResult()
    {
        UpdateTextResult();
        SavePrefs();
    }

    private void UpdateTextResult()
    {
        if (string.IsNullOrEmpty(tmpTextValue.text) || string.IsNullOrEmpty(tmpTextRatio.text)) return;
        double resultEnd = double.Parse(tmpTextValue.text) * double.Parse(tmpTextRatio.text);
        textResult.text = resultEnd.ToString();
    }

    private void SavePrefs()
    {
        Debug.LogError("2. " + tmpTextRatio.text);
        PlayerPrefs.SetString(RATIO_KEY, tmpTextRatio.text);
        PlayerPrefs.Save();
        Debug.LogError("3. " + PlayerPrefs.GetString(RATIO_KEY));

    }
}
