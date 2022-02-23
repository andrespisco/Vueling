using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Person.Business.Contract.Services;
using VY.Person.Data.Contract.Entities;
using VY.Person.Data.Contract.Repositories;
using VY.Person.Dtos;
using VY.Person.Infraestructure.Contract;

namespace VY.Person.Business.Impl.Services
{
    public class PersonService : IPersonService
    {
        private readonly IFilePersonRepository _filePersonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IFilePersonRepository filePersonRepository, IMapper mapper, ILogger<PersonService> logger)
        {
            _filePersonRepository = filePersonRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //Here we need the OperationResult class for do our fucntions...
        /// <summary>
        /// Get Person by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationResult<PersonDto>> GetPersonAsync(Guid id)
        {
            OperationResult<PersonDto> result = new OperationResult<PersonDto>();

            try
            {
                var person = await _filePersonRepository.GetAsyncById(id);
                var personDto = _mapper.Map<PersonEntity, PersonDto>(person);
                result.SetResult(personDto);

            }
            catch (Exception ex)
            {
                result.AddError(9999, ex.Message, ex);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<OperationResult<IEnumerable<PersonDto>>> GetPersonsAsync()
        {
            OperationResult<IEnumerable<PersonDto>> result = new OperationResult<IEnumerable<PersonDto>>();

            try
            {
                var persons = await _filePersonRepository.GetAllAsync();
                var personsDto = _mapper.Map<IEnumerable<PersonEntity>, IEnumerable<PersonDto>>(persons);
                result.SetResult(personsDto);

            }
            catch (Exception ex)
            {
                result.AddError(9999, ex.Message, ex);
                _logger.LogError(ex, ex.Message);

            }
            return result;
        }

        public async Task<OperationResult<PersonDto>> AddPersonAsync(PersonDto personDto)
        {
            OperationResult<PersonDto> result = new OperationResult<PersonDto>();

            try
            {
                var person = _mapper.Map<PersonDto, PersonEntity>(personDto);
                person = await _filePersonRepository.AddAsync(person);
                var resultDto = _mapper.Map<PersonEntity, PersonDto>(person);
                result.SetResult(resultDto);

            }
            catch (Exception ex)
            {
                result.AddError(9999, ex.Message, ex);
            }

            return result;
        }

        public async Task<OperationResult<bool>> UpdatePersonAsync(Guid id, PersonDto personDto)
        {
            OperationResult<bool> result = new OperationResult<bool>();

            try
            {
                var person = _mapper.Map<PersonDto, PersonEntity>(personDto);
                person.Id = id;
                result.Result = await _filePersonRepository.UpdateAsync(person);
            }
            catch (Exception ex)
            {
                result.AddError(9999, ex.Message, ex);

            }
            return result;
        }

        public async Task<OperationResult<bool>> DeletePersonAsync(Guid id)
        {
            OperationResult<bool> result = new OperationResult<bool>();

            try
            {
                result.Result = await _filePersonRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                result.AddError(9999, ex.Message, ex);
            }
            return result;
        }


    }
}
