using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;

    public Character selected;

    private float hp;
    private float maxHP;
    // Start is called before the first frame update
    private void Start()
    {
        slider.minValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        hp = selected.HP;
        maxHP = selected.maxHP;
        slider.maxValue = maxHP;
        slider.value = hp;

        text.text = string.Format("{0}/{1}", hp, maxHP);
    }
}
