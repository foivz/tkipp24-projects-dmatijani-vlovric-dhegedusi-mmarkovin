using BussinessLogicLayer.services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer {
    // Domagoj Hegedušić
    public partial class UcStatistics : UserControl {
        StatisticsService statisticsService = new StatisticsService();
        private DataGrid dgMostPopularBooks;
        private ItemsControl icMostPopularGenres;
        private ItemsControl icReviewCount;
        private ItemsControl icIncomeStatistics;
        public UcStatistics() {
            InitializeComponent();
            cmbStats.SelectionChanged += StatsComboBoxControl_SelectionChanged;
            cmbStats.SelectedIndex = 0;
        }

        private void StatsComboBoxControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cmbStats.SelectedItem != null) {

                int selectedOption = cmbStats.SelectedIndex;

                EmployeeService employeeService = new EmployeeService();
                var Library_id = employeeService.GetEmployeeLibraryId(LoggedUser.Username);

                switch (selectedOption) {
                    case 0:
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }
                        if (icReviewCount != null && grid.Children.Contains(icReviewCount)) {
                            grid.Children.Remove(icReviewCount);
                        }
                        if (icIncomeStatistics != null && grid.Children.Contains(icIncomeStatistics)) {
                            grid.Children.Remove(icIncomeStatistics);
                        }

                        CreateLayoutMostPopularBooks(Library_id);
                       
                        break;

                    case 1:

                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }
                        if (icReviewCount != null && grid.Children.Contains(icReviewCount)) {
                            grid.Children.Remove(icReviewCount);
                        }
                        if (icIncomeStatistics != null && grid.Children.Contains(icIncomeStatistics)) {
                            grid.Children.Remove(icIncomeStatistics);
                        }

                        CreateLayoutMostPopularGenres(Library_id);
                        break;


                    case 2: 
                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }
                        if (icIncomeStatistics != null && grid.Children.Contains(icIncomeStatistics)) {
                            grid.Children.Remove(icIncomeStatistics);
                        }

                        CreateLayoutReviewCount(Library_id);

                        break;

                    case 3: 
                        if (dgMostPopularBooks != null && grid.Children.Contains(dgMostPopularBooks)) {
                            grid.Children.Remove(dgMostPopularBooks);
                        }
                        if (icMostPopularGenres != null && grid.Children.Contains(icMostPopularGenres)) {
                            grid.Children.Remove(icMostPopularGenres);
                        }
                        if (icReviewCount != null && grid.Children.Contains(icReviewCount)) {
                            grid.Children.Remove(icReviewCount);
                        }

                        CreateLayoutIncome(Library_id);
                        break;

                    default:
                        break;
                }
            }
        }

        private void CreateLayoutIncome(int Library_id) {
            var incomeStatistics = statisticsService.GetIncomeStatistics(Library_id);

            icIncomeStatistics = new ItemsControl {
                Name = "icIncomeStatistics",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                ItemsSource = new List<IncomeStatistics> { incomeStatistics }
            };

            DataTemplate dataTemplate = new DataTemplate(typeof(IncomeStatistics));

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);

            FrameworkElementFactory border1Factory = new FrameworkElementFactory(typeof(Border));
            border1Factory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            border1Factory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            border1Factory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            border1Factory.SetValue(Border.PaddingProperty, new Thickness(5));
            border1Factory.SetValue(Border.MarginProperty, new Thickness(5, 0, 5, 2));

            FrameworkElementFactory textBlock1Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock1Factory.SetBinding(TextBlock.TextProperty, new Binding("MemberCount") {
                StringFormat = "Broj registriranih članova: {0}"
            });
            textBlock1Factory.SetValue(TextBlock.FontSizeProperty, 18.0);

            border1Factory.AppendChild(textBlock1Factory);

            FrameworkElementFactory border2Factory = new FrameworkElementFactory(typeof(Border));
            border2Factory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            border2Factory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            border2Factory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            border2Factory.SetValue(Border.PaddingProperty, new Thickness(5));
            border2Factory.SetValue(Border.MarginProperty, new Thickness(5, 0, 5, 2));

            FrameworkElementFactory stackPanel2Factory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanel2Factory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory textBlock2Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2Factory.SetBinding(TextBlock.TextProperty, new Binding("TotalIncome") {
                StringFormat = "Ukupni prihod od članarine: {0}"
            });
            textBlock2Factory.SetValue(TextBlock.FontSizeProperty, 18.0);

            FrameworkElementFactory textBlockEuroFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockEuroFactory.SetValue(TextBlock.TextProperty, " €");
            textBlockEuroFactory.SetValue(TextBlock.FontSizeProperty, 18.0);

            stackPanel2Factory.AppendChild(textBlock2Factory);
            stackPanel2Factory.AppendChild(textBlockEuroFactory);

            border2Factory.AppendChild(stackPanel2Factory);

            stackPanelFactory.AppendChild(border1Factory);
            stackPanelFactory.AppendChild(border2Factory);

            dataTemplate.VisualTree = stackPanelFactory;

            icIncomeStatistics.ItemTemplate = dataTemplate;

            Grid.SetRow(icIncomeStatistics, 1);
            Grid.SetColumn(icIncomeStatistics, 0);
            grid.Children.Add(icIncomeStatistics);
        }





        private void CreateLayoutReviewCount(int Library_id) {
            var ReviewStatistics = statisticsService.GetReviewCount(Library_id);

            icReviewCount = new ItemsControl {
                Name = "icReviewCount",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                ItemsSource = ReviewStatistics
            };

            DataTemplate dataTemplate = new DataTemplate(typeof(ReviewStatistics));

            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(5));
            borderFactory.SetValue(Border.MarginProperty, new Thickness(1));

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory textBlock1Factory = new FrameworkElementFactory(typeof(TextBlock));
            MultiBinding multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(new Binding("Grade"));
            multiBinding.StringFormat = "Ocjena ({0})";
            textBlock1Factory.SetBinding(TextBlock.TextProperty, multiBinding);
            textBlock1Factory.SetValue(TextBlock.MarginProperty, new Thickness(5, 0, 60, 0));
            textBlock1Factory.SetValue(TextBlock.FontSizeProperty, 18.0);

            FrameworkElementFactory textBlock2Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2Factory.SetBinding(TextBlock.TextProperty, new Binding("Number_Count"));
            textBlock2Factory.SetValue(TextBlock.MarginProperty, new Thickness(60, 0, 0, 0));
            textBlock2Factory.SetValue(TextBlock.FontSizeProperty, 18.0);
            textBlock2Factory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            stackPanelFactory.AppendChild(textBlock1Factory);
            stackPanelFactory.AppendChild(textBlock2Factory);

            borderFactory.AppendChild(stackPanelFactory);

            dataTemplate.VisualTree = borderFactory;

            icReviewCount.ItemTemplate = dataTemplate;

            Grid.SetRow(icReviewCount, 1);
            Grid.SetColumn(icReviewCount, 0);
            grid.Children.Add(icReviewCount);
        }



        public void CreateLayoutMostPopularGenres(int Library_id) {
            var mostPopularGenres = statisticsService.GetMostPopularGenres(Library_id);

            icMostPopularGenres = new ItemsControl {
                Name = "icMostPopularGenres",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                ItemsSource = mostPopularGenres
            };

            DataTemplate dataTemplate = new DataTemplate(typeof(MostPopularGenres));

            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(5));
            borderFactory.SetValue(Border.MarginProperty, new Thickness(1));

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory textBlock1Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock1Factory.SetBinding(TextBlock.TextProperty, new Binding("Genre_name"));
            textBlock1Factory.SetValue(TextBlock.MarginProperty, new Thickness(5, 0, 60, 0));
            textBlock1Factory.SetValue(TextBlock.FontSizeProperty, 18.0);

            FrameworkElementFactory textBlock2Factory = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2Factory.SetBinding(TextBlock.TextProperty, new Binding("Times_Borrowed"));
            textBlock2Factory.SetValue(TextBlock.MarginProperty, new Thickness(60, 0, 0, 0));
            textBlock2Factory.SetValue(TextBlock.FontSizeProperty, 18.0);
            textBlock2Factory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            stackPanelFactory.AppendChild(textBlock1Factory);
            stackPanelFactory.AppendChild(textBlock2Factory);

            borderFactory.AppendChild(stackPanelFactory);

            dataTemplate.VisualTree = borderFactory;

            icMostPopularGenres.ItemTemplate = dataTemplate;

            Grid.SetRow(icMostPopularGenres, 1);
            grid.Children.Add(icMostPopularGenres);
        }

        private void CreateLayoutMostPopularBooks(int Library_id) {
            dgMostPopularBooks = new DataGrid {
                Name = "dgMostPopularBooks",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 500,
                Height = 300,
                AutoGenerateColumns = false,
                IsReadOnly = true
            };

            DataGridTextColumn column1 = new DataGridTextColumn {
                Header = "Ime knjige",
                Binding = new Binding("Book_Name"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
            DataGridTextColumn column2 = new DataGridTextColumn {
                Header = "Autor",
                Binding = new Binding("Author_Name"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
            DataGridTextColumn column3 = new DataGridTextColumn {
                Header = "Broj posudbi",
                Binding = new Binding("Times_Borrowed"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star) 
            };

            dgMostPopularBooks.Columns.Add(column1);
            dgMostPopularBooks.Columns.Add(column2);
            dgMostPopularBooks.Columns.Add(column3);

            Grid.SetRow(dgMostPopularBooks, 1);
            Grid.SetColumn(dgMostPopularBooks, 0);
            grid.Children.Add(dgMostPopularBooks);

            dgMostPopularBooks.ItemsSource = statisticsService.GetMostPopularBooks(Library_id);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            statisticsService.Dispose();
        }
    }
}
