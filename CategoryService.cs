using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMyStoreWPF
{
    public class CategoryService
    {
        private readonly string _connectionString= "Data Source=localhost,1433;Initial Catalog=MyStore;User ID=sa;Password=123456;TrustServerCertificate=True";

        public CategoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT [CategoryID]
      ,[CategoryName]
  FROM [dbo].[Categories]
";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                Id = (int)reader["CategoryID"],
                                Name = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public void AddCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO [dbo].[Categories]([CategoryName]) VALUES (@Name)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE [dbo].[Categories] SET [CategoryName] = @Name WHERE CategoryID = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", category.Id);
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM [dbo].[Categories] WHERE CategoryID = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
