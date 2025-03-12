using System;
using UnityEngine;

namespace _Scripts.Player.Utils
{
    public class TriggerBoxUtility : MonoBehaviour
    {
        private IController _controller;

        private void Awake()
        {
            _controller = GetComponentInChildren<IController>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _controller.OnTriggerEnter(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            _controller.OnTriggerExit(other);
        }
    }
}