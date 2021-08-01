﻿using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace GalacticScale
{
    public class GSUIRangeSlider : MonoBehaviour
    {
        public float minValue
        {
            get => _slider.MinValue;
            set => _slider.MinValue = value;
        }
        public float maxValue
        {
            get => _slider.MaxValue;
            set => _slider.MaxValue = value;
        }
        public GSOptionCallback OnLowChange;
        public GSOptionCallback OnHighChange;
        public RangeSlider _slider;
        public Text _labelText;
        public Text _hintText;
        public string Hint
        {
            get => _hintText.text;
            set => _hintText.text = value;
        }
        public string Label
        {
            get => _labelText.text;
            set => _labelText.text = value;
        }
        public float LowValue
        {
            get => _slider.LowValue;
            set => _slider.LowValue = value;
        }
        public float HighValue
        {
            get => _slider.HighValue;
            set => _slider.HighValue = value;
        }
        public bool WholeNumbers
        {
            get => _slider.WholeNumbers;
            set => _slider.WholeNumbers = value;
        }

        public Text _lowValueText;
        public Text _highValueText;
        public void OnSliderValueChange(RangeSlider slider)
        {
            // float lowValue = (int)(slider.LowValue * 100) / 100f;
            // float highValue = (int)(slider.LowValue * 100) / 100f;
            // _lowValueText.text = lowValue.ToString();
            // _highValueText.text = highValue.ToString();
            // if (LowValue != lowValue)
            // {
            //     LowValue = lowValue;
            //     if (OnLowChange != null) OnLowChange.Invoke(lowValue);
            // }
            //
            // if (HighValue != highValue)
            // {
            //     HighValue = highValue;
            //     if (OnHighChange != null) OnHighChange.Invoke(highValue);
            // }
            //
        }

        public void Test(float a, float b)
        {
            
        }


    }

}