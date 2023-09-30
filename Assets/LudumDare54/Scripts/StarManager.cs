
    using UnityEngine;

    public class StarManager : MonoBehaviour
    {
        public static StarManager instance;

        public int stars = 0;
        
        void Awake()
        {
            instance = this;
        }
    }
