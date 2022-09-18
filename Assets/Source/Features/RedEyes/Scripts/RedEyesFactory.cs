using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace RedEyes
{
    public class RedEyesFactory : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private RedEyes _eyesTemplate;
        [SerializeField] private MinMaxCurve _spawnDistanceToTarget = new MinMaxCurve(1, new AnimationCurve(), new AnimationCurve());
        [SerializeField] private float _spawnDelayInSeconds = 150;
        [SerializeField] private float _offsetY = 0.5f;

        private void Start()
        {
            StartCoroutine(SpawningEyes());
        }

        private IEnumerator SpawningEyes()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelayInSeconds);
                
                var spawnedEyes = Instantiate(_eyesTemplate, 
                    _target.position + _target.forward * _spawnDistanceToTarget.Evaluate(Random.value) + Vector3.up * _offsetY, 
                    Quaternion.identity);
                
                spawnedEyes.Initialize(_target);
            }
        }
    }
}