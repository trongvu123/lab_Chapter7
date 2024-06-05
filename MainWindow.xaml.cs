using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabMyStoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CategoryService _categoryService;
        private List<Category> _categories;

        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "Data Source=localhost,1433;Initial Catalog=MyStore;User ID=sa;Password=123456;TrustServerCertificate=True";
            _categoryService = new CategoryService(connectionString);
            LoadCategories();
        }

        private void LoadCategories()
        {
            _categories = _categoryService.GetAllCategories();
            CategoriesDataGrid.ItemsSource = _categories;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                var newCategory = new Category { Name = name };
                _categoryService.AddCategory(newCategory);
                LoadCategories();
                NameTextBox.Clear(); // Clear the TextBox after adding
            }
            else
            {
                MessageBox.Show("Please enter a valid name.");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
            {
                string name = NameTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    selectedCategory.Name = name;
                    _categoryService.UpdateCategory(selectedCategory);
                    LoadCategories();
                    NameTextBox.Clear(); // Clear the TextBox after updating
                }
                else
                {
                    MessageBox.Show("Please enter a valid name.");
                }
            }
            else
            {
                MessageBox.Show("Please select a category to update.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
            {
                _categoryService.DeleteCategory(selectedCategory.Id);
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
            }
        }
    }
}