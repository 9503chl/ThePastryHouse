using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    public sealed class ButtonGroup : MonoBehaviour
    {
        [SerializeField]
        private int defaultIndex = -1;

#if UNITY_2019_1_OR_NEWER
        [Tooltip("Use pressed state instead of selected state for transition.")]
        [SerializeField]
        private bool usePressedState = true;
#endif

        [SerializeField]
        private bool hideSelection = false;

        public bool HideSelection
        {
            get
            {
                return hideSelection;
            }
            set
            {
                hideSelection = value;
                SetButtonPressed(selectedButton, !hideSelection);
            }
        }

        public Button[] Buttons = new Button[0];

        public UnityEvent onClick;

        public event UnityAction<Button, bool> OnSelect;

        [NonSerialized]
        private Dictionary<Button, Sprite> buttonSprites = new Dictionary<Button, Sprite>();

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
                if (selectedButton != deselectedButton && !hideSelection)
                {
                    SetButtonPressed(deselectedButton, false);
                    SetButtonPressed(selectedButton, true);
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
                if (selectedButton != deselectedButton && !hideSelection)
                {
                    SetButtonPressed(deselectedButton, false);
                    SetButtonPressed(selectedButton, true);
                }
            }
        }

#if UNITY_EDITOR
        [NonSerialized]
        private bool showSelection = true;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                if (showSelection == hideSelection)
                {
                    showSelection = !hideSelection;
                    SetButtonPressed(selectedButton, !hideSelection);
                }
            }
        }
#endif

        private void Awake()
        {
            foreach (Button button in Buttons)
            {
                if (button != null)
                {
                    if (!buttonSprites.ContainsKey(button) && button.image != null)
                    {
                        buttonSprites.Add(button, button.image.sprite);
                    }
                    button.onClick.AddListener(delegate { ButtonClick(button); });
                }
            }
        }

        private void Start()
        {
            if (selectedButton == null)
            {
                if (defaultIndex >= 0 && defaultIndex < Buttons.Length)
                {
                    selectedButton = Buttons[defaultIndex];
                    if (!hideSelection)
                    {
                        SetButtonPressed(selectedButton, true);
                    }
                }
            }
        }

        private void SetButtonPressed(Button button, bool pressed)
        {
            if (button != null)
            {
                switch (button.transition)
                {
                    case Selectable.Transition.ColorTint:
                        if (button.image != null)
                        {
#if UNITY_2019_1_OR_NEWER
                            button.image.color = pressed ? (usePressedState ? button.colors.pressedColor : button.colors.selectedColor) : button.colors.normalColor;
#else
                            button.image.color = pressed ? button.colors.pressedColor : button.colors.normalColor;
#endif
                        }
                        break;
                    case Selectable.Transition.SpriteSwap:
                        if (button.image != null)
                        {
                            if (!buttonSprites.ContainsKey(button))
                            {
                                buttonSprites.Add(button, button.image.sprite);
                            }
#if UNITY_2019_1_OR_NEWER
                            button.image.sprite = pressed ? (usePressedState ? button.spriteState.pressedSprite : button.spriteState.selectedSprite) : buttonSprites[button];
#else
                            button.image.sprite = pressed ? button.spriteState.pressedSprite : buttonSprites[button];
#endif
                        }
                        break;
                    case Selectable.Transition.Animation:
                        if (button.animator != null)
                        {
#if UNITY_2019_1_OR_NEWER
                            button.animator.SetTrigger(pressed ? (usePressedState ? button.animationTriggers.pressedTrigger : button.animationTriggers.selectedTrigger) : button.animationTriggers.normalTrigger);
#else
                            button.animator.SetTrigger(pressed ? button.animationTriggers.pressedTrigger : button.animationTriggers.normalTrigger);
#endif
                        }
                        break;
                }
                if (OnSelect != null)
                {
                    OnSelect.Invoke(button, pressed);
                }
            }
        }

        private void ButtonClick(Button button)
        {
            if (isActiveAndEnabled)
            {
                SelectedButton = button;
                if (onClick != null)
                {
                    onClick.Invoke();
                }
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetInteractable(bool value)
        {
            foreach (Button button in Buttons)
            {
                if (button != null)
                {
                    button.interactable = value;
                }
            }
        }

        public void Add(Button button)
        {
            if (button != null)
            {
                List<Button> buttons = new List<Button>(Buttons);
                buttons.Add(button);
                Buttons = buttons.ToArray();
                if (!buttonSprites.ContainsKey(button) && button.image != null)
                {
                    buttonSprites.Add(button, button.image.sprite);
                }
                button.onClick.AddListener(delegate { ButtonClick(button); });
            }
        }

        public void Remove(Button button)
        {
            if (button != null)
            {
                if (button == selectedButton)
                {
                    if (!hideSelection)
                    {
                        SetButtonPressed(button, false);
                    }
                    selectedButton = null;
                }
                List<Button> buttons = new List<Button>(Buttons);
                buttons.Remove(button);
                if (buttonSprites.ContainsKey(button))
                {
                    buttonSprites.Remove(button);
                }
                button.onClick.RemoveAllListeners();
                Buttons = buttons.ToArray();
            }
        }

        public void Clear()
        {
            foreach (Button button in Buttons)
            {
                if (button != null)
                {
                    if (buttonSprites.ContainsKey(button))
                    {
                        buttonSprites.Remove(button);
                    }
                    button.onClick.RemoveAllListeners();
                }
            }
            Buttons = new Button[0];
            buttonSprites.Clear();
            selectedButton = null;
        }
    }
}
