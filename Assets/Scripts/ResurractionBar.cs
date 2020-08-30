using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResurractionBar : MonoBehaviour {

    private Fill fill;
    private float barMaskWidth;
    [SerializeField] RectTransform barMaskRectTransform;
    [SerializeField] RectTransform edgeRectTransform;
    [SerializeField] RawImage barRawImage;
    [SerializeField] GameObject losePanel;

    private void Start()
    {
        barMaskWidth = barMaskRectTransform.sizeDelta.x;

        fill = new Fill();

    }

    private void Update() {
        fill.Update();

        if (fill.fillAmount <= 0)
        {
            losePanel.SetActive(true);
            this.gameObject.SetActive(false);
        }

        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= .3f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = fill.GetFillNormalized() * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;

        edgeRectTransform.anchoredPosition = new Vector2(fill.GetFillNormalized() * barMaskWidth, 0);

        edgeRectTransform.gameObject.SetActive(fill.GetFillNormalized() > 0f);
    }

}


public class Fill 
{
    public const int MANA_MAX = 100;

    public float fillAmount;
    private float fillRegenAmount;

    public Fill() {
        fillAmount = 100;
        fillRegenAmount = 10f;
    }

    public void Update() {
        fillAmount -= fillRegenAmount * Time.deltaTime;
        fillAmount = Mathf.Clamp(fillAmount, 0f, MANA_MAX);

    }

    public float GetFillNormalized() {
        return fillAmount / MANA_MAX;
    }

}