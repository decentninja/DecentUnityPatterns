/*
  ABSTRACT SMART TEMPLATE - CRUD for the lazy
  From: https://github.com/decentninja/DecentUnityPatterns

  A performant API for lists, useful for UI or other repeatedly instantiated items by keeping the templates in the hierarchy and separating data from the view.

  + Lowers the amount of prefabs
  + Keeps everything previewable in the editor
  
  Inherit from AbstractSmartTemplate and pass it a data structure that can be compared with IEquatable.
  Structs are ideal as they already implement this interface and fits the use case.
  Then call PopulateAndUpdate as many times as you like, if need be every frame, the hierarchy is only touched if required.
  Don't override Awake.
  Works great nested by passing PopulateAndUpdate down the hierarchy and scaling off only what you need.
  If you end up reusing the template as a prefab, you can override the Template() method.
  
  EXAMPLE:
      CODE:
          public class ItemSmartTemplate : AbstractSmartTemplate<ItemSmartTemplate, Data>
          {
              [SerializeField] Text name, age;
              internal override void Set(Data data)
              {
                  name.text = data.Name;
                  age.text = data.Age.ToString();
              }
          }

          ... in another file

          struct Data {
            string Name;
            int Age;
          }
          itemSmartTemplate.PopulateAndUpdate(new Data[]{new Data("Charles", 32), new Data("Emma", 25)});

      GAME OBJECT HIERARKY:
          Scrollview
              Content
                  Item Template : ItemSmartTemplate
                      Name : Text (Assigned to name);
                      Age : Text (Assigned to age);
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSmartTemplate<T, D> : MonoBehaviour where T : AbstractSmartTemplate<T, D> {

    public class NotMasterException : Exception { }

    List<T> m_currentData = new List<T>();
    bool m_master = true;
    D m_data;

    void Awake() {
        if (m_master) {
            gameObject.SetActive(false);
        }
    }

    void MasterCheck() {
        if (!m_master) throw new NotMasterException();
    }

    public T Instantiate(D data) {
        MasterCheck();
        T thing = Instantiate(Template());
        thing.transform.SetParent(transform.parent);
        thing.m_master = false;
        thing.gameObject.SetActive(true);
        thing.m_data = data;
        thing.Set(data);
        m_currentData.Add(thing);
        return thing;
    }

    public void PopulateAndUpdate(IEnumerable<D> datas) {
        MasterCheck();
        int i = 0;
        foreach (var data in datas) {
            if (i < m_currentData.Count) {
                if (!m_currentData[i].m_data.Equals(data)) {
                    m_currentData[i].m_data = data;
                    m_currentData[i].Set(data);
                }
            } else {
                Instantiate(data);
            }
            i++;
        }
        int j = i;
        for (; j < m_currentData.Count; j++) {
            Destroy(m_currentData[j].gameObject);
        }
        m_currentData.RemoveRange(i, j - i);
    }

    internal virtual T Template() {
        return (T) this;
    }

    internal abstract void Set(D data);

}
