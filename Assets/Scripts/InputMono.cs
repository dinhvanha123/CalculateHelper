using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
public class InputMono : MonoBehaviour
{
    public static string RATIO_KEY = "VALUE_KEY";
    public Text textResult;
    public TMP_InputField tmpTextValue;
    public TMP_InputField tmpTextRatio;

    public Text textConvert;
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
        if (textConvert == null)
        {
            textConvert = GameObject.Find("textConvert").GetComponent<Text>();
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
           // SavePrefs();
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
        //Debug.LogError(tmpTextRatio.text);
        textConvert.text = ChuyenSo(tmpTextValue.text);
    }

    private void SavePrefs()
    {
       // Debug.LogError("2. " + tmpTextRatio.text);
        PlayerPrefs.SetString(RATIO_KEY, tmpTextRatio.text);
        PlayerPrefs.Save();
       // Debug.LogError("3. " + PlayerPrefs.GetString(RATIO_KEY));

    }

    // source: https://tuoitreit.vn/threads/chuyen-so-sang-chu-c.41013/
    private string ChuyenSo(string number)
    {
        string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
        string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string doc;
        int i, j, k, n, len, found, ddv, rd;
        number = number.Replace(".", "").Replace(",","");
        len = number.Length;
        number += "ss";
        doc = "";
        found = 0;
        ddv = 0;
        rd = 0;
        i = 0;
        while (i < len)
        {
            //So chu so o hang dang duyet
            n = (len - i + 2) % 3 + 1;

            //Kiem tra so 0
            found = 0;
            for (j = 0; j < n; j++)
            {
                if (number[i + j] != '0')
                {
                    found = 1;
                    break;
                }
            }

            //Duyet n chu so
            if (found == 1)
            {
                rd = 1;
                for (j = 0; j < n; j++)
                {
                    ddv = 1;
                    switch (number[i + j])
                    {
                        case '0':
                            if (n - j == 3) doc += cs[0] + " ";
                            if (n - j == 2)
                            {
                                if (number[i + j + 1] != '0') doc += "lẻ ";
                                ddv = 0;
                            }
                            break;
                        case '1':
                            if (n - j == 3) doc += cs[1] + " ";
                            if (n - j == 2)
                            {
                                doc += "mười ";
                                ddv = 0;
                            }
                            if (n - j == 1)
                            {
                                if (i + j == 0) k = 0;
                                else k = i + j - 1;

                                if (number[k] != '1' && number[k] != '0')
                                    doc += "mốt ";
                                else
                                    doc += cs[1] + " ";
                            }
                            break;
                        case '5':
                            if (i + j == len - 1)
                                doc += "lăm ";
                            else
                                doc += cs[5] + " ";
                            break;
                        default:
                            doc += cs[(int)number[i + j] - 48] + " ";
                            break;
                    }

                    //Doc don vi nho
                    if (ddv == 1)
                    {
                        doc += dv[n - j - 1] + " ";
                    }
                }
            }


            //Doc don vi lon
            if (len - i - n > 0)
            {
                if ((len - i - n) % 9 == 0)
                {
                    if (rd == 1)
                        for (k = 0; k < (len - i - n) / 9; k++)
                            doc += "tỉ ";
                    rd = 0;
                }
                else
                    if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
            }

            i += n;
        }

        if (len == 1)
        {
            if (number[0] == '0' || number[0] == '5')
            {
                doc = cs[(int)number[0] - 48];
                return char.ToUpper(doc[0]) + doc.Substring(1);
            }
        }

        // format first char: https://www.educative.io/edpresso/how-to-capitalize-the-first-letter-of-a-string-in-c-sharp
        doc = doc.Replace("  ", " ");
        doc = char.ToUpper(doc[0]) + doc.Substring(1);
        
        return doc;
    }
}
