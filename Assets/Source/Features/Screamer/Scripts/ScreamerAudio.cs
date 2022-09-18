using System;
using System.Collections;
using UnityEngine;

namespace Notes
{
    public class ScreamerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _stepsAudioSource;
        [SerializeField] private AudioSource _screamAudioSource;

        public void PlayScream(Action onEndScream)
        {
            if (_screamAudioSource.isPlaying)
                throw new InvalidOperationException();
            
            _screamAudioSource.Play();
            StartCoroutine(WaitScream(onEndScream));
        }

        public void PlaySteps() => _stepsAudioSource.Play();

        public void StopSteps() => _stepsAudioSource.Stop();

        private IEnumerator WaitScream(Action onEndScream)
        {
            yield return new WaitUntil(() =>_screamAudioSource.isPlaying == false);
            onEndScream?.Invoke();
        }
    }
}
