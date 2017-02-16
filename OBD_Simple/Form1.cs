using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace OBD_Simple
{
    public partial class Form1 : Form
    {
        public static string StartUp_Folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        public static string New_List_File = "\\Storage Card\\OBD_Mobile_List.obd";
        public static string Firm_List_File = StartUp_Folder + "\\OBD_Mobile_List.dat";

        public List<string> OBD_File = new List<string>();
        public List<string> OBD_List = new List<string>();
        public List<string> Search_Result_List = new List<string>();


        public Form1()
        {
            InitializeComponent();
        }

        void GZ_DeCompress(string file, string outputFile)
        {
            try
            {
                using (var inStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (var zipStream = new GZipStream(inStream, CompressionMode.Decompress, true))
                        {
                            byte[] buffer = new byte[inStream.Length];

                            while (true)
                            {
                                int count = zipStream.Read(buffer, 0, buffer.Length);

                                if (count != 0)
                                    outStream.Write(buffer, 0, count);

                                if (count != buffer.Length)
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error decompressing file {0}: {1}", file, ex.Message));
            }
        }	
        public static string[] File_ReadAllLines(string path, System.Text.Encoding encoding)
        {
            ArrayList al = new ArrayList();
            StreamReader sr = new StreamReader(path, encoding);
            string line = sr.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                al.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            return (string[])al.ToArray(typeof(string));
        }
        void SetKeyboardVisible(bool isVisible)
        {
            inputPanel.Enabled = isVisible;
        }
        bool Is_Car_Registration(string In_String)
        {
            foreach (char Is_Digit in In_String.ToCharArray())
            {
                if (Char.IsNumber(Is_Digit))
                {
                    return true;
                }
            }
            return false;
        }

        string Add_Status(string In_Data)
        {
            string[] inx = In_Data.Split(',');
            string[] outx = new string[inx.Length];
            for (int idx = 0; idx < inx.Length; idx++)
            {
                if (inx[idx].Contains("+0") || inx[idx].Contains("+1") || inx[idx].Contains("+2"))
                {
                    outx[idx] = inx[idx];
                }
                else
                {
                    outx[idx] = inx[idx] + "+1";
                }
            }
            return string.Join(",", outx).Replace(",+1", string.Empty);
        }
        string Remove_Status(string In_Data)
        {
            string[] inx = In_Data.Split(',');
            string[] outx = new string[inx.Length];
            for (int idx = 0; idx < inx.Length; idx++)
            {
                if (inx[idx].Contains("+"))
                {
                    outx[idx] = inx[idx].Substring(0, inx[idx].Length - 2);
                }
                else
                {
                    outx[idx] = inx[idx];
                }
            }
            return string.Join(",", outx);
        }	
        
        void Load_OBD_List()
        {
            OBD_File.AddRange(File_ReadAllLines(Firm_List_File, Encoding.ASCII));
            OBD_List.AddRange(OBD_File.GetRange(1, OBD_File.Count - 1));
        }
        string Smart_Filter(string Search_For, string[] Where)
        {
            List<string> outx = new List<string>();
            for (int Test = 0; Test < Where.Length; Test++)
            {
                if (Where[Test].ToLower().Contains(Search_For.ToLower()))
                {
                    outx.Add(Test.ToString());
                }
            }
            return string.Join(",", outx.ToArray());
        }
        void Smart_Search(string Search_For)
        {
            Search_Result_List.Clear();
            comboBox_Search_Results.Items.Clear();

            for (int Main_Node = 0; Main_Node < OBD_List.ToArray().Length; Main_Node++)
            {
                string Main_Firm = OBD_List[Main_Node].Split('|')[0];
                for (int Sub_Node = 0; Sub_Node <  OBD_List[Main_Node].Split('|')[1].Split('/').Length; Sub_Node++ )
                {
                    string SC = OBD_List[Main_Node].Split('|')[1].Split('/')[Sub_Node];
					if ( Is_Car_Registration(Search_For) )
					{
                        if (SC.Split('=')[1].Split(':')[1].ToLower().Contains(Search_For.ToLower()))
                        {
                            Search_Result_List.Add("V" + "|" +
                                                   Main_Firm + "," + SC.Split('=')[0] + "|" +
                                                   Main_Node.ToString() + "," + Sub_Node.ToString() + "|" +
                                                   Smart_Filter(Search_For, SC.Split('=')[1].Split(':')[1].Split(',')));
                        }
                    } else {
                        if (SC.Split('=')[1].Split(':')[0].ToLower().Contains(Search_For.ToLower()))
                        {
                            Search_Result_List.Add("P" + "|" +
                                                   Main_Firm + "," + SC.Split('=')[0] + "|" +
                                                   Main_Node.ToString() + "," + Sub_Node.ToString() + "|" +
                                                   Smart_Filter(Search_For, SC.Split('=')[1].Split(':')[0].Split(',')));
                        }
                    }
                }
            }
            foreach (string Found_Items in Search_Result_List.ToArray())
            {
                comboBox_Search_Results.Items.Add(Found_Items.Split('|')[1]);
            }

            if (comboBox_Search_Results.Items.Count > 0)
            {
                comboBox_Search_Results.Visible = true;
            }
            else
            {
                comboBox_Search_Results.Visible = false;
                label_Search_Info.Visible = true;
            }
        }
        private void textBox_Search_Item_GotFocus(object sender, EventArgs e)
        {
            label_Search_Info.Visible = false;
            comboBox_Search_Results.Visible = false;
            comboBox_Search_Results.Items.Clear();
            lBox_Personnel.Visible = false;
            lBox_Vehicles.Visible = false;
            lBox_Personnel.Items.Clear();
            lBox_Vehicles.Items.Clear();
            label_Info_Personnel.Visible = false;
            label_Info_Car_Registrations.Visible = false;
            label_Search_Info.Visible = false;
            textBox_Search_Item.Text = string.Empty;
            SetKeyboardVisible(true);
        }
        private void textBox_Search_Item_LostFocus(object sender, EventArgs e)
        {
            SetKeyboardVisible(false);
        }
        private void button_Search_Click(object sender, EventArgs e)
        {
            if (textBox_Search_Item.Text.Length >= 3)
            {
                comboBox_Search_Results.Items.Clear();
                comboBox_Search_Results.Visible = false;
                Smart_Search(textBox_Search_Item.Text);
            }
            else if (textBox_Search_Item.Text.Length < 3)
            {
                textBox_Search_Item.Text = "Upiši bar 3 slova ...";
            }
        }
        private void comboBox_Search_Results_SelectedIndexChanged(object sender, EventArgs e)
        {
			string What_Is_Found = string.Empty;
            int Firma = -1, SC = -1, Found_Items_Count = -1;
			string[] Found_Items = null;
			What_Is_Found = Search_Result_List[comboBox_Search_Results.SelectedIndex].Split('|')[0];
            lBox_Personnel.Visible = true;
            lBox_Vehicles.Visible = true;
            Firma = int.Parse(Search_Result_List[comboBox_Search_Results.SelectedIndex].Split('|')[2].Split(',')[0]);
            SC = int.Parse(Search_Result_List[comboBox_Search_Results.SelectedIndex].Split('|')[2].Split(',')[1]);
			Found_Items = Search_Result_List[comboBox_Search_Results.SelectedIndex].Split('|')[3].Split(',');
			Found_Items_Count = Found_Items.Length;
            label_Info_Personnel.Visible = true;
            label_Info_Car_Registrations.Visible = true;
            lBox_Personnel.Items.Clear();
            lBox_Vehicles.Items.Clear();

			switch ( What_Is_Found )
			{
				case "P":
                    foreach (string Filter in Found_Items)
                    {
                        lBox_Personnel.Items.Add(OBD_List[Firma].Split('|')[1].Split('/')[SC].Split('=')[1].Split(':')[0].Split(',')[int.Parse(Filter)]);
                    }
                    foreach (string Item_V in OBD_List[Firma].Split('|')[1].Split('/')[SC].Split('=')[1].Split(':')[1].Split(','))
                    {
                        lBox_Vehicles.Items.Add(Item_V.Replace("+0", ", Zabrana ulaza!").Replace("+1", ", Dozvoljen ulaz u dnevnu!").Replace("+2", ", Dozvoljen ulaz u nocnu!").Replace("+3", "Uvijek dozvoljen ulaz!"));
                    }
                    break;
				case "V":
                    foreach (string Filter in Found_Items)
                    {
                        lBox_Vehicles.Items.Add(OBD_List[Firma].Split('|')[1].Split('/')[SC].Split('=')[1].Split(':')[1].Split(',')[int.Parse(Filter)].Replace("+0", ", Zabrana ulaza!").Replace("+1", ", Dozvoljen ulaz u dnevnu!").Replace("+2", ", Dozvoljen ulaz u nocnu!").Replace("+3", "Uvijek dozvoljen ulaz!"));
                    }
                    foreach (string Item_P in OBD_List[Firma].Split('|')[1].Split('/')[SC].Split('=')[1].Split(':')[0].Split(','))
                    {
                        lBox_Personnel.Items.Add(Item_P);
                    }
					break;
			}
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(New_List_File))
            {
                GZ_DeCompress(New_List_File, Firm_List_File);
                File.Delete(New_List_File);
            }
            if (File.Exists(Firm_List_File))
            {
                Load_OBD_List();
            }
        }

    }
}