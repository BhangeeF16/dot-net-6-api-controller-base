using AutoMapper;
using Domain.Common.DTO;
using Domain.Common.Exceptions;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices;

namespace Application.EntityServices
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<List<RoleDto>> GetAllRoleRequestAsync()
        {
            try
            {
                var GetRoleList = await _unitOfWork.RoleRepository.GetAllAsync();
                return _mapper.Map<List<RoleDto>>(GetRoleList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RoleDto> GetRoleByIdRequestAsync(int Id)
        {
            try
            {
                var thisRole = await _unitOfWork.RoleRepository.GetFirstOrDefaultAsync(x => x.ID == Id);
                if (thisRole == null)
                {
                    throw new ClientException("No Role Found", System.Net.HttpStatusCode.NotFound);
                }
                return await Task.FromResult(_mapper.Map<RoleDto>(thisRole)) ?? new RoleDto();
            }
            catch (Exception ex)
            {
                return new RoleDto();
            }
        }
        public async Task<RoleDto> GetCurrentUserRole()
        {
            try
            {
                var RoleId = Convert.ToInt32(_currentUserService.RoleId);
                var thisUserRole = await _unitOfWork.RoleRepository.GetFirstOrDefaultAsync(x => x.ID == RoleId);
                return _mapper.Map<RoleDto>(thisUserRole) ?? new RoleDto();
            }
            catch (Exception ex)
            {
                return new RoleDto();
            }
        }
    }
}
