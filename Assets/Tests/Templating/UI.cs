using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    [SerializeField] ItemTemplate m_itemTemplate;

    public struct Item {
        public struct Child {
            public string Name;
            public Child(string name) {
                Name = name;
            }
        }
        public string Title, Subtitle;
        public Child[] Children;
        public Item(string title, string subtitle, Child[] children) {
            Title = title;
            Subtitle = subtitle;
            Children = children;
        }
    }


    Item[] m_items = new Item[] {
        new Item("One", "Little", new Item.Child[] {
            new Item.Child("Good"), new Item.Child("Bad")
        }),
        new Item("Two", "Soft", new Item.Child[] {
            new Item.Child("Warm"), new Item.Child("Wow")
        }),
        new Item("Tree", "Middle", new Item.Child[] {
            new Item.Child("Kitten"), new Item.Child("Word")
        }),
        new Item("Four", "Hard", new Item.Child[] {
            new Item.Child("Will you even notice?"), new Item.Child("Bad")
        }),
        new Item("Five", "Easy", new Item.Child[] {
            new Item.Child("Good"), new Item.Child("Bad")
        }),
        new Item("Six", "Big", new Item.Child[] {
            new Item.Child("Good"), new Item.Child("Bad")
        })
    };

    void Start () {
        m_itemTemplate.PopulateAndUpdate(m_items);
	}
	
}
