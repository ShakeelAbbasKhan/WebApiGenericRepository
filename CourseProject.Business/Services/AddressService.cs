using AutoMapper;
using CourseProject.Business.Exceptions;
using CourseProject.Business.Validation;
using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Interface;
using CourseProject.Common.Model;
using CourseProject.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Services
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Address> _genericRepository;
        private readonly AddressCreateValidator _addressCreateValidator;
        private readonly AddressUpdateValidator _addressUpdateValidator;

        public AddressService(IMapper mapper, IGenericRepository<Address> genericRepository, AddressCreateValidator addressCreateValidator, AddressUpdateValidator addressUpdateValidator)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _addressCreateValidator = addressCreateValidator;
            _addressUpdateValidator = addressUpdateValidator;
        }

        public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
        {
            await _addressCreateValidator.ValidateAndThrowAsync(addressCreate);
            var entity  = _mapper.Map<Address>(addressCreate);
            await _genericRepository.InsertAsync(entity);
            await _genericRepository.SaveChangesAsync();
            return entity.Id;

        }

        public async Task DeleteAddressAsync(AddressDelete addressDelete)
        {
            var entity = await _genericRepository.GetByIdAsync(addressDelete.Id , (address) => address.Employees) ;

            if(entity == null)
            {
                throw new AddressNotFoudException(addressDelete.Id);
            }

            if(entity.Employees.Count > 0)
            { 
                throw new DependentEmployeesExistException(entity.Employees);
            }
            _genericRepository.Delete(entity);
            await _genericRepository.SaveChangesAsync();
        }

       

        public async Task<AddressGet> GetAddressAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if(entity == null)
                throw new AddressNotFoudException(id);

            return _mapper.Map<AddressGet>(entity);
        }

        public async Task<List<AddressGet>> GetAddressesAsync()
        {
            var entities = await _genericRepository.GetAsync(null, null);
            return _mapper.Map<List<AddressGet>>(entities);
        }

        public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
        {
            await _addressUpdateValidator.ValidateAndThrowAsync(addressUpdate);

            var existingAddress = await _genericRepository.GetByIdAsync(addressUpdate.Id);

            if (existingAddress == null)
                throw new AddressNotFoudException(addressUpdate.Id);

            var entity = _mapper.Map<Address>(addressUpdate);
            _genericRepository.UpdateAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }
    }
}
