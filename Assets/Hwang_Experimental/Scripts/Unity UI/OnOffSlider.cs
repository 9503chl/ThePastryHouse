using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Slider))]
    public class OnOffSlider : MonoBehaviour
    {
        public Color ColorIsOn = Color.green;
        public Color ColorIsOff = Color.red;
        public Color ColorDisabled = Color.white;

        public bool ClickToToggle = true;

        [Serializable]
        public class OnOffSliderEvent : UnityEvent<bool> { }

        public OnOffSliderEvent onChanged = new OnOffSliderEvent();

        [NonSerialized]
        private Slider slider;

        [NonSerialized]
        private Image image;

        [NonSerialized]
        private Button button;

        [NonSerialized]
        private bool wasOn = false;

        public bool isOn
        {
            get
            {
                if (slider != null)
                {
                    return (slider.normalizedValue >= 0.5f);
                }
                return wasOn;
            }
            set
            {
                if (slider != null)
                {
                    SliderOnOff(value);
                }
                wasOn = value;
            }
        }

        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(SliderValueChanged);
            image = slider.handleRect.GetComponent<Image>();
            SetIsOnWithoutNotify(isOn);
            wasOn = isOn;
        }

        private void OnEnable()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
                slider.onValueChanged.AddListener(SliderValueChanged);
            }
            image = slider.handleRect.GetComponent<Image>();
            SetIsOnWithoutNotify(isOn);
            wasOn = isOn;
            if (Application.isPlaying)
            {
                button = slider.handleRect.GetComponent<Button>();
                if (button == null)
                {
                    button = slider.handleRect.gameObject.AddComponent<Button>();
                    button.transition = Selectable.Transition.None;
                    EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();
                    eventTrigger.AddListener(EventTriggerType.PointerDown, slider.OnPointerDown);
                    eventTrigger.AddListener(EventTriggerType.PointerUp, slider.OnPointerUp);
                    eventTrigger.AddListener(EventTriggerType.Drag, slider.OnDrag);
                }
                button.onClick.AddListener(ButtonClick);
            }
        }

        private void OnDisable()
        {
            if (image != null)
            {
                image.color = ColorDisabled;
            }
            if (Application.isPlaying)
            {
                if (button != null)
                {
                    button.onClick.RemoveListener(ButtonClick);
                }
            }
            slider = null;
        }

        private void SliderOnOff(bool turnOn)
        {
            if (turnOn)
            {
                image.color = ColorIsOn;
                if (Application.isPlaying)
                {
                    slider.value = slider.maxValue;
                }
                else
                {
                    slider.SetValueWithoutNotify(slider.maxValue);
                }
                if (!wasOn)
                {
                    onChanged.Invoke(true);
                }
            }
            else
            {
                image.color = ColorIsOff;
                if (Application.isPlaying)
                {
                    slider.value = slider.minValue;
                }
                else
                {
                    slider.SetValueWithoutNotify(slider.minValue);
                }
                if (wasOn)
                {
                    onChanged.Invoke(false);
                }
            }
            wasOn = turnOn;
        }

        private void SliderValueChanged(float value)
        {
            if (slider != null)
            {
                SliderOnOff(slider.normalizedValue >= 0.5f);
            }
        }

        private void ButtonClick()
        {
            if (ClickToToggle && slider.enabled)
            {
                SliderOnOff(!wasOn);
            }
        }

#if UNITY_EDITOR
        private float oldValue;

        private void Update()
        {
            if (slider != null)
            {
                if (oldValue != slider.value)
                {
                    oldValue = slider.value;
                    SetIsOnWithoutNotify(slider.normalizedValue >= 0.5f);
                }
            }
        }

        private void OnValidate()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
            if (slider != null && slider.handleRect != null)
            {
                image = slider.handleRect.GetComponent<Image>();
                if (image != null)
                {
                    if (isActiveAndEnabled)
                    {
                        if (isOn)
                        {
                            image.color = ColorIsOn;
                        }
                        else
                        {
                            image.color = ColorIsOff;
                        }
                    }
                    else
                    {
                        image.color = ColorDisabled;
                    }
                }
            }
        }
#endif

        public void SetIsOnWithoutNotify(bool isOn)
        {
            if (isOn)
            {
                image.color = ColorIsOn;
            }
            else
            {
                image.color = ColorIsOff;
            }
            slider.SetValueWithoutNotify(isOn ? slider.maxValue : slider.minValue);
            wasOn = isOn;
        }
    }
}
