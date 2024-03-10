using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnsBuster
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            if(null == instance)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}