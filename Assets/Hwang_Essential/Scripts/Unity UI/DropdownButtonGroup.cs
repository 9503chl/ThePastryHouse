using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public sealed class DropdownButtonGroup : MonoBehaviour
    {
        public enum ButtonOrder
        {
            None,
            MoveToFirst,
            MoveToLast
        }

        [SerializeField]
        private int defaultIndex = -1;

        public Image DropdownBackground;

        public Button DropdownButton;
        public Sprite CollapsedImage;
        public Sprite ExpandedImage;

        public ButtonOrder ChangeButtonOrder;

        public Button[] Buttons = new Button[0];

        public UnityEvent onClick;
        public UnityEvent onCancel;

        [NonSerialized]
        private bool isDroppedDown = false;

        public bool IsDroppedDown
        {
            get
            {
                return isDroppedDown;
            }
            set
            {
                SetDropdownState(value);
            }
        }

        [NonSerialized]
        private Button selectedButton;

        public Button SelectedButton
        {
            get
            {
                return selectedButton;
            }
            set
            {
                Button deselectedButton = selectedButton;
                selectedButton = value;
                if (selectedButton != deselectedButton)
                {
                    SetButtonSibling(deselectedButton, false);
                    SetButtonSibling(selectedButton, true);
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (Buttons[i] == selectedButton)
                    {
                        return i;
                    }
                }
                return -1;
            }
            set
            {
                Button deselectedButton = selectedButton;
                selectedButton = null;
                if (value >= 0 && value < Buttons.Length)
                {
                    selectedButton = Buttons[value];
                }
                if (selectedButton != deselectedButton)
                {
                    SetButtonSibling(deselectedButton, false);
                    SetButtonSibling(selectedButton, true);
                }
            }
        }

        private void Awake()
        {
            foreach (Button button in Buttons)
            {
                if (button != null)
                {
                    button.onClick.AddListener(delegate { ButtonClick(button); });
                }
            }
            if (DropdownButton != null)
            {
                DropdownButton.onClick.AddListener(delegate { SetDropdownState(!isDroppedDown); });
            }
        }

        private void Start()
        {
            if (selectedButton == null)
            {
                if (defaultIndex >= 0 && defaultIndex < Buttons.Length)
                {
                    selectedButton = Buttons[defaultIndex];
                    SetButtonSibling(selectedButton, true);
                }
            }
            SetDropdownState(isDroppedDown);
        }

        private void OnDisable()
        {
            if (isDroppedDown)
            {
                SetDropdownState(false);
                if (onCancel != null)
                {
                    onCancel.Invoke();
                }
            }
        }

        private IEnumerator Checkup()
        {
            EventSystem.current.SetSelectedGameObject(DropdownButton.gameObject);
            yield return new WaitForEndOfFrame();

            while (isDroppedDown)
            {
                GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
                bool cancel = true;
                if (currentSelected == DropdownButton.gameObject)
                {
                    cancel = false;
                }
                else
                {
                    foreach (Button button in Buttons)
                    {
                        if (button != null && currentSelected == button.gameObject)
                        {
                            cancel = false;
                            break;
                        }
                    }
                }
                if (cancel)
                {
                    SetDropdownState(false);
                    if (onCancel != null)
                    {
                        onCancel.Invoke();
                    }
                }
                yield return null;
            }
        }

        private void SetDropdownState(bool expanded)
        {
            if (expanded)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (Buttons[i] != null)
                    {
                        Buttons[i].gameObject.SetActive(true);
                    }
                }
                if (DropdownBackground != null)
                {
                    DropdownBackground.gameObject.SetActive(true);
                }
                if (DropdownButton != null)
                {
                    DropdownButton.image.sprite = ExpandedImage;
                }
                isDroppedDown = true;
                if (isActiveAndEnabled)
                {
                    StartCoroutine(Checkup());
                }
            }
            else
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    if (Buttons[i] != null && Buttons[i] != selectedButton)
                    {
                        Buttons[i].gameObject.SetActive(false);
                    }
                }
                if (DropdownBackground != null)
                {
                    DropdownBackground.gameObject.SetActive(false);
                }
                if (DropdownButton != null)
                {
                    DropdownButton.image.sprite = CollapsedImage;
                }
                isDroppedDown = false;
            }
        }

        private void SetButtonSibling(Button button, bool selected)
        {
            if (button != null && ChangeButtonOrder != ButtonOrder.None)
            {
                button.gameObject.SetActive(selected);
                if (selected)
                {
                    if (ChangeButtonOrder == ButtonOrder.MoveToFirst)
                    {
                        button.transform.SetAsFirstSibling();
                    }
                    else
                    {
                        button.transform.SetAsLastSibling();
                    }
                }
                else
                {
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (Buttons[i] == button)
                        {
                            Buttons[i].transform.SetSiblingIndex(i);
                            break;
                        }
                    }
                }
            }
        }

        private void ButtonClick(Button button)
        {
            if (isActiveAndEnabled)
            {
                if (isDroppedDown)
                {
                    SelectedButton = button;
                    SetDropdownState(false);
                    if (onClick != null)
                    {
                        onClick.Invoke();
                    }
                }
                else
                {
                    SetDropdownState(true);
                }
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
