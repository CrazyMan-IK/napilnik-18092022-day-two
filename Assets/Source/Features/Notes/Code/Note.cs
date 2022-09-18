using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Notes
{
    public class Note : MonoBehaviour
    {
        [SerializeField] private string _text = null;

        public string Text => _text;
    }
}
