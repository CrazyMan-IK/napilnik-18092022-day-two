using CharacterMovement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screamer
{
    public class TestRestartMenu : MonoBehaviour
    {
        [SerializeField] private ScreamerMediator _screamerMediator;
        [SerializeField] private Movement _movement;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _canvas.enabled = false;
        }

        private void OnEnable()
        {
            _screamerMediator.Scared += OnScared;
        }

        private void OnDisable()
        {
            _screamerMediator.Scared -= OnScared;
        }

        private void Update()
        {
            if (_canvas.enabled == false)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnScared()
        {
            _canvas.enabled = true;
            _movement.enabled = false;
        }
    }
}
