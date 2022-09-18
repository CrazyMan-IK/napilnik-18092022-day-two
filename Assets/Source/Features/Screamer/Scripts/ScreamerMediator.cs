using System;
using System.Collections;
using AYellowpaper;
using Notes;
using UnityEngine;

namespace Screamer
{
    public class ScreamerMediator : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IScreamer> _screamer;
        [SerializeField] private ScreamerCondition _screamerCondition;
        [SerializeField] private ScreamerAudio _audio;
        [SerializeField] private float _startScreamerDelayInSeconds = 600;

        public event Action Scared;
        
        private void Awake()
        {
            StartCoroutine(ScreamingWork());
        }

        private IEnumerator ScreamingWork()
        {
            bool scared = false;
            
            while (scared == false)
            {
                yield return new WaitForSeconds(_startScreamerDelayInSeconds);
                
                _audio.PlaySteps();
                _screamerCondition.StartCheck(onEndCheck: (screamerResult) =>
                {
                    _audio.StopSteps();
                    switch (screamerResult)
                    {
                        case ScreamerResult.Scared:
                            scared = true;
                            _screamer.Value.Show();
                            _audio.PlayScream(onEndScream: () => Scared?.Invoke());
                            break;
                        case ScreamerResult.NotScared:
                            break;
                    }
                });

                yield return new WaitUntil(() => _screamerCondition.Playing == false);
            }
        }
    }
}
