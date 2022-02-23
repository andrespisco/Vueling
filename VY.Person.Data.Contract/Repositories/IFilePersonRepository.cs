using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VY.Person.Data.Contract.Entities;

namespace VY.Person.Data.Contract.Repositories
{
    public interface IFilePersonRepository
    {
        Task<PersonEntity> AddAsync(PersonEntity personEntity);
        Task AppendTextAsync(string path);
        Task<bool> DeleteAsync(Guid id);
        bool FileExists();
        Task<IEnumerable<PersonEntity>> GetAllAsync();
        Task<PersonEntity> GetAsyncById(Guid id);
        Task<string[]> ReadAllLinesAsync();
        Task<bool> UpdateAsync(PersonEntity personEntity);
        Task WriteAllTextAsync(IEnumerable<string> lines);
    }
}