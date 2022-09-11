using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Manager;

public class ExcelTool
{
    static string EXCEL_PATH = Application.dataPath + "/Editor/Excel/";
    static string SCRIPT_PATH = Application.dataPath + "/Editor/Scripts/";

    [MenuItem("Tools/ExcelToTable")]
    static void ExcelToBin()
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
                    CreateTupleClass(table);
                    CreateTableClass(table);                   
                }
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("ExcelToTable is Completed\n" + BinaryManager.DATA_DIR);
    }

    static void CreateTupleClass(DataTable table)
    {
        if (!File.Exists(SCRIPT_PATH + table.TableName + ".cs"))
        {
            Dictionary<string, string> name_type = new Dictionary<string, string>();
            for (int i = 0; i < table.Columns.Count; i++)
                name_type.Add(table.Rows[2][i].ToString(), table.Rows[3][i].ToString());
            using (FileStream fs = new FileStream(SCRIPT_PATH + table.TableName + ".cs", FileMode.Create, FileAccess.Write))
            {
                string str = "public class " + table.TableName + "\n{\n";
                foreach (var pair in name_type)
                    str += ("\tpublic " + pair.Value + " " + pair.Key + ";\n");
                str += "}\n";
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }
    }

    static void CreateTableClass(DataTable table)
    {
        if (!File.Exists(SCRIPT_PATH + table.TableName + "Table.cs"))
        {
            string keyType = "";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Rows[1][i].Equals("key"))
                {
                    keyType = table.Rows[3][i].ToString();
                    break;
                }
            }
            using (FileStream fs = new FileStream(SCRIPT_PATH + table.TableName + "Table.cs", FileMode.Create, FileAccess.Write))
            {
                string str = "using System.Collections.Generic;\n\npublic class " + table.TableName + "Table\n{\n";
                str += "\tpublic Dictionary<" + keyType + ", " + table.TableName + "> tuples = new Dictionary<" + keyType + ", " + table.TableName + ">();\n";
                str += "}\n";
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }
    }

    static void SaveTable(DataTable table)
    {
        if (!Directory.Exists(BinaryManager.DATA_DIR + "Excel/"))
            Directory.CreateDirectory(BinaryManager.DATA_DIR + "Excel/");
        using (FileStream fs = File.Open(BinaryManager.DATA_DIR + "Excel/" + table.TableName + ".table", FileMode.OpenOrCreate, FileAccess.Write))
        {
            string keyName = "";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Rows[1][i].Equals("key"))
                {
                    keyName = table.Rows[2][i].ToString();
                    break;
                }
            }
            byte[] bytes = BitConverter.GetBytes(keyName.Length);
            fs.Write(bytes, 0, bytes.Length);//存储键名长度
            bytes = Encoding.UTF8.GetBytes(keyName);
            fs.Write(bytes, 0, bytes.Length);//存储键名
            bytes = BitConverter.GetBytes(table.Rows.Count - 4);
            fs.Write(bytes, 0, bytes.Length);//存储行数
            for (int row = 4; row < table.Rows.Count; row++)
            {
                for (int column = 0; column < table.Columns.Count; column++)
                { 
                    switch (table.Rows[3][column].ToString())
                    {
                        case "int":
                            bytes = BitConverter.GetBytes(int.Parse(table.Rows[row][column].ToString()));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case "float":
                            bytes = BitConverter.GetBytes(float.Parse(table.Rows[row][column].ToString()));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case "bool":
                            bytes = BitConverter.GetBytes(bool.Parse(table.Rows[row][column].ToString()));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case "string":
                            string str = table.Rows[row][column].ToString();
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
