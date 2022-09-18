using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Screamer
{
    public class Screamer : MonoBehaviour, IScreamer
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _showDuration = 0.5f;

        private Coroutine _showingCoroutine;

        private void Awake()
        {
            Hide();
        }
        
        public void Hide()
        {
            if (_showingCoroutine != null)
                StopCoroutine(_showingCoroutine);
            
            _image.enabled = false;
            _image.transform.localScale = Vector3.zero;
        }

        public void Show()
        {
            if (_showingCoroutine != null)
                throw new InvalidOperationException("Already showing");
            
            _image.enabled = true;
            _showingCoroutine = StartCoroutine(Showing());
        }

        private IEnumerator Showing()
        {
            float elapsedTime = 0;

            while (elapsedTime <= _showDuration)
            {
                elapsedTime += Time.deltaTime;
                _image.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / _showDuration);

                yield return null;
            }
            
            _image.transform.localScale = Vector3.one;
            _showingCoroutine = null;
        }
    }
}