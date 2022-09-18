using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AYellowpaper;
using Notes;
using CharacterMovement.Interfaces;
using UnityEditor.Experimental.GraphView;

namespace Journal
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Journal : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IInput> _input = null;
        [SerializeField] private NotesInteractor _notesInteractor = null;
        [SerializeField] private List<TMP_Text> _textContainers = new List<TMP_Text>();
        [SerializeField] private Button _left = null;
        [SerializeField] private Button _right = null;

        private readonly List<string> _notes = new List<string>();
        private CanvasGroup _canvasGroup = null;
        private int _currentIndex = 0;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            ReRender();
        }

        private void OnEnable()
        {
            _notesInteractor.PickedUp += OnNotePickedUp;
            _left.onClick.AddListener(OnLeftClicked);
            _right.onClick.AddListener(OnRightClicked);
            _input.Value.JournalKeyPressed += OnJournalKeyPressed;
        }

        private void OnDisable()
        {
            _notesInteractor.PickedUp -= OnNotePickedUp;
            _left.onClick.RemoveListener(OnLeftClicked);
            _right.onClick.RemoveListener(OnRightClicked);

            if (_input.Value != null)
            {
                _input.Value.JournalKeyPressed -= OnJournalKeyPressed;
            }
        }

        public void Add(string note)
        {
            _notes.Add(note);
        }
        
        private void ReRender()
        {
            var notes = _notes.Skip(_currentIndex * _textContainers.Count).Take(4);

            var i = 0;
            foreach (var note in notes)
            {
                _textContainers[i].text = note;

                i++;
            }

            for (; i < _textContainers.Count; i++)
            {
                _textContainers[i].text = "";
            }

            _left.gameObject.SetActive(true);
            _right.gameObject.SetActive(true);
            if (_currentIndex <= 0)
            {
                _left.gameObject.SetActive(false);
            }
            if ((_currentIndex + 1) * _textContainers.Count >= _notes.Count)
            {
                _right.gameObject.SetActive(false);
            }
        }

        private void OnNotePickedUp(string note)
        {
            _notes.Add(note);

            ReRender();
        }

        private void OnLeftClicked()
        {
            _currentIndex--;

            ReRender();
        }

        private void OnRightClicked()
        {
            _currentIndex++;

            ReRender();
        }

        private void OnJournalKeyPressed()
        {
            _canvasGroup.enabled = !_canvasGroup.enabled;

            if (_canvasGroup.enabled)
            {
                _input.Value.CursorLock();

                return;
            }

            _input.Value.CursorUnlock();
        }
    }
}
