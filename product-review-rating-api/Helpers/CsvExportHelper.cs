using System.Text;

namespace product_review_rating_api.Helpers
{
    public static class CsvExportHelper
    {
        public static byte[] Export<T>(IEnumerable<T> data)
        {
            var sb = new StringBuilder();
            var properties = typeof(T).GetProperties();

            // Header
            sb.AppendLine(string.Join(",", properties.Select(p => $"\"{p.Name}\"")));

            // Data rows
            foreach (var item in data)
            {
                var values = properties.Select(p => $"\"{p.GetValue(item)?.ToString()?.Replace("\"", "\"\"")}\"");
                sb.AppendLine(string.Join(",", values));
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }

}
