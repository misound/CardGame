using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practise : MonoBehaviour
{
    [Header("Press Times Test")]
    [SerializeField] private int time;
    [SerializeField] private Text valueText;
    [SerializeField] private Button TimesB;

    [Header("Change Image")] 
    [SerializeField] private bool BSwitch = false;
    [SerializeField] private Button imButton;
    [SerializeField] private Text imButtonText;

    [SerializeField] private Image image01;
    [SerializeField] private Image image02;

    [SerializeField] private Sprite Forimage01;
    [SerializeField] private Sprite Forimage02;

    [Header("InputField")] 
    [SerializeField] private Toggle showToggle;
    [SerializeField] private InputField inputField;
    [SerializeField] private Text inputText;
    [SerializeField] private bool inputstate;

    [Header("scrollbar & slider")] 
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Slider slider;
    [SerializeField] private Text scrollbarText;
    [SerializeField] private Text sliderText;

    [Header("Dropdown")] 
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Text dropdowntext;
    [SerializeField] private string[] textinfo = {"This is a boring test project.","This is a another boring practise.","Why do I doing this?"};
    

    private void Start()
    {
        TimesB.onClick.AddListener(OnTimesButtonClick);
        imButton.onClick.AddListener(OnImageChangeButtonClick);
        imButtonText.text = "顯示圖片";


        showToggle.isOn = false;
        showToggle.onValueChanged.AddListener(OnToggleClick);
        inputField.onValueChanged.AddListener(TextChange);
        inputField.gameObject.SetActive(false);
        inputText.gameObject.SetActive(false);

        slider.maxValue = 100f;
        slider.minValue = -100f;
        scrollbarText.text = scrollbar.value.ToString();
        sliderText.text = slider.value.ToString();
        
        scrollbar.onValueChanged.AddListener(OnscrollbarChanged);
        slider.onValueChanged.AddListener(OnSliderChanged);

        dropdown.onValueChanged.AddListener(OnDropdownChange);
        textinfo[0] = "This is a boring test project.";
        textinfo[1] = "This is a another boring practise.";
        textinfo[2] = "Why do I doing this?";
        OnDropdownTextChange();
    }

    private void Update()
    {
    }

    public void OnTimesButtonClick()
    {
        valueText.text = (++time).ToString();

        if (time >= 10)
        {
            valueText.text = "你已經按了" + time + "次";
        }
    }

    public void OnImageChangeButtonClick()
    {
        if (BSwitch)
        {
            image01.sprite = null;
            image02.sprite = null;

            imButtonText.text = "顯示圖片";
        }
        else
        {
            image01.sprite = Forimage01;
            image02.sprite = Forimage02;

            imButtonText.text = "關閉圖片";
        }

        BSwitch = !BSwitch;
    }

    public void OnToggleClick(bool toggle)
    {
        if (toggle)
        {
            inputField.gameObject.SetActive(true);
            inputText.gameObject.SetActive(true);
        }
        else
        {
            inputField.gameObject.SetActive(false);
            inputText.gameObject.SetActive(false);
        }

        inputstate = !inputstate;
    }

    public void TextChange(string value)
    {
        inputText.text = inputField.text;
    }

    public void OnscrollbarChanged(float value)
    {
        scrollbarText.text = scrollbar.value.ToString();
    }

    public void OnSliderChanged(float value)
    {
        sliderText.text = slider.value.ToString();
    }

    public void OnDropdownChange(int value)
    {
        OnDropdownTextChange();
    }
    
    public void OnDropdownTextChange()
    {
        dropdowntext.text = textinfo[dropdown.value];
    }
}