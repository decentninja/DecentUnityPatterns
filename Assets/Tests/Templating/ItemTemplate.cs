using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTemplate : AbstractSmartTemplate<ItemTemplate, UI.Item>  {

    [SerializeField] Text m_title, m_subtitle;
    [SerializeField] ChildTemplate m_childTemplate;

    internal override void Set(UI.Item data) {
        m_title.text = data.Title;
        m_subtitle.text = data.Subtitle;
        m_childTemplate.PopulateAndUpdate(data.Children);
    }

}
