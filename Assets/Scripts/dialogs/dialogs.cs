using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InputSystem;
using UnityEngine.UI;

public class dialogs : MonoBehaviour
{
    InputReader rd;
    public TextMeshProUGUI textcomponent;
    public string[] lines;
    public TextMeshProUGUI nameby;
    public float textspeed;
    public int index;
    float i = 5;
    bool cantransition;
    public Image noelbaba;
    public Image büyücü;
    public TextMeshProUGUI spaceebas;
    private void Awake()
    { 
        rd = new InputReader();

    }
    void Start()
    {
        textcomponent.text = string.Empty;
        startdialogue();


    }
    private void OnEnable()
    {
        rd.spacebutton += trasitiondialog;
    }
    private void OnDisable()
    {
        rd.spacebutton -= trasitiondialog;
    }
    void Update()
    {
        
    }
    void startdialogue()
    {
        index = 0;
        StartCoroutine(Typeline());
    }
    IEnumerator Typeline()
    {
        cantransition = true;
        foreach(char c in lines[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
        cantransition = false;
    }

    void nextline()
    {
        if(index < lines.Length -1)
        {
            index++;
            textcomponent.text = string.Empty;
            if (index == 2)
            {
                nameby.gameObject.SetActive(true);
                textcomponent.transform.localPosition = new Vector3(textcomponent.transform.localPosition.x, -283, textcomponent.transform.localPosition.z);
                büyücü.gameObject.SetActive(true);
                noelbaba.gameObject.SetActive(false);
                nameby.text = "Büyücü:";
            }
            if (index == 3)
            {
                nameby.gameObject.SetActive(true);
                textcomponent.transform.localPosition = new Vector3(textcomponent.transform.localPosition.x, -283, textcomponent.transform.localPosition.z);
                büyücü.gameObject.SetActive(false);
                noelbaba.gameObject.SetActive(true);
                nameby.text = "Noel Baba:";

            }
            if (index == 4)
            {
                nameby.gameObject.SetActive(true);
                textcomponent.transform.localPosition = new Vector3(textcomponent.transform.localPosition.x, -283, textcomponent.transform.localPosition.z);
                büyücü.gameObject.SetActive(true);
                noelbaba.gameObject.SetActive(false);
                nameby.text = "Büyücü:";
            }
            if (index == 5)
            {
                nameby.gameObject.SetActive(true);
                textcomponent.transform.localPosition = new Vector3(textcomponent.transform.localPosition.x, -283, textcomponent.transform.localPosition.z);
                büyücü.gameObject.SetActive(false);
                noelbaba.gameObject.SetActive(true);
                nameby.text = "Noel Baba:";
            }
            if (index == 6)
            {
                nameby.gameObject.SetActive(true);
                textcomponent.transform.localPosition = new Vector3(textcomponent.transform.localPosition.x, -283, textcomponent.transform.localPosition.z);
                büyücü.gameObject.SetActive(true);
                noelbaba.gameObject.SetActive(false);
                nameby.text = "Büyücü:";
            }
            if (index > 6)
            {
                textcomponent.transform.localPosition = new Vector3(textcomponent.transform.localPosition.x, -200, textcomponent.transform.localPosition.z);
                nameby.gameObject.SetActive(false);
                büyücü.gameObject.SetActive(false);
                noelbaba.gameObject.SetActive(false);
            }
            StartCoroutine(Typeline());

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    void trasitiondialog()
    {
        if(cantransition == true)
        {
            return;
        }
        if (textcomponent.text == lines[index])
        {
            spaceebas.gameObject.SetActive(false);
            nextline();
        }
        else
        {
            StopAllCoroutines();
            textcomponent.text = lines[index];
        }
    }
}
