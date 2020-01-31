using UnityEngine;

namespace Utils
{
    public abstract class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
    {
        [SerializeField]
        private bool _isPersistent;
        
        private static T _instance;

        public static T GetInstance(bool? isPersistent = null)
        {
            if (_instance)
            {
                if (isPersistent.HasValue)
                {
                    _instance._isPersistent = isPersistent.Value && _instance._isPersistent;
                }

                return _instance;
            }

            var instance = FindObjectOfType<T>();

            var isSetToPersistent = isPersistent ?? false;
            if (instance)
            {
                if (instance._isPersistent || isSetToPersistent)
                {
                    instance._isPersistent = true;
                    DontDestroyOnLoad(instance);
                }

                _instance = instance;
                return _instance;
            }

            var gameObject = new GameObject();
            instance = gameObject.AddComponent<T>();
            instance._isPersistent = isSetToPersistent;

            _instance = instance;
            return _instance;
        }

        protected void Awake()
        {
            if (_isPersistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected void OnDestroy()
        {
            _instance = null;
        }
    }
}