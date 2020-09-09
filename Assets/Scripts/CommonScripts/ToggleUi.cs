using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ToggleUi : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private bool isOn;
    [SerializeField]
    private RectTransform toggleIndicator;
    [SerializeField]
    private Image bcImage;
    public Image toggleIndicatorImage;
    public Sprite onToggleIndicatorSprite;
    public Sprite offToggleIndicatorSprite;
    public Image toggleLogo;
    public Sprite onToggleLogoSprite;
    public Sprite offToggleLogoSprite;
    private float onPosX;
    private float offPosX;
    private float tweenTime = 0.25f;

  public bool IsOn { get => isOn;}

  public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;

    private void OnEnable()
    {
        Toggle(isOn);
    }
    private void Start()
    {
        offPosX = toggleIndicator.anchoredPosition.x;
        onPosX = bcImage.rectTransform.rect.width - toggleIndicator.rect.width;
    }

    public void Toggle(bool value)
    {
        if (value != isOn)
        {
            isOn = value;
            MoveIndicator(isOn);
            if (valueChanged != null)
            {
                valueChanged(isOn);
            }
        }
    }

    private void MoveIndicator(bool value)
    {
        if (value)
        {
            toggleIndicator.DOAnchorPosX(onPosX - 20, tweenTime);
            toggleIndicatorImage.GetComponent<Image>().sprite = onToggleIndicatorSprite;
            toggleLogo.GetComponent<Image>().sprite = onToggleLogoSprite;

        }
        else
        {
            toggleIndicator.DOAnchorPosX(offPosX, tweenTime);
            toggleIndicatorImage.GetComponent<Image>().sprite = offToggleIndicatorSprite;
            toggleLogo.GetComponent<Image>().sprite = offToggleLogoSprite;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);
    }
}
