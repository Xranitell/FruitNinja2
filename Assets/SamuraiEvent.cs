using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiEvent : MonoBehaviour
{
        public static SamuraiEvent instance;
        [SerializeField] private Animator anim;
    
        private void Awake()
        {
            instance = this;
        }
    
        public void StartAnimation()
        {
            anim.SetTrigger("StartSamuraiEvent");
        }
        
}
