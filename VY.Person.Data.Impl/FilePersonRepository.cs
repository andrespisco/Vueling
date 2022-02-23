using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VY.Person.Data.Contract.Entities;
using VY.Person.Data.Contract.Repositories;

namespace VY.Person.Data.Impl
{
    public class FilePersonRepository : IFilePersonRepository
    {
        private readonly string _fileName;

        //Ctor of class
        public FilePersonRepository(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// For append all text of my file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual async Task AppendTextAsync(string path)
        {
            await File.AppendAllTextAsync(_fileName, path);
        }
        /// <summary>
        /// For write all text in file
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public virtual async Task WriteAllTextAsync(IEnumerable<string> lines)
        {
            await File.WriteAllLinesAsync(_fileName, lines);
        }

        public virtual async Task<string[]> ReadAllLinesAsync()
        {
            return await File.ReadAllLinesAsync(_fileName);
        }

        public async Task<PersonEntity> AddAsync(PersonEntity personEntity)
        {
            personEntity.Id = Guid.NewGuid();
            string data = string.Format("{0};{1};{2}\n", personEntity.Id, personEntity.Name, personEntity.LastName);
            await AppendTextAsync(data); //This way i can append my person...
            return personEntity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (!FileExists()) return false;

            bool found = false;
            var lines = (await ReadAllLinesAsync()).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts[0] == id.ToString()) //If part[0] id is equal that id  then has found the id for remove
                {
                    lines.RemoveAt(i);
                    found = true;
                    break;
                }
            }
            await WriteAllTextAsync(lines);
            return found;

        }
        /// <summary>
        /// Method to verificate that files exists
        /// </summary>
        /// <returns></returns>
        public virtual bool FileExists()
        {
            return File.Exists(_fileName);
        }
        /// <summary>
        /// Get the Person by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PersonEntity> GetAsyncById(Guid id)
        {
            if (!FileExists()) return null;

            string[] lines = await ReadAllLinesAsync();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts[0] == id.ToString())
                {
                    return new PersonEntity { Id = Guid.Parse(parts[0]), Name = parts[1], LastName = parts[2] };

                }
            }
            return null;
        }

        public async Task<IEnumerable<PersonEntity>> GetAllAsync()
        {
            List<PersonEntity> listPerson = new List<PersonEntity>();

            if (File.Exists(_fileName)) return listPerson;

            string[] lines = await ReadAllLinesAsync();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                var person = new PersonEntity { Id = Guid.Parse(parts[1]), Name = parts[2], LastName = parts[3] };
                listPerson.Add(person);
            }
            return listPerson;
        }

        public async Task<bool> UpdateAsync(PersonEntity personEntity)
        {
            if (FileExists()) return false;

            bool found = false;

            string[] lines = await ReadAllLinesAsync();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts[0] == personEntity.Id.ToString())
                {
                    lines[i] = string.Format("{0};{1};{2}\n", personEntity.Id, personEntity.Name, personEntity.LastName);
                    found = true;
                    break;
                }
            }

            await WriteAllTextAsync(lines);
            return found;
        }
    }
}
