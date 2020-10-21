using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BaguetFactory
{
    static class Storage
    {

        public static readonly string connectionString = @"Data Source=DESKTOP-HNPN4VJ;Initial Catalog=BaguetStorage;Integrated Security=True";

        public static readonly string takenMaterialString = @"D:\BaguetStorage\TakenMaterial.txt";
        public static void MaterialTakingPerWeek(double Amount)
        {
            double currentState;
            using (StreamReader sr = new StreamReader(takenMaterialString))
            {
                currentState = Double.Parse(sr.ReadLine());
            }
            using (StreamWriter sw = new StreamWriter(takenMaterialString, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(currentState += Amount);
            }

        }
        public static double MaterialShowingPerWeek()
        {
            using (StreamReader sr = new StreamReader(takenMaterialString))
            {
                return Double.Parse(sr.ReadLine());
            }
        }
        public static bool MaterialTakingFromDB(Type material, double Amount)
        {
            string _material = material.ToString();
            _material = _material.Substring(14);

            string sqlExpression = "SELECT Amount FROM StorageInventory WHERE Type = '" + _material + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();


                double MaterialAmount = 0;
                while (reader.Read())
                {
                    MaterialAmount = Convert.ToDouble(reader.GetValue(0));
                }

                if (Amount < MaterialAmount)
                {
                    sqlExpression = "UPDATE StorageInventory SET Amount = '" + (MaterialAmount - Amount)
                        + "' WHERE Type = '" + _material + "';";
                    reader.Close();
                    command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    throw new Exception("There is not enough material in Storage!");

                }
            }
        }
        public static bool MaterialTakingFromFile(Type material, double Amount)
        {
            bool b;
            string matPath;
            double amount;
            double _amount;
            string finalString = " ";
            try
            {
                matPath = @"D:\BaguetStorage\" + material.ToString().Substring(14) + "Storage.txt";

                using (StreamReader sr = new StreamReader(matPath, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (material.ToString().Substring(14) == line.Substring(4, 15).Replace(" ", ""))
                        {
                            amount = Convert.ToDouble(line.Substring(26));
                            _amount = amount - Amount;
                            finalString = line.Substring(0, 26) + _amount;
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter(matPath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(finalString);
                    b = true;
                }
            }
            catch (Exception)
            {
                b = false;
            }
            return b;
        }
        public static DataTable ShowStorage()
        {
            DataSet ds;
            SqlDataAdapter adapter;
            string sql = "SELECT * FROM StorageInventory";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }
        public static string ShowStorage(string path)
        {
            string stateOfStorage = "";
            using (StreamReader sr = new StreamReader(path + "WoodStorage.txt"))
            {
                stateOfStorage += sr.ReadLine() + "\n";
            }
            using (StreamReader sr = new StreamReader(path + "MetalProfileStorage.txt"))
            {
                stateOfStorage += sr.ReadLine() + "\n";
            }
            using (StreamReader sr = new StreamReader(path + "PlasticProfileStorage.txt"))
            {
                stateOfStorage += sr.ReadLine() + "\n";
            }
            using (StreamReader sr = new StreamReader(path + "DyeStorage.txt"))
            {
                stateOfStorage += sr.ReadLine() + "\n";
            }
            using (StreamReader sr = new StreamReader(path + "PolishStorage.txt"))
            {
                stateOfStorage += sr.ReadLine() + "\n";
            }
            return stateOfStorage;
        }
        public static void changeInFile(Type material, double Amount)
        {
            Console.WriteLine(material.ToString().Substring(14));
            double amount;
            string finalString;
            string line;
            using (StreamReader sr = new StreamReader(@"D:\BaguetStorage\" + material.ToString().Substring(14) + "Storage.txt"))
            {
                line = sr.ReadLine();
                amount = double.Parse(line.Substring(26));
                finalString = line.Substring(0, 26);
            }
            amount += Amount;
            finalString += amount;
            using (StreamWriter sw = new StreamWriter(@"D:\BaguetStorage\" + material.ToString().Substring(14) + "Storage.txt", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(finalString);
            }

        }
    }
}
