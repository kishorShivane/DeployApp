using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DeployApp
{
    public partial class Deploy : Form
    {
        private static string dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static LogWriter logger = new LogWriter();
        private static bool EnableBackUP = System.Configuration.ConfigurationSettings.AppSettings["SmartCardFileBackUP"] == "true" ? true : false;
        public Deploy()
        {
            InitializeComponent();
        }

        #region DBF Converter

        private void btnGenDutyDbf_Click(object sender, EventArgs e)
        {
            try
            {
                btnGenDutyDbf.Enabled = false;
                OleDbConnection obdcconn = new OleDbConnection();
                string fileName = "DUTY.DBF";
                File.Copy(dir + "\\DutyTemplate.Dbf", dir + "\\" + fileName, true);
                logger.LogWriteDBF("File conversion started");
                lblStatus.Text = "File conversion is in progress.. Please wait..!!";
                obdcconn.ConnectionString = GetConnection(System.Configuration.ConfigurationSettings.AppSettings["DBFInputPath"]);
                obdcconn.Open();
                OleDbCommand oCmd = obdcconn.CreateCommand();
                oCmd.CommandText = "SELECT * FROM DUTYSEL.DBF;";

                /*Load data to table*/

                DataTable dt1 = new DataTable();
                dt1.Load(oCmd.ExecuteReader());

                string currentPath = System.Configuration.ConfigurationSettings.AppSettings["DBFOutputPath"];
                obdcconn.Close();
                logger.LogWriteDBF("Read contents of source dbf");
                WriteDataToTemplate(currentPath, fileName, dt1);
                lblStatus.Text = "File conversion completed, file is in " + currentPath + fileName;
                btnGenDutyDbf.Enabled = true;
            }
            catch (System.Exception ex)
            {
                btnGenDutyDbf.Enabled = true;
                logger.LogWriteDBF(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public static int GetRecordCount()
        {
            OleDbConnection obdcconn = new OleDbConnection
            {
                ConnectionString = GetConnection(dir)
            };
            obdcconn.Open();
            OleDbCommand oCmd = obdcconn.CreateCommand();
            oCmd.CommandText = "SELECT * FROM DUTY.DBF;";

            /*Load data to table*/

            DataTable dt1 = new DataTable();
            dt1.Load(oCmd.ExecuteReader());
            return dt1.Rows.Count;
        }

        public static void WriteDataToTemplate(string filePath, string fileName, DataTable table)
        {
            try
            {
                logger.LogWriteDBF("Writing content to destination dbf file");
                OleDbConnection odbcConnection = new OleDbConnection(GetConnection(dir));
                OleDbCommand oCmd = null;
                //c = GetRecordCount();
                oCmd = odbcConnection.CreateCommand();
                odbcConnection.Open();
                oCmd.CommandText = "INSERT INTO " + fileName + " VALUES ('0000',' 0000','0000','0000','0','0000',0,0,0,'')";
                oCmd.ExecuteNonQuery();
                //c = GetRecordCount();
                oCmd = odbcConnection.CreateCommand();
                oCmd.CommandText = @"DELETE FROM " + fileName + " WHERE BOARD <> '0000';";
                int affected = oCmd.ExecuteNonQuery();
                odbcConnection.Close();
                //c = GetRecordCount();

                int count = 0;
                try
                {
                    odbcConnection.Open();
                    foreach (DataRow row in table.Rows)
                    {
                        count++;
                        oCmd = odbcConnection.CreateCommand();
                        string insertSql = "INSERT INTO " + fileName + " VALUES(";
                        string values = "";
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            string value = ReplaceEscape(row[i].ToString());
                            switch (i)
                            {
                                case 1:
                                    value = " " + value;
                                    break;
                                case 2:
                                    value = value.PadRight(6, ' ');
                                    break;
                                case 6:
                                    value = CalculateDay(value);
                                    values = values + value + ",";
                                    values = values + "0,";
                                    values = values + "0,";
                                    break;
                                case 7:
                                    values = "'" + value + "'," + values;
                                    break;
                                default:
                                    break;
                            }
                            if (i != 6 && i != 0 && i != 7)
                            {
                                values = values + "'" + value + "',";
                            }
                        }

                        values = values.Substring(0, values.Length - 1) + ")";
                        insertSql = insertSql + values;
                        oCmd.CommandText = insertSql;
                        oCmd.ExecuteNonQuery();
                        oCmd.Dispose();
                    }
                    odbcConnection.Close();
                }
                catch (System.Exception ex)
                {
                    logger.LogWriteDBF(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                //c = GetRecordCount();
                odbcConnection.Open();
                oCmd = odbcConnection.CreateCommand();
                oCmd.CommandText = @"DELETE FROM " + fileName + " WHERE BOARD='0000'";
                oCmd.ExecuteNonQuery();
                odbcConnection.Close();
                //c = GetRecordCount();
                CopyTemplateToWorkingDir(filePath, fileName);
                logger.LogWriteDBF("Records: " + count.ToString());
                logger.LogWriteDBF("File conversion completed: " + filePath + "\\" + fileName);
            }
            catch (System.Exception ex)
            {
                logger.LogWriteDBF(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private static void CopyTemplateToWorkingDir(string filePath, string fileName)
        {
            string dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileFullPath = filePath + fileName;
            File.Copy(dir + "\\" + fileName, fileFullPath, true);
        }

        private static string CalculateDay(string value)
        {
            char[] dayChar = value.ToCharArray();
            int count = 0;
            for (int item = 0; item < dayChar.Length; item++)
            {
                switch (dayChar[item])
                {
                    case 'M':
                        count += 1;
                        break;
                    case 'T':
                        if (item == 1)
                        {
                            count += 2;
                        }
                        else
                        {
                            count += 8;
                        }
                        break;
                    case 'W':
                        count += 4;
                        break;
                    case 'F':
                        count += 16;
                        break;
                    case 'S':
                        if (item == 5)
                        {
                            count += 32;
                        }
                        else
                        {
                            count += 64;
                        }

                        break;
                    default:
                        break;
                }
            }
            return count.ToString();
        }

        private static string GetConnection(string path)
        {
            //return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=dBASE III;";
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=dBASE III;";
        }

        public static string ReplaceEscape(string str)
        {
            str = str.Replace("'", "''");
            return str;
        }

        #endregion


        #region Smartcard Hotlisting
        private void btnHotListFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDecimal.Text.Trim().Length >= 10)
                {
                    lblSmartCard.Text = "Please wait while we are processing..!!";
                    decimal decValue = Convert.ToDecimal(txtDecimal.Text);
                    logger.LogWriteSmartCard("LCR Conversion Started: " + txtDecimal.Text);
                    string lCRCode = GenerateLCRCode(decValue);
                    logger.LogWriteSmartCard("LCR Converted String of " + txtDecimal.Text + " : " + lCRCode);
                    UpdateSerialNumber();
                    AppendLCRCodeToFile(lCRCode);
                    logger.LogWriteSmartCard("LCR Converted String appended to file successfully.");
                    txtDecimal.Text = "";
                    lblSmartCard.Text = "Smartcard Hotlisting Process Completed..!!";
                    MessageBox.Show("Smartcard Hotlisting Process Completed..!!");
                }
                else
                {
                    MessageBox.Show("Please enter 10 Digit Decimal number..!!");
                }
            }
            catch (Exception ex)
            {
                logger.LogWriteSmartCard(ex.Message);
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public string GenerateLCRCode(decimal decValue)
        {
            string hexValue = decValue.ToHexString();
            string result = "FF 00 00 ##HEX## 00 00 ##LCR##";
            string PINFeed = "5A";
            if (hexValue.Length < 8)
            {
                hexValue = hexValue.PadLeft(8, '0'); //RIGHT HERE!!!
            }

            char[] charHex = hexValue.ToCharArray();
            string hexResult = charHex[0].ToString() + charHex[1].ToString()
                + " " + charHex[2].ToString() + charHex[3].ToString()
                + " " + charHex[4].ToString() + charHex[5].ToString()
                + " " + charHex[6].ToString() + charHex[7].ToString();
            string[] splitHex = hexResult.Split(' ');
            int LCR = ((((Convert.ToInt32(PINFeed, 16) ^ Convert.ToInt32(splitHex[0], 16))
                ^ Convert.ToInt32(splitHex[1], 16)) ^ Convert.ToInt32(splitHex[2], 16))
                ^ Convert.ToInt32(splitHex[3], 16));

            result = result.Replace("##HEX##", hexResult).Replace("##LCR##", LCR.ToString("X").PadLeft(2, '0'));

            return result;
        }

        private void AppendLCRCodeToFile(string lCRCode)
        {
            try
            {
                string path = System.Configuration.ConfigurationSettings.AppSettings["SmartCardTextPath"];

                if (File.Exists(path))
                {
                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(" " + lCRCode);
                    }
                }
                else
                {
                    MessageBox.Show("Smartctl.txt file not found..!!");
                }
            }
            catch (Exception ex)
            {
                logger.LogWriteSmartCard(ex.Message);
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void UpdateSerialNumber()
        {
            try
            {
                string path = System.Configuration.ConfigurationSettings.AppSettings["SmartCardTextPath"];
                string backUpPath = System.Configuration.ConfigurationSettings.AppSettings["SmartCardFileBackUpPath"];
                List<string> lines = new List<string>();
                string serialNumber = Get8Digits();
                if (File.Exists(path))
                {
                    if (EnableBackUP)
                    {
                        if (!Directory.Exists(backUpPath))
                        {
                            Directory.CreateDirectory(backUpPath);
                        }
                        logger.LogWriteSmartCard("smartctl.txt file back up created with name -  BackUP_" + serialNumber + ".txt");
                        File.Copy(path, backUpPath + "\\BackUP_" + serialNumber + ".txt", true);
                    }

                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            string text = s.Trim();
                            if (text.StartsWith("\"") && text.EndsWith("\""))
                            {
                                lines.Add(" \"" + serialNumber + "\"");
                            }
                            else
                            {
                                lines.Add(s);
                            }
                        }
                        sr.Close();
                    }

                    File.WriteAllText(path, "");

                    using (StreamWriter tw = new StreamWriter(path))
                    {
                        lines.ForEach(x =>
                        {
                            tw.WriteLine(x);
                        });
                        tw.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Smartctl.txt file not found..!!");
                }
                logger.LogWriteSmartCard("Updated serial number to : " + serialNumber);
            }
            catch (Exception ex)
            {
                logger.LogWriteSmartCard(ex.Message);
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public string Get8Digits()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return string.Format("{0:D8}", random);
        }
        #endregion


        #region WCM
        private void btnWCM_Click(object sender, EventArgs e)
        {
            try
            {
                string exePath = System.Configuration.ConfigurationSettings.AppSettings["WCMExePath"];
                Process.Start(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
        #endregion
    }
}
