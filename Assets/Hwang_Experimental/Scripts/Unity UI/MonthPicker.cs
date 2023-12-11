using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    public class MonthPicker : MonoBehaviour
    {
        public Button CurrentYearButton;
        public Text CurrentYearText;
        public Button PreviousYearButton;
        public Button NextYearButton;
        public LayoutGroup MonthGrid;
        public Image CurrentCellImage;
        public Image SelectedCellImage;
        public Color NormalTextColor = Color.white;
        public Color OutsideTextColor = Color.gray;

        private const string YEAR_TEXT_FORMAT = "yyyy";
        private const string MONTH_TEXT_FORMAT = "MMMM";

        [SerializeField]
        private SystemLanguage calendarLanguage = SystemLanguage.Unknown;
        public SystemLanguage CalendarLanguage
        {
            get
            {
                return calendarLanguage;
            }
            set
            {
                if (calendarLanguage != value)
                {
                    calendarLanguage = value;
                    RebuildCalendar();
                }
            }
        }

        [SerializeField]
        private string yearTextFormat = YEAR_TEXT_FORMAT;
        public string YearTextFormat
        {
            get
            {
                return yearTextFormat;
            }
            set
            {
                if (yearTextFormat != value)
                {
                    yearTextFormat = value;
                    RebuildCalendar();
                }
            }
        }

        [SerializeField]
        private string monthTextFormat = MONTH_TEXT_FORMAT;
        public string MonthTextFormat
        {
            get
            {
                return monthTextFormat;
            }
            set
            {
                if (monthTextFormat != value)
                {
                    monthTextFormat = value;
                    RebuildCalendar();
                }
            }
        }

        [SerializeField]
        private bool interactable = true;
        public bool Interactable
        {
            get
            {
                return interactable;
            }
            set
            {
                if (interactable != value)
                {
                    interactable = value;
                    SetInteractable();
                }
            }
        }

        [SerializeField]
        private bool showOutsideCells = true;
        public bool ShowOutsideCells
        {
            get
            {
                return showOutsideCells;
            }
            set
            {
                if (showOutsideCells != value)
                {
                    showOutsideCells = value;
                    RebuildCalendar();
                }
            }
        }

        [SerializeField]
        private DateTime currentDate = DateTime.Today;
        public DateTime CurrentDate
        {
            get
            {
                return currentDate;
            }
            set
            {
                if (currentDate != value)
                {
                    currentDate = value;
                    RebuildCalendar();
                }
            }
        }

        [SerializeField]
        private DateTime selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    RebuildCalendar();
                }
            }
        }

        public UnityEvent onSelect = new UnityEvent();

        private class Cell
        {
            public Button CellButton;
            public Text CellText;
            public bool IsInside;
            public int CellValue;

            public static UnityAction<Button> OnButtonClick;

            public Cell(Button cellButton, bool isInside, int cellValue)
            {
                CellButton = cellButton;
                cellButton.onClick.RemoveAllListeners();
                cellButton.onClick.AddListener(CellButtonClick);
                CellText = cellButton.GetComponentInChildren<Text>();
                IsInside = isInside;
                CellValue = cellValue;
            }

            private void CellButtonClick()
            {
                if (OnButtonClick != null)
                {
                    OnButtonClick(CellButton);
                }
            }
        }

        [NonSerialized]
        private List<Cell> cells = new List<Cell>();

        private void Start()
        {
            FillMonthCells();
            RebuildCalendar();
        }

        private void OnEnable()
        {
            if (CurrentYearButton != null)
            {
                CurrentYearButton.interactable = interactable;
                CurrentYearButton.onClick.AddListener(CurrentYearButtonClick);
            }
            if (PreviousYearButton != null)
            {
                PreviousYearButton.interactable = interactable;
                PreviousYearButton.onClick.AddListener(PreviousYearButtonClick);
            }
            if (NextYearButton != null)
            {
                NextYearButton.interactable = interactable;
                NextYearButton.onClick.AddListener(NextYearButtonClick);
            }
            Cell.OnButtonClick += CellButtonClick;
            RebuildCalendar();
        }

        private void OnDisable()
        {
            if (CurrentYearButton != null)
            {
                CurrentYearButton.onClick.RemoveListener(CurrentYearButtonClick);
            }
            if (PreviousYearButton != null)
            {
                PreviousYearButton.onClick.RemoveListener(PreviousYearButtonClick);
            }
            if (NextYearButton != null)
            {
                NextYearButton.onClick.RemoveListener(NextYearButtonClick);
            }
            Cell.OnButtonClick -= CellButtonClick;
        }

        private void FillMonthCells()
        {
            if (MonthGrid != null)
            {
                int month = -1;
                cells.Clear();
                Button[] cellButtons = MonthGrid.GetComponentsInChildren<Button>();
                foreach (Button cellButton in cellButtons)
                {
                    if (month <= 0) // November and December of previous year
                    {
                        cells.Add(new Cell(cellButton, false, month - 1));
                    }
                    else if (month > 12) // January and Febrary of next year
                    {
                        cells.Add(new Cell(cellButton, false, month - 12));
                    }
                    else // January to December of current year
                    {
                        cells.Add(new Cell(cellButton, true, month));
                    }
                    month++;
                }
            }
        }

        private void SetInteractable()
        {
            if (CurrentYearButton != null)
            {
                CurrentYearButton.interactable = interactable;
            }
            if (PreviousYearButton != null)
            {
                PreviousYearButton.interactable = interactable;
            }
            if (NextYearButton != null)
            {
                NextYearButton.interactable = interactable;
            }
        }

        private CultureInfo GetCultureInfo()
        {
            if (calendarLanguage != SystemLanguage.Unknown)
            {
                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                string languageName = string.Format("{0}", calendarLanguage);
                if (calendarLanguage == SystemLanguage.Chinese)
                {
                    languageName = "zh-CN"; // "Chinese (Simplified)"
                }
                else if (calendarLanguage == SystemLanguage.ChineseSimplified)
                {
                    languageName = "zh-Hans"; // "zh-CHS" is legacy name of "Chinese (Simplified)"
                }
                else if (calendarLanguage == SystemLanguage.ChineseTraditional)
                {
                    languageName = "zh-Hant"; // "zh-CHT" is legacy name of "Chinese (Traditional)"
                }
                else if (calendarLanguage == SystemLanguage.SerboCroatian)
                {
                    languageName = "hr"; // "Serbo-Croatian" and "sh" are deprecated. "Bosnian", "bs", "Croatian", "hr", "Serbian" or "sr" should be used.
                }
                foreach (CultureInfo culture in cultures)
                {
                    if (string.Compare(culture.Name, languageName, true) == 0 || string.Compare(culture.DisplayName, languageName, true) == 0)
                    {
                        return culture;
                    }
                }
            }
            return CultureInfo.CurrentCulture;
        }

        private string GetDateTimeText(DateTime dt, string format, string defFormat)
        {
            if (!string.IsNullOrEmpty(format))
            {
                try
                {
                    return dt.ToString(format, GetCultureInfo());
                }
                catch (Exception)
                {
                }
            }
            return dt.ToString(defFormat, GetCultureInfo());
        }

        private void SetCurrentCell(Cell cell)
        {
            if (CurrentCellImage != null)
            {
                if (Application.isPlaying)
                {
                    if (cell != null)
                    {
                        CurrentCellImage.transform.SetParent(cell.CellButton.transform);
                        CurrentCellImage.transform.SetAsFirstSibling();
                        CurrentCellImage.rectTransform.anchoredPosition = Vector2.zero;
                        CurrentCellImage.enabled = true;
                    }
                    else
                    {
                        CurrentCellImage.transform.SetParent(transform);
                        CurrentCellImage.rectTransform.anchoredPosition = Vector2.zero;
                        CurrentCellImage.enabled = false;
                    }
                }
                else
                {
                    CurrentCellImage.enabled = false;
                }
            }
        }

        private void SetSelectedCell(Cell cell)
        {
            if (SelectedCellImage != null)
            {
                if (Application.isPlaying)
                {
                    if (cell != null)
                    {
                        SelectedCellImage.transform.SetParent(cell.CellButton.transform);
                        SelectedCellImage.transform.SetAsLastSibling();
                        SelectedCellImage.rectTransform.anchoredPosition = Vector2.zero;
                        SelectedCellImage.enabled = true;
                    }
                    else
                    {
                        SelectedCellImage.transform.SetParent(transform);
                        SelectedCellImage.rectTransform.anchoredPosition = Vector2.zero;
                        SelectedCellImage.enabled = false;
                    }
                }
                else
                {
                    SelectedCellImage.enabled = false;
                }
            }
        }

        private void RebuildCalendar()
        {
            SetCurrentCell(null);
            SetSelectedCell(null);
            if (CurrentYearText != null)
            {
                CurrentYearText.text = GetDateTimeText(currentDate, yearTextFormat, YEAR_TEXT_FORMAT);
            }
            DateTime todayDate = DateTime.Today;
            DateTime cellDate;
            foreach (Cell cell in cells)
            {
                if (cell.IsInside)
                {
                    cellDate = new DateTime(currentDate.Year, cell.CellValue, 1);
                    cell.CellText.color = NormalTextColor;
                    cell.CellText.enabled = true;
                    if (cellDate.Year == todayDate.Year && cellDate.Month == todayDate.Month)
                    {
                        SetCurrentCell(cell);
                    }
                    if (cellDate.Year == selectedDate.Year && cellDate.Month == selectedDate.Month)
                    {
                        SetSelectedCell(cell);
                    }
                }
                else if (cell.CellValue < 0)
                {
                    cellDate = new DateTime(currentDate.Year, 1, 1).AddMonths(cell.CellValue);
                    cell.CellText.color = OutsideTextColor;
                    cell.CellText.enabled = showOutsideCells;
                }
                else
                {
                    cellDate = new DateTime(currentDate.Year, 12, 1).AddMonths(cell.CellValue);
                    cell.CellText.color = OutsideTextColor;
                    cell.CellText.enabled = showOutsideCells;
                }
                cell.CellText.text = GetDateTimeText(cellDate, monthTextFormat, MONTH_TEXT_FORMAT);
                cell.CellButton.enabled = cell.CellText.enabled;
            }
        }

        private void CurrentYearButtonClick()
        {
            currentDate = DateTime.Today;
            RebuildCalendar();
        }

        private void PreviousYearButtonClick()
        {
            currentDate = currentDate.AddYears(-1);
            RebuildCalendar();
        }

        private void NextYearButtonClick()
        {
            currentDate = currentDate.AddYears(1);
            RebuildCalendar();
        }

        public void CellButtonClick(Button cellButton)
        {
            if (interactable)
            {
                if (cellButton != null)
                {
                    foreach (Cell cell in cells)
                    {
                        if (cell.CellButton == cellButton)
                        {
                            if (cell.IsInside)
                            {
                                currentDate = new DateTime(currentDate.Year, cell.CellValue, 1);
                            }
                            else if (cell.CellValue < 0)
                            {
                                currentDate = new DateTime(currentDate.Year, 1, 1).AddMonths(cell.CellValue);
                            }
                            else
                            {
                                currentDate = new DateTime(currentDate.Year, 12, 1).AddMonths(cell.CellValue);
                            }
                            selectedDate = currentDate;
                            RebuildCalendar();
                            onSelect.Invoke();
                            break;
                        }
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            FillMonthCells();
            RebuildCalendar();
        }
#endif
    }
}
