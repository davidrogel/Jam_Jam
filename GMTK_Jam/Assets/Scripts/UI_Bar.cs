using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour {

    public float Value = 10;
    public float MaxValue = 10;
    private RectTransform BarTr;
    private GameObject Bar;

    public float speed = 2;

    [Header("Color Curves")]
    public AnimationCurve Red;
    public AnimationCurve Green;
    public AnimationCurve Blue;

    private void Start()
    {
        BarTr = GetComponent<RectTransform>();
        Bar = transform.GetChild(0).gameObject;
    }

    private void LateUpdate()
    {
        UpdateWidth();
        UpdateColor();
    }

    private void UpdateWidth()
    {
        float ValuePercentage = (Value ) / MaxValue;
        float Displacement = (ValuePercentage * BarTr.sizeDelta.x);

        float FinalPosX = BarTr.position.x - (BarTr.sizeDelta.x - Displacement);


        Bar.transform.position = Vector3.Lerp(Bar.transform.position, new Vector3(FinalPosX, Bar.transform.position.y, Bar.transform.position.z), speed * Time.deltaTime);
    }

    private void UpdateColor()
    {
        float ValuePercentage = Value / MaxValue;
        Image img = Bar.GetComponent<Image>();
        img.color = new Color(Red.Evaluate(ValuePercentage) , Green.Evaluate(ValuePercentage), Blue.Evaluate(ValuePercentage));
    }

}
