using BussinessLogicLayer.services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PresentationLayer {
    public partial class UcStatistics : UserControl {
        private readonly StatisticsService _statisticsService;
        private readonly EmployeeService _employeeService;
        private DataGrid _dgMostPopularBooks;
        private ItemsControl _icMostPopularGenres;
        private ItemsControl _icReviewCount;
        private ItemsControl _icIncomeStatistics;

        public UcStatistics() {
            InitializeComponent();
            _statisticsService = new StatisticsService();
            _employeeService = new EmployeeService();
            cmbStats.SelectionChanged += StatsComboBoxControl_SelectionChanged;
            cmbStats.SelectedIndex = 0;
        }

        private void StatsComboBoxControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cmbStats.SelectedItem != null) {
                int selectedOption = cmbStats.SelectedIndex;
                var libraryId = _employeeService.GetEmployeeLibraryId(LoggedUser.Username);
                ClearCurrentLayout();

                switch (selectedOption) {
                    case 0:
                        CreateLayoutMostPopularBooks(libraryId);
                        break;
                    case 1:
                        CreateLayoutMostPopularGenres(libraryId);
                        break;
                    case 2:
                        CreateLayoutReviewCount(libraryId);
                        break;
                    case 3:
                        CreateLayoutIncome(libraryId);
                        break;
                }
            }
        }

        private void ClearCurrentLayout() {
            if (_dgMostPopularBooks != null) grid.Children.Remove(_dgMostPopularBooks);
            if (_icMostPopularGenres != null) grid.Children.Remove(_icMostPopularGenres);
            if (_icReviewCount != null) grid.Children.Remove(_icReviewCount);
            if (_icIncomeStatistics != null) grid.Children.Remove(_icIncomeStatistics);
        }

        private void CreateLayoutIncome(int libraryId) {
            var incomeStatistics = _statisticsService.GetIncomeStatistics(libraryId);
            _icIncomeStatistics = CreateItemsControl("icIncomeStatistics", new List<IncomeStatistics> { incomeStatistics }, CreateIncomeStatisticsTemplate());

            AddControlToGrid(_icIncomeStatistics, 1, 0);
        }

        private void CreateLayoutReviewCount(int libraryId) {
            var reviewStatistics = _statisticsService.GetReviewCount(libraryId);
            _icReviewCount = CreateItemsControl("icReviewCount", reviewStatistics, CreateReviewStatisticsTemplate());

            AddControlToGrid(_icReviewCount, 1, 0);
        }

        private void CreateLayoutMostPopularGenres(int libraryId) {
            var mostPopularGenres = _statisticsService.GetMostPopularGenres(libraryId);
            _icMostPopularGenres = CreateItemsControl("icMostPopularGenres", mostPopularGenres, CreateMostPopularGenresTemplate());

            AddControlToGrid(_icMostPopularGenres, 1, 0);
        }

        private void CreateLayoutMostPopularBooks(int libraryId) {
            _dgMostPopularBooks = new DataGrid {
                Name = "dgMostPopularBooks",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                AutoGenerateColumns = false,
                IsReadOnly = true,
                ItemsSource = _statisticsService.GetMostPopularBooks(libraryId)
            };

            _dgMostPopularBooks.Columns.Add(CreateDataGridTextColumn("Ime knjige", "Book_Name"));
            _dgMostPopularBooks.Columns.Add(CreateDataGridTextColumn("Autor", "Author_Name"));
            _dgMostPopularBooks.Columns.Add(CreateDataGridTextColumn("Broj posudbi", "Times_Borrowed"));

            AddControlToGrid(_dgMostPopularBooks, 1, 0);
        }

        private DataGridTextColumn CreateDataGridTextColumn(string header, string bindingPath) {
            return new DataGridTextColumn {
                Header = header,
                Binding = new Binding(bindingPath),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
        }

        private ItemsControl CreateItemsControl(string name, IEnumerable<object> itemsSource, DataTemplate itemTemplate) {
            return new ItemsControl {
                Name = name,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                ItemsSource = itemsSource,
                ItemTemplate = itemTemplate
            };
        }

        private DataTemplate CreateIncomeStatisticsTemplate() {
            DataTemplate dataTemplate = new DataTemplate(typeof(IncomeStatistics));
            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);

            FrameworkElementFactory memberCountBorder = CreateBorderWithTextBlock("MemberCount", "Broj registriranih članova: {0}", 18.0);
            FrameworkElementFactory incomeBorder = CreateBorderWithStackPanel(new[] {
                CreateTextBlockFactory("TotalIncome", "Ukupni prihod od članarine: {0}", 18.0),
                CreateStaticTextBlockFactory(" €", 18.0)
            });

            stackPanelFactory.AppendChild(memberCountBorder);
            stackPanelFactory.AppendChild(incomeBorder);

            dataTemplate.VisualTree = stackPanelFactory;
            return dataTemplate;
        }

        private DataTemplate CreateReviewStatisticsTemplate() {
            DataTemplate dataTemplate = new DataTemplate(typeof(ReviewStatistics));
            FrameworkElementFactory borderFactory = CreateBorderFactory();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory gradeTextBlock = CreateTextBlockFactory("Grade", "Ocjena ({0})", 18.0, new Thickness(5, 0, 60, 0));
            FrameworkElementFactory countTextBlock = CreateTextBlockFactory("Number_Count", "", 18.0, new Thickness(60, 0, 0, 0));

            stackPanelFactory.AppendChild(gradeTextBlock);
            stackPanelFactory.AppendChild(countTextBlock);
            borderFactory.AppendChild(stackPanelFactory);

            dataTemplate.VisualTree = borderFactory;
            return dataTemplate;
        }

        private DataTemplate CreateMostPopularGenresTemplate() {
            DataTemplate dataTemplate = new DataTemplate(typeof(MostPopularGenres));
            FrameworkElementFactory borderFactory = CreateBorderFactory();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory genreNameTextBlock = CreateTextBlockFactory("Genre_name", "", 18.0, new Thickness(5, 0, 60, 0));
            FrameworkElementFactory timesBorrowedTextBlock = CreateTextBlockFactory("Times_Borrowed", "", 18.0, new Thickness(60, 0, 0, 0));

            stackPanelFactory.AppendChild(genreNameTextBlock);
            stackPanelFactory.AppendChild(timesBorrowedTextBlock);
            borderFactory.AppendChild(stackPanelFactory);

            dataTemplate.VisualTree = borderFactory;
            return dataTemplate;
        }

        private FrameworkElementFactory CreateBorderWithTextBlock(string bindingPath, string stringFormat, double fontSize) {
            FrameworkElementFactory borderFactory = CreateBorderFactory();

            FrameworkElementFactory textBlockFactory = CreateTextBlockFactory(bindingPath, stringFormat, fontSize);
            borderFactory.AppendChild(textBlockFactory);

            return borderFactory;
        }

        private FrameworkElementFactory CreateBorderWithStackPanel(FrameworkElementFactory[] children) {
            FrameworkElementFactory borderFactory = CreateBorderFactory();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            foreach (var child in children) {
                stackPanelFactory.AppendChild(child);
            }

            borderFactory.AppendChild(stackPanelFactory);
            return borderFactory;
        }

        private FrameworkElementFactory CreateBorderFactory() {
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(5));
            borderFactory.SetValue(Border.MarginProperty, new Thickness(1));
            return borderFactory;
        }

        private FrameworkElementFactory CreateTextBlockFactory(string bindingPath, string stringFormat, double fontSize, Thickness? margin = null) {
            FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding(bindingPath) { StringFormat = stringFormat });
            textBlockFactory.SetValue(TextBlock.FontSizeProperty, fontSize);
            if (margin.HasValue) {
                textBlockFactory.SetValue(TextBlock.MarginProperty, margin.Value);
            }
            return textBlockFactory;
        }

        private FrameworkElementFactory CreateStaticTextBlockFactory(string text, double fontSize) {
            FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetValue(TextBlock.TextProperty, text);
            textBlockFactory.SetValue(TextBlock.FontSizeProperty, fontSize);
            return textBlockFactory;
        }

        private void AddControlToGrid(UIElement control, int row, int column) {
            Grid.SetRow(control, row);
            Grid.SetColumn(control, column);
            grid.Children.Add(control);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            _statisticsService.Dispose();
            _employeeService.Dispose();
        }
    }
}
