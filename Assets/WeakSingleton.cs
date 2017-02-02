/*
  WEAK SINGLETON - The Penultimate Singleton for Unity
  From: https://github.com/decentninja/DecentUnityPatterns

  A good way to decouple singletons by wrapping their use in a callback,
  forcing users to think about the situation when the singleton is missing.

  + Makes all scenes playable from editor, even if long distant dependencies are missing
  + Reduces coupling!

  Inherit from WeakSingleton
  To access the singleton from anywhere call bool Use(callback),
  it will return true if the singleton existed in the scene and therefore ran your code.
  Don't save any references out of the WeakSingleton.
  Don't override Awake.

  EXAMPLE:
      public class PlayerSingleton : WeakSingleton<PlayerSingleton>
      {
        public void die()
        {
            Destroy(gameObject);
        }
      }

      ... in another file

      PlayerSingleton.Use(player =>
      {
          player.die();
      });

 */

using UnityEngine;

public abstract class WeakSingleton<T> : MonoBehaviour where T : WeakSingleton<T> {

    public delegate void Callback(T instance);

    private static T m_instance = null;
    private static bool m_warned = false;

    public static bool Use(Callback callback) {
        if (m_instance)
            callback(m_instance);
        else if (!m_warned) {
            Debug.Log(typeof(T).Name + " did not answer because it is not instanced. Should be OK if this is a test bed.");
            m_warned = true;
        }
        return m_instance != null;
    }

    void Awake() {
        if (m_instance) {
            Debug.LogError("More than 1 " + typeof(T).Name + " in scene!");
            return;
        }
        m_instance = (T)this;
    }

    void OnDestroy() {
        m_instance = null;
    }

}
