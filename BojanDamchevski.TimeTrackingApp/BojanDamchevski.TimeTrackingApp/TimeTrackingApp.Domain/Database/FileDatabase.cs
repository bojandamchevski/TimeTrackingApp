using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TimeTrackingApp.Domain.Models;

namespace TimeTrackingApp.Domain.Database
{
    public class FileDatabase<T> : IDatabase<T> where T : User
    {
        private string _folderPath;
        private string _filePath;
        private int _id;

        public FileDatabase()
        {
            _id = 1;
            _folderPath = @"..\..\..\Db";
            _filePath = _folderPath + @"\users.json";
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                WriteData(new List<T>());
            }

        }

        public User ActivateAccount(User activatedUser)
        {
            List<T> dbEntities = GetAllEntitiesFromDb();
            T dbEntity = dbEntities.FirstOrDefault(x => x.Id == activatedUser.Id);
            dbEntity.AccountActivity = true;
            UpdateUser(dbEntity);
            return dbEntity;
        }

        public User CheckUser(string username, string password)
        {
            List<T> dbEntities = GetAllEntitiesFromDb();
            T dbEntity = dbEntities.FirstOrDefault(x => x.Username == username && x.Password == password);
            return dbEntity;
        }

        public void InsertUser(T user)
        {
            List<T> dbEntities = GetAllEntitiesFromDb();
            if (dbEntities == null)
            {
                dbEntities = new List<T>();
                _id = 1;
            }
            if (dbEntities.Count == 0)
            {
                _id = 1;
            }
            else
            {
                _id = dbEntities.Count + 1;
            }
            user.Id = _id;
            dbEntities.Add(user);
            UpdateUser(user);
        }

        public User RemoveUser(User removedUser)
        {
            List<T> dbEntities = GetAllEntitiesFromDb();
            T entityDb = dbEntities.FirstOrDefault(x => x.Id == removedUser.Id);
            if (entityDb == null)
            {
                throw new Exception($"The user does not exist");
            }
            entityDb.AccountActivity = false;
            UpdateUser(entityDb);
            return entityDb;
        }

        public void UpdateUser(T user)
        {
            List<T> dbEntities = GetAllEntitiesFromDb();
            T entityDb = dbEntities.FirstOrDefault(x => x.Username == user.Username && x.Id == user.Id);
            dbEntities.Remove(entityDb);
            entityDb = user;
            dbEntities.Add(user);
            WriteData(dbEntities);
        }

        private void WriteData(List<T> dbEntities)
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(dbEntities));
            }
        }

        private List<T> GetAllEntitiesFromDb()
        {
            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                return JsonConvert.DeserializeObject<List<T>>(streamReader.ReadToEnd());
            }
        }
    }
}
