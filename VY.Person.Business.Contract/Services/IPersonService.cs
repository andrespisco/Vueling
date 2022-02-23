using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VY.Person.Dtos;
using VY.Person.Infraestructure.Contract;

namespace VY.Person.Business.Contract.Services
{
    public interface IPersonService
    {
        Task<OperationResult<PersonDto>> AddPersonAsync(PersonDto personDto);
        Task<OperationResult<bool>> DeletePersonAsync(Guid id);
        Task<OperationResult<PersonDto>> GetPersonAsync(Guid id);
        Task<OperationResult<IEnumerable<PersonDto>>> GetPersonsAsync();
        Task<OperationResult<bool>> UpdatePersonAsync(Guid id, PersonDto personDto);
    }
}