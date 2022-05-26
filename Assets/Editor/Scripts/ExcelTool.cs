using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using App.Utilities;

public class ExcelTool
{
    private static string EXCEL_PATH = Application.dataPath + "/Editor/Excel/";
    private static string SCRIPT_PATH = Application.dataPath + "/Editor/Scripts/";

    [MenuItem("Tools/ExcelToBinary")]
    private static void ExcelToBin()
    {
        if (!Directory.Exists(EXCEL_PATH))
            Directory.CreateDirectory(EXCEL_PATH);
        if (!Directory.Exists(SCRIPT_PATH))
            Directory.CreateDirectory(SCRIPT_PATH);
        DirectoryInfo excel_dir = Directory.GetParent(EXCEL_PATH);
        FileInfo[] files = excel_dir.GetFiles();
        List<FileInfo> excels = new List<FileInfo>();
        foreach (FileInfo file in files)
            if (!file.Extension.Equals(".meta"))
                excels.Add(file);
        foreach (FileInfo excel in excels)
        {
            using (FileStream excel_fs = File.Open(excel.FullName, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(excel_fs);
                DataSet set = reader.AsDataSet();
                for (int i = 0; i < set.Tables.Count; i++)
                {
                    DataTable table = set.Tables[i];
                    SaveTable(table);
                    GenerateTableClass(table);
                    GenerateTableContainer(table);                   
                }
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("ExcelToBinary is Completed\n" + BinaryManager.DATA_DIR);
    }

    private static void GenerateTableClass(DataTable table)
    {
        if (!File.Exists(SCRIPT_PATH + table.TableName + ".cs"))
        {
            Dictionary<string, string> type_var = new Dictionary<string, string>();
            for (int i = 0; i < table.Columns.Count; i++)
                type_var.Add(table.Rows[0][i].ToString(), table.Rows[1][i].ToString());
            using (FileStream fs = new FileStream(SCRIPT_PATH + table.TableName + ".cs", FileMode.Create, FileAccess.Write))
            {
                string str = "public class " + table.TableName + "\n{\n";
                foreach (var pair in type_var)
                    str += ("\tpublic " + pair.Value + " " + pair.Key + ";\n");
                str += "}\n";
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }
    }

    private static void GenerateTableContainer(DataTable table)
    {
        if (!File.Exists(SCRIPT_PATH + table.TableName + "Container.cs"))
        {
            string keyType = "";
            for (int i = 0; i < table.Columns.Count; i++)
                if (table.Rows[2][i].Equals("key"))
                {
                    keyType = table.Rows[1][i].ToString();
                    break;
                }
            using (FileStream fs = new FileStream(SCRIPT_PATH + table.TableName + "Container.cs", FileMode.Create, FileAccess.Write))
            {
                string str = "using System.Collections.Generic;\n\npublic class " + table.TableName + "Container\n{\n";
                str += "\tpublic Dictionary<" + keyType + ", " + table.TableName + "> pairs = new Dictionary<" + keyType + ", " + table.TableName + ">();\n";
                str += "}\n";
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }
    }

    private static void SaveTable(DataTable table)
    {
        if (!Directory.Exists(BinaryManager.DATA_DIR + "Excel/"))
            Directory.CreateDirectory(BinaryManager.DATA_DIR + "Excel/");
        using (FileStream fs = File.Open(BinaryManager.DATA_DIR + "Excel/" + table.TableName + ".table", FileMode.OpenOrCreate, FileAccess.Write))
        {
            string keyName = "";
            for (int i = 0; i < table.Columns.Count; i++)
                if (table.Rows[2][i].Equals("key"))
                {
                    keyName = table.Rows[0][i].ToString();
                    break;
                }
            byte[] bytes = BitConverter.GetBytes(keyName.Length);
            fs.Write(bytes, 0, bytes.Length);//存储键名长度
            bytes = Encoding.UTF8.GetBytes(keyName);
            fs.Write(bytes, 0, bytes.Length);//存储键名
            bytes = BitConverter.GetBytes(table.Rows.Count - 4);
            fs.Write(bytes, 0, bytes.Length);//存储行数

            for (int r = 4; r < table.Rows.Count; r++)
            {
                for (int c = 0; c < table.Columns.Count; c++)
                { 
                    switch (table.Rows[1][c].ToString())
                    {
                        case "int":
                            bytes = BitConverter.GetBytes(int.Parse(table.Rows[r][c].ToString()));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case "float":
                            bytes = BitConverter.GetBytes(float.Parse(table.Rows[r][c].ToString()));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case "bool":
                            bytes = BitConverter.GetBytes(bool.Parse(table.Rows[r][c].ToString()));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case "string":
                            string str = table.Rows[r][c].ToString();
                            bytes = BitConverter.GetBytes(str.Length);
                            fs.Write(bytes, 0, bytes.Length);//存储字符串长度
                            bytes = Encoding.UTF8.GetBytes(str);
                            fs.Write(bytes, 0, bytes.Length);//存储字符串
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
