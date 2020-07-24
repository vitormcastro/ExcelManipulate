using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace ExcelManipulate.Auxiliar
{
    public static class utils
    {

        public static List<string> OrganizeList(List<string> list)
        {
            List<string> ret = new List<string>();
            ret.Add("Nome;E-mail;Ano");
            string name = string.Empty;
            string pemail = string.Empty;
            string eemail = string.Empty;
            bool twoEmail;
            string ano = string.Empty;
            for (int i = 1; i < list.Count; i++)
            {
                twoEmail = false;
                string[] ls = list[i].Split(';');
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = ls[0];
                    if (!string.IsNullOrWhiteSpace(ls[3]))
                    {
                        pemail = ls[3];
                        twoEmail = true;
                    }

                    if (!string.IsNullOrWhiteSpace(ls[2]))
                    {
                        eemail = ls[2];
                    }
                    else
                    {
                        twoEmail = false;
                    }
                    if (twoEmail && eemail == pemail)
                    {
                        if (PersonalEmail(eemail))
                        {
                            eemail = string.Empty;
                        }
                        else
                        {
                            pemail = string.Empty;
                        }
                    }
                    ano = ls[1];
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(pemail))
                    {
                        ret.Add(name + ";" + pemail + ";" + ano);
                    }
                    if (!string.IsNullOrWhiteSpace(eemail))
                    {
                        ret.Add(name + ";" + eemail + ";" + ano);
                    }
                    eemail = string.Empty;
                    pemail = string.Empty;
                    name = ls[0];
                    ano = ls[1];
                    if (!string.IsNullOrWhiteSpace(ls[3]))
                    {
                        pemail = ls[3];
                        twoEmail = true;
                    }
                    if (!string.IsNullOrWhiteSpace(ls[2]))
                    {
                        eemail = ls[2];
                    }
                    else
                    {
                        twoEmail = false;
                    }
                    if (twoEmail && eemail == pemail)
                    {
                        if (PersonalEmail(eemail))
                        {
                            eemail = string.Empty;
                        }
                        else
                        {
                            pemail = string.Empty;
                        }
                    }

                }
                if (i == (list.Count - 1))
                {
                    if (!string.IsNullOrWhiteSpace(pemail))
                    {
                        ret.Add(name + ";" + pemail + ";" + ano);
                    }
                    if (!string.IsNullOrWhiteSpace(eemail))
                    {
                        ret.Add(name + ";" + eemail + ";" + ano);
                    }
                }
            }
            return ret;
        }

        public static List<string> TirarDuplicidade(List<string> list)
        {
            List<string> ret = new List<string>();
            ret.Add("Nome;E-mail;Ano");
            string name = string.Empty;
            int pano = 0;
            int eano = 0;
            string pemail = string.Empty;
            string eemail = string.Empty;
            for (int i = 1; i < list.Count; i++)
            {
                string[] ls = list[i].Split(';');
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = ls[0];
                    if (!string.IsNullOrWhiteSpace(ls[1]))
                    {
                        if (PersonalEmail(ls[1].Split('@')[1]))
                        {
                            pemail = ls[1];
                            pano = Convert.ToInt32(ls[2].Trim());
                        }
                        else
                        {
                            eemail = ls[1];
                            eano = Convert.ToInt32(ls[2].Trim());
                        }
                    }
                }
                else if (name == ls[0])
                {
                    if (!string.IsNullOrWhiteSpace(ls[1]))
                    {
                        if (PersonalEmail(ls[1].Split('@')[1]))
                        {
                            if (string.IsNullOrWhiteSpace(pemail))
                            {
                                pemail = ls[1];
                                pano = Convert.ToInt32(ls[2].Trim());
                            }
                            else  if(pemail == ls[1])
                            {
                                int ano = Convert.ToInt32(ls[2].Trim());
                                pano = ano > pano ? ano : pano;
                            }
                            else
                            {
                                int ano = Convert.ToInt32(ls[2].Trim());
                                if(ano > pano)
                                {
                                    pemail = ls[1];
                                    pano = Convert.ToInt32(ls[2].Trim());
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(eemail))
                            {
                                eemail = ls[1];
                                eano = Convert.ToInt32(ls[2].Trim());
                            }
                            else if (eemail == ls[1])
                            {
                                int ano = Convert.ToInt32(ls[2].Trim());
                                eano = ano > eano ? ano : eano;
                            }
                            else
                            {
                                int ano = Convert.ToInt32(ls[2].Trim());
                                if (ano > eano)
                                {
                                    eemail = ls[1];
                                    eano = Convert.ToInt32(ls[2].Trim());
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(pemail))
                    {
                        ret.Add(name + ";" + pemail + ";" + pano);
                    }
                    if (!string.IsNullOrWhiteSpace(eemail))
                    {
                        ret.Add(name + ";" + eemail + ";" + eano);
                    }
                    eemail = string.Empty;
                    eano = 0;
                    pemail = string.Empty;
                    pano = 0;
                    name = ls[0];
                    if (!string.IsNullOrWhiteSpace(ls[1]))
                    {
                        if (PersonalEmail(ls[1].Split('@')[1]))
                        {
                            pemail = ls[1];
                            pano = Convert.ToInt32(ls[2].Trim());
                        }
                        else
                        {
                            eemail = ls[1];
                            eano = Convert.ToInt32(ls[2].Trim());
                        }
                    }
                }
                if (i == (list.Count - 1))
                {
                    if (!string.IsNullOrWhiteSpace(pemail))
                    {
                        ret.Add(name + ";" + pemail + ";" + pano);
                    }
                    if (!string.IsNullOrWhiteSpace(eemail))
                    {
                        ret.Add(name + ";" + eemail + ";" + eano);
                    }
                }
            }
            return ret;
        }
        public static List<string> ClearThePast(List<string> list)
        {
            List<string> ret = new List<string>();
            ret.Add("Nome;E-mail;Ano");
            string name = string.Empty;
            int pano = 0;
            int eano = 0;
            string pemail = string.Empty;
            string eemail = string.Empty;
            for (int i = 1; i < list.Count; i++)
            {
                string[] ls = list[i].Split(';');
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = ls[4];
                    if (!string.IsNullOrWhiteSpace(ls[1]))
                    {
                        if (PersonalEmail(ls[1].Split('@')[1]))
                        {
                            pemail = ls[1];
                            pano = Convert.ToInt32(ls[2].Trim());
                        }
                        else
                        {
                            eemail = ls[1];
                            eano = Convert.ToInt32(ls[2].Trim());
                        }
                    }


                }
                else if (name == ls[4])
                {
                    if (!string.IsNullOrWhiteSpace(ls[1]))
                    {
                        if (PersonalEmail(ls[1].Split('@')[1]))
                        {
                            if (string.IsNullOrWhiteSpace(pemail))
                            {
                                pemail = ls[1];
                                pano = Convert.ToInt32(ls[2].Trim());
                            }
                            else
                            {
                                int year = Convert.ToInt32(ls[2].Trim());
                                if (year > pano)
                                {
                                    pemail = ls[1];
                                    pano = Convert.ToInt32(ls[2].Trim());
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(eemail))
                            {
                                eemail = ls[1];
                                eano = Convert.ToInt32(ls[2].Trim());
                            }
                            else
                            {
                                int year = Convert.ToInt32(ls[2].Trim());
                                if (year > eano)
                                {
                                    eemail = ls[1];
                                    eano = Convert.ToInt32(ls[2].Trim());
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(pemail))
                    {
                        ret.Add(name + ";" + pemail + ";" + pano);
                    }
                    if (!string.IsNullOrWhiteSpace(eemail))
                    {
                        ret.Add(name + ";" + eemail + ";" + eano);
                    }
                    eemail = string.Empty;
                    eano = 0;
                    pemail = string.Empty;
                    pano = 0;
                    name = ls[4];
                    if (!string.IsNullOrWhiteSpace(ls[1]))
                    {
                        if (PersonalEmail(ls[1].Split('@')[1]))
                        {
                            pemail = ls[1];
                            pano = Convert.ToInt32(ls[2].Trim());
                        }
                        else
                        {
                            eemail = ls[1];
                            eano = Convert.ToInt32(ls[2].Trim());
                        }
                    }
                }
                if (i == (list.Count - 1))
                {
                    if (!string.IsNullOrWhiteSpace(pemail))
                    {
                        ret.Add(name + ";" + pemail + ";" + pano);
                    }
                    if (!string.IsNullOrWhiteSpace(eemail))
                    {
                        ret.Add(name + ";" + eemail + ";" + eano);
                    }
                }
            }
            return ret;
        }



        private static bool PersonalEmail(string email)
        {
            switch (email)
            {
                case "gmail.com":
                case "zohomail.com":
                case "aol.com":
                case "outlook.com":
                case "outlook.com.br":
                case "hotmail.com":
                case "gmx.com":
                case "gmx.us":
                case "@yahoo.com":
                case "protonmail.com":
                case "protonmail.ch":
                case "@bol.com.br":
                    return true;
                default:
                    return false;
            }
        }

        public static string[] GetList(string arquivo)
        {
            if (File.Exists(arquivo))
            {
                var list = File.ReadAllLines(arquivo, UnicodeEncoding.UTF7);
                return list;
            }

            throw new Exception("Arquivo não encontrado");
        }

        public static DataTable getCsvList(string arquivo)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(arquivo, UnicodeEncoding.UTF7))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return csvData;
        }

        public static void WriteCsv(string[] list, string arquivo)
        {
            /* string resp = "";
             bool puloulinha = true;
             foreach (string l in list)
             {
                 if (string.IsNullOrEmpty(l))
                 {
                     if (puloulinha)
                     {
                         resp = resp + Environment.NewLine;
                     }
                     else
                     {
                         resp = resp + ";" + Environment.NewLine;
                     }
                     puloulinha = true;
                 }
                 else
                 {
                     if (puloulinha)
                     {
                         resp = resp + l;
                     }
                     else
                     {
                         resp = resp + ";" + l;
                     }
                     puloulinha = false;
                 }
             }*/
            File.WriteAllLines(arquivo, list, UnicodeEncoding.UTF8);
        }
    }
}
