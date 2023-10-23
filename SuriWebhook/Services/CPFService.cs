using Npgsql;
using System.Text.RegularExpressions;

namespace SuriWebhook.Services
{
    public class CPFService
    {
        private readonly DatabaseService _databaseService;

        public CPFService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool validateCPF(string cpf)
        {
            string cleanedCPF = new string(cpf.Where(char.IsDigit).ToArray());

            if (cleanedCPF.Length != 11)
            {
                return false;
            }

            if (cleanedCPF.Distinct().Count() == 1)
            {
                return false;
            }

            int[] digits = cleanedCPF.Select(c => int.Parse(c.ToString())).ToArray();
            int sum1 = 0;
            int sum2 = 0;

            for (int i = 0; i < 9; i++)
            {
                sum1 += digits[i] * (10 - i);
            }

            for (int i = 0; i < 10; i++)
            {
                sum2 += digits[i] * (11 - i);
            }

            int remainder1 = (sum1 * 10) % 11;
            int remainder2 = (sum2 * 10) % 11;

            if (remainder1 == 10) remainder1 = 0;
            if (remainder2 == 10) remainder2 = 0;

            return remainder1 == digits[9] && remainder2 == digits[10];
        }

        public string GetPDFByCPF(string cpf)
        {
            string connectionString = _databaseService.GetConnectionString();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    "SELECT content FROM public.boletos WHERE cpf = @CPF",
                    connection))
                {
                    command.Parameters.AddWithValue("@CPF", cpf);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string pdfPath = reader["content"] as string;
                            return pdfPath;
                        }
                    }
                }
                connection.Close();
            }

            return null;
        }



    }
}
