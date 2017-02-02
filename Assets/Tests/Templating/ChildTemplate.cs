using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildTemplate : AbstractSmartTemplate<ChildTemplate, UI.Item.Child> {

    [SerializeField] Text m_name;

    internal override void Set(UI.Item.Child data) {
        m_name.text = data.Name;
    }

}
