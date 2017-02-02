/*
  CHEAT - For fun and profit
  From: https://github.com/decentninja/DecentUnityPatterns

  A good way to make testing faster and easier.

  Add the cheat script to scenes that are allowed to cheat.
  Call RegisterCheat on the weak singleton Cheats with (name, key, callback).
  See Start() for more examples.

  EXAMPLE:
    Cheats.Use(cheats => {
        cheats.RegisterCheat("God mode", "g", () => {
            m_canTakeDamage = false;
        });
    });
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cheats : WeakSingleton<Cheats> {

    public class KeyAlreadyAssignedException : Exception { }

    public struct Cheat {
        public string Name;
        public string Key;
        public delegate void Do();
        public Do What;

        public Cheat(string name, string key, Do what) {
            Name = name;
            Key = key;
            What = what;
        }

        public string Activated() {
            return Name + " triggered";
        }
    }

    List<Cheat> m_cheats = new List<Cheat>();
    HashSet<string> m_takenKeys = new HashSet<string>();

    public void RegisterCheat(string name, string key, Cheat.Do what) {
        if (!m_takenKeys.Contains(key)) {
            m_takenKeys.Add(key);
            var cheat = new Cheat(name, key, what);
            m_cheats.Add(cheat);
        } else {
            throw new KeyAlreadyAssignedException();
        }
    }

    void Start() {
        RegisterCheat("Print cheats", "c", () => {
            Debug.Log("All registered cheats are");
            Debug.Log("Key Name");
            foreach (var cheat in m_cheats) {
                Debug.Log(cheat.Key + " " + cheat.Name);
            }
        });
        bool slowtime = false;
        RegisterCheat("Bullet time", "b", () => {
            if (!slowtime) {
                slowtime = true;
                Time.timeScale = 0.1f;
            } else {
                Time.timeScale = 1;
                slowtime = false;
            }
        });
        RegisterCheat("Restart", "r", () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    void Update() {
        foreach (var cheat in m_cheats) {
            if (Input.GetKeyDown(cheat.Key)) {
                Debug.Log(cheat.Activated());
                cheat.What();
            }
        }
    }

}
