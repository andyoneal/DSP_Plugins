﻿using System.Collections.Generic;
using ScenarioRTL;
using UnityEngine;
using UnityEngine.UI;

namespace GalacticScale
{
    public class ExternalThemeSelector : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GS2.LogJson(GS2.externalThemes);
            GS2.themeSelector = this;
            foreach (var t in  GS2.externalThemes)
            {
                GS2.Warn($"Adding {t.Key}");
                var item = Object.Instantiate(itemTemplate, itemList, false);
                var tsi = item.GetComponent<ThemeSelectItem>();
                tsi.label = t.Value.DisplayName;
                tsi.theme = t.Value;
                tsi.gameObject.SetActive(true);
                items.Add(tsi);
            }
        }


        // public ThemeLibrary ThemeLibrary = new ThemeLibrary();
        public RectTransform itemTemplate;
        public RectTransform groupTemplate;
        public RectTransform itemList;
        public Toggle masterToggle;
        public List<ThemeSelectItem> items = new List<ThemeSelectItem>();
        public List<ThemeSelectGroup> groups = new List<ThemeSelectGroup>();
        public void Init()
        {            

            foreach (var theme in GS2.externalThemes)
            {
              Debug.Log("Test"+theme.Value);
            }
        }

        public void CollapseAll()
        {
            foreach (var t in items)
            {
                t.gameObject.SetActive(false);
            }
        }

        public void ExpandAll()
        {
            foreach (var t in items) t.gameObject.SetActive(true);
        }

        public void CheckAll()
        {
            GS2.Warn("CheckAll");
            foreach (var t in items) t.Set(true);
        }

        public void SetAll(bool val)
        {
            if (val) CheckAll();
            else UnCheckAll();
        }

        public void ToggleAll()
        {
            GS2.Warn("Toggle");
            if (!masterToggle.isOn) UnCheckAll();
            else CheckAll();
        }
        public void UnCheckAll()
        {
            GS2.Warn("Uncheck All");
            foreach (var t in items) t.Set(false);
        }
        public ThemeLibrary Get()
        {
            var output = new ThemeLibrary();
            foreach (var tsi in items)
            {
                if (tsi.ticked) output.Add(tsi.theme.Name, tsi.theme);
            }

            return output;
        }

        public void MasterToggleClick()
        {
            Debug.Log("Click");
            Debug.Log(masterToggle.isOn.ToString());
           
        }
    }
}