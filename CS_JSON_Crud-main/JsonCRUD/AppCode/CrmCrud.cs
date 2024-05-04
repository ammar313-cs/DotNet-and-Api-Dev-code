using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using System;

namespace JsonCRUD
{
    public class CrmCrud
    {
        public List<Crm>? friendsOfMine = null;

        public CrmCrud()
        {
            LoadJsonFromDisk();
        }

        public bool SaveJsonToDisk()
        {
            string fileName = @"~\mydata.json";

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(friendsOfMine);

            if (File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            System.IO.File.WriteAllText(fileName, json);

            return (true);
        }

        public List<Crm> JsonFileFindRecord(Crm obj)
        {
            LoadJsonFromDisk();
            if (friendsOfMine != null)
                return (friendsOfMine);

            return null;
        }

        public void LoadJsonFromDisk()
        {

            string fileName = @"~\mydata.json";
            if (File.Exists(fileName)==true)
            {
                var list = JsonConvert.DeserializeObject<List<Crm>>
                        (File.ReadAllText(fileName));

                if(list != null)
                    friendsOfMine = list;
                else
                    friendsOfMine = new List<Crm>();
            }
            else
            {
                friendsOfMine = new List<Crm>();
            }

        }

        public List<CrmDto> GetAllDisplayData()
        {
            LoadJsonFromDisk();
            int index = 0;
            if (friendsOfMine != null)
            {
                List<CrmDto> all = new List<CrmDto>();

                for (int i = 0; i < friendsOfMine.Count; i++)
                {
                    CrmDto rec = new CrmDto();
                    rec.Id = friendsOfMine[i].Id;
                    rec.FirstName = friendsOfMine[i].FirstName;
                    rec.LastName = friendsOfMine[i].LastName;
                    rec.CellPhoneNum = friendsOfMine[i].CellPhoneNum;
                    rec.DisplayCreateDate = friendsOfMine[i].CreateDate.ToString("MM/dd/yyyy");
                    rec.SelectedIndex = index;
                    index++;
                    all.Add(rec);
                }

                return (all);
            }

            return null;
        }

        public (bool Success, string[]? errors) JsonFileInsert(Crm obj)
        {
            LoadJsonFromDisk();
            if(friendsOfMine == null)
                friendsOfMine = new List<Crm>();

            var test = friendsOfMine.FirstOrDefault(r => r.Id == obj.Id);
            if (test == null)
            {
                // add item to list
                friendsOfMine.Add(obj);

                // serializeList to disk
                bool rv = SaveJsonToDisk();
                if(rv != true)
                {
                    MessageBox.Show("Error writing json to disk");
                    string[] msg1 =
                    {
                        "Error writing JSON to disk",
                        $"{obj.Id} {obj.FirstName} is in memory object"
                    };
                    return (false, msg1);
                }

                return (true,null);
            }

            string[] msg2 =
                    {
                        "Error inserting record into memory object",
                        $"{obj.Id} {obj.FirstName} already exists"
                    };
            return (false,msg2);
        }

        public (bool Success, string[]? errors) JsonFileUpdate(Crm obj)
        {
            LoadJsonFromDisk();
            if (friendsOfMine == null)
                friendsOfMine = new List<Crm>();

            var test = friendsOfMine.FirstOrDefault(r => r.Id == obj.Id);
            if (test != null)
            {
                test.FirstName = obj.FirstName;
                test.LastName = obj.LastName;
                test.CellPhoneNum = obj.CellPhoneNum;
                test.LastTouchDate = DateTime.Now;

                // serializeList to disk
                bool rv = SaveJsonToDisk();
                if (rv != true)
                {
                    MessageBox.Show("Error writing json to disk");
                    string[] msg1 =
                    {
                        "Error writing JSON to disk",
                        $"{obj.Id} {obj.FirstName} is in memory object"
                    };
                    return (false, msg1);
                }

                return (true, null);
            }

            string[] msg2 =
                    {
                        "Error updating record into memory object",
                        $"{obj.Id} {obj.FirstName} already exists"
                    };
            return (false, msg2);

        }

        public (bool Success, string[]? errors) JsonFileDelete(Crm obj)
        {
            LoadJsonFromDisk();
            if (friendsOfMine == null)
                friendsOfMine = new List<Crm>();

            var test = friendsOfMine.FirstOrDefault(r => r.Id == obj.Id);
            if (test != null)
            {
                // add item to list
                friendsOfMine.RemoveAll(r => r.Id == test.Id);

                // serializeList to disk
                bool rv = SaveJsonToDisk();
                if (rv != true)
                {
                    MessageBox.Show("Error writing json to disk");
                    string[] msg1 =
                    {
                        "Error writing JSON to disk",
                        $"{obj.Id} {obj.FirstName} is in memory object"
                    };
                    return (false, msg1);
                }

                return (true, null);
            }

            string[] msg2 =
                    {
                        "Error inserting record into memory object",
                        $"{obj.Id} {obj.FirstName} already exists"
                    };
            return (false, msg2);

        }
    }
}
