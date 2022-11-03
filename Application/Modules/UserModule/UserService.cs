using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Common.Models.UserModule;
using Domain.Common.RequestModels.UserRequests;
using Domain.Common.Utilities;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IUserModule;
using Domain.IServices.IHelperServices;
using System.Net;

namespace Application.Modules.UserModule
{
    public class UserService : IUserService
    {
        #region Constructors and Locals

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IFileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        #endregion

        #region GET

        public async Task<UserDto> GetRequestAsync(int id)
        {
            try
            {
                var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == id && x.IsActive == true && x.IsDeleted == false, x => x.Role);
                if (thisUser == null)
                {
                    throw new ClientException("No User Found", HttpStatusCode.NotFound);
                }
                return await Task.FromResult(_mapper.Map<UserDto>(thisUser)) ?? new UserDto();
            }
            catch (Exception ex)
            {
                return new UserDto();
            }
        }
        public async Task<UserDto> GetCurrentUserRequestAsync()
        {
            try
            {
                var userId = Convert.ToInt32(_currentUserService.UserID);
                var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == userId && x.IsActive == true && x.IsDeleted == false, x => x.Role);
                var response = _mapper.Map<UserDto>(thisUser) ?? new UserDto();

                if (!string.IsNullOrEmpty(thisUser.ImageKey))
                {
                    response.ImageKey = _fileUploadService.GetFileCompleteUrl(thisUser.ImageKey);
                }

                if (thisUser.fk_RoleID.Equals(2))
                {
                    response.CorporateProfile = _mapper.Map<UserCorporateProfileDto>(_unitOfWork.UserRepository.GetUserCorporateProfile(thisUser.ID));
                }

                if (thisUser.fk_RoleID.Equals(3))
                {
                    response.CandidateProfile = _mapper.Map<UserCandidateProfileDto>(_unitOfWork.UserRepository.GetUserCandidateProfile(thisUser.ID));
                }

                return response;
            }
            catch (Exception ex)
            {
                return new UserDto();
            }
        }

        #endregion

        #region ADD

        public async Task<UserDto> AddRequestAsync(UpsertUserRequest model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ClientException("Invalid UserName", HttpStatusCode.NotFound);
            }
            else
            {
                if (DoesExistAsync(model.UserName).Result)
                {
                    throw new ClientException("User Already Exists", HttpStatusCode.NotFound);
                }
                else
                {
                    var thisUser = _mapper.Map<User>(model);
                    thisUser.Password = PasswordHasher.GeneratePasswordHash(GetRandomPassword());
                    await _unitOfWork.UserRepository.AddAsync(thisUser);
                    _unitOfWork.Complete();
                    return _mapper.Map<UserDto>(thisUser);
                }
            }
        }

        #endregion

        #region UPDATE

        public async Task<UserDto> UpdateRequestAsync(UpsertUserRequest model)
        {
            var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == model.ID && x.IsActive == true && x.IsDeleted == false);
            if (thisUser == null)
            {
                throw new ClientException("No User Found", HttpStatusCode.NotFound);
            }
            else
            {
                thisUser.FirstName = model.FirstName;
                thisUser.LastName = model.LastName;
                thisUser.PhoneNumber = model.PhoneNumber;
                thisUser.UserName = model.UserName;
                thisUser.fk_RoleID = model.fk_RoleID;
                _unitOfWork.Complete();
            }
            return await Task.FromResult(_mapper.Map<UserDto>(thisUser));
        }
        public async Task<UserDto> UpdateProfilePictureRequestAsync(UpsertProfilePictureRequest model)
        {
            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == userId && x.IsActive == true && x.IsDeleted == false);
            if (thisUser == null)
            {
                throw new ClientException("No User Found", HttpStatusCode.NotFound);
            }
            else
            {
                if (model.ProfilePicture != null)
                {
                    var key = _fileUploadService.UploadFile(model.ProfilePicture);
                    thisUser.ImageKey = string.IsNullOrEmpty(key) ? thisUser.ImageKey : key;
                }

                _unitOfWork.Complete();
            }
            var response = _mapper.Map<UserDto>(thisUser);
            if (!string.IsNullOrEmpty(thisUser.ImageKey))
            {
                response.ImageKey = _fileUploadService.GetFileCompleteUrl(thisUser.ImageKey);
            }
            return response;
        }
        public async Task<UserDto> UpdateCurrentUserRequestAsync(UpdateCurrentUserRequest model)
        {
            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == userId && x.IsActive == true && x.IsDeleted == false);
            if (thisUser == null)
            {
                throw new ClientException("No User Found", HttpStatusCode.NotFound);
            }
            else
            {
                thisUser.FirstName = model.FirstName;
                thisUser.LastName = model.LastName;
                thisUser.UserName = model.UserName;
                thisUser.DOB = model.DOB;
                thisUser.Ethnicity = model.Ethnicity;
                thisUser.Gender = model.Gender;
                thisUser.Address = model.Address;
                thisUser.PhoneNumber = model.PhoneNumber;

                if (model.Image != null)
                {
                    var key = _fileUploadService.UploadFile(model.Image);
                    thisUser.ImageKey = string.IsNullOrEmpty(key) ? thisUser.ImageKey : key;
                }

                _unitOfWork.Complete();
            }
            var response = _mapper.Map<UserDto>(thisUser);
            if (!string.IsNullOrEmpty(thisUser.ImageKey))
            {
                response.ImageKey = _fileUploadService.GetFileCompleteUrl(thisUser.ImageKey);
            }
            return response;
        }

        #endregion

        #region USERS - Register/login

        public async Task<RegisterRequestModel> RegisterRequestAsync(RegisterRequestModel request)
        {
            if (request.RoleID.Equals(1))
            {
                throw new ClientException("Not Allowed", HttpStatusCode.MethodNotAllowed);
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new ClientException("Passwords Dont Match !!", HttpStatusCode.BadRequest);
            }

            if (await _unitOfWork.UserRepository.ExistsAsync(x => x.UserName == request.UserName) || await _unitOfWork.UserRepository.ExistsAsync(x => x.UserName == request.UserName))
            {
                throw new ClientException("User with this User Name already Exists", HttpStatusCode.BadRequest);
            }

            if (await _unitOfWork.UserRepository.ExistsAsync(x => x.UserName == request.UserName) || await _unitOfWork.UserRepository.ExistsAsync(x => x.UserName == request.UserName))
            {
                throw new ClientException("User with this UserName already Exists", HttpStatusCode.BadRequest);
            }
            var newUser = new User
            {
                FirstName = request?.FirstName,
                LastName = request?.LastName,
                UserName = request?.UserName,
                PhoneNumber = request?.PhoneNumber,
                DOB = request.DOB,
                Password = PasswordHasher.GeneratePasswordHash(request.Password),
                fk_RoleID = request.RoleID,
            };

            if (request.RoleID.Equals(2))
            {
                newUser.CorporateProfile = new UserCorporateProfile
                {
                    Corporate = new Domain.Entities.CorporateModule.Corporate
                    {
                        Company = request.Company,
                        HeadQuarterContact = request.HeadQuarterContact,
                        HeadQuarterName = request.HeadQuarterName
                    }
                };
            }
            else
            {
                newUser.CandidateProfile = new UserCandidateProfile()
                {
                    ContactEmail = request.UserName,
                    ContactPhone = request.PhoneNumber
                };
            }

            await _unitOfWork.UserRepository.AddAsync(newUser);
            _unitOfWork.Complete();
            return request;
        }
        public async Task<UserLoginResponseModel> ForgetPasswordRequestAsync(string UserName)
        {
            try
            {
                var DoesThisUserExist = await _unitOfWork.UserRepository.ExistsAsync(x => x.UserName == UserName && x.IsActive == true && x.IsDeleted == false);
                if (!DoesThisUserExist)
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "No User Found !",
                    };
                }
                else
                {
                    var newPassword = GetRandomPassword();

                    var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.UserName == UserName && x.IsActive == true && x.IsDeleted == false, x => x.Role);
                    thisUser.Password = PasswordHasher.GeneratePasswordHash(newPassword);
                    _unitOfWork.Complete();

                    var appSettings = await _unitOfWork.AppsettingsRepository.GetAllAsync();
                    var emailTemplate = EmailUtility.GetEmailTemplateFromFile("ForgetPasswordTemplate");
                    emailTemplate = emailTemplate.Replace("{newPassword}", newPassword);
                    EmailUtility.SendMail(UserName, emailTemplate, "Acme Inc. - Forget Password Request", emailTemplate.ToString(), appSettings.ToList());

                    return new UserLoginResponseModel
                    {
                        Success = true,
                        Message = "Sent password reset. Please check your email",
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResponseModel
                {
                    Success = false,
                    Message = "Some thing went wrong",
                };
            }
        }
        public async Task<UserLoginResponseModel> ChangePasswordAsync(UpdatePasswordRequestModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NewPassword))
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "New Password Cannot Be Empty",
                    };
                }
                if (string.IsNullOrEmpty(request.CurrentPassword))
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "Current password Cannot Be Empty",
                    };
                }
                if (string.IsNullOrEmpty(request.PasswordConfirmation))
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "Password Confirmtion Cannot Be Empty",
                    };
                }
                if (request.CurrentPassword == request.PasswordConfirmation)
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "Old password Cannot Be Same with New Password",
                    };
                }
                var userId = Convert.ToInt32(_currentUserService.UserID);
                var DoesThisUserExist = await _unitOfWork.UserRepository.ExistsAsync(x => x.ID == userId && x.IsActive == true && x.IsDeleted == false);
                if (!DoesThisUserExist)
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "No User Found !",
                    };
                }
                else
                {
                    var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == userId && x.IsActive == true && x.IsDeleted == false, x => x.Role);
                    if (request.NewPassword == request.PasswordConfirmation)
                    {
                        if (PasswordHasher.VerifyHash(request.CurrentPassword, thisUser.Password))
                        {
                            thisUser.Password = PasswordHasher.GeneratePasswordHash(request.NewPassword);
                            _unitOfWork.Complete();
                            return new UserLoginResponseModel
                            {
                                Success = true,
                                Message = "Password Has Been Changed",
                            };
                        }
                        else
                        {
                            return new UserLoginResponseModel
                            {
                                Success = false,
                                Message = "Old password does not match",
                            };
                        }
                    }
                    else
                    {
                        return new UserLoginResponseModel
                        {
                            Success = false,
                            Message = "New password does not match with Confirm Password",
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResponseModel
                {
                    Success = false,
                    Message = "Some thing went wrong",
                };
            }
        }
        public async Task<UserLoginResponseModel> LoginRequestAsync(string UserName, string Password)
        {
            try
            {
                var DoesThisUserExist = await _unitOfWork.UserRepository.ExistsAsync(x => x.UserName == UserName && x.IsActive == true && x.IsDeleted == false);
                if (!DoesThisUserExist)
                {
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "No User Found !",
                    };
                }
                else
                {
                    var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.UserName == UserName && x.IsActive == true && x.IsDeleted == false, x => x.Role);
                    if (PasswordHasher.VerifyHash(Password, thisUser.Password ?? string.Empty))
                    {
                        return new UserLoginResponseModel
                        {
                            User = thisUser,
                            Success = true,
                            Message = "User Logged in Succesfully",
                        };
                    }
                    return new UserLoginResponseModel
                    {
                        Success = false,
                        Message = "Password Does Not Match !",
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResponseModel
                {
                    Success = false,
                    Message = "Some thing went wrong",
                };
            }
        }

        #endregion

        #region Exist/bool

        public async Task<bool> DoesExistAsync(int Id)
        {
            var IsExist = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == Id && x.IsActive == true && x.IsDeleted == false);
            if (IsExist != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DoesExistAsync(string UserName)
        {
            var IsExist = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.UserName == UserName && x.IsActive == true && x.IsDeleted == false);
            if (IsExist != null)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region DELETE

        public async Task<bool> DeleteRequestAsync(int id)
        {
            var thisUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.ID == id && x.IsActive == true && x.IsDeleted == false);
            if (thisUser != null)
            {
                thisUser.IsDeleted = true;
            }
            else
            {
                throw new ClientException("No User Found", HttpStatusCode.NotFound);
            }
            _unitOfWork.Complete();
            return true;
        }

        #endregion

        private static string GetRandomPassword()
        {
            return Guid.NewGuid().ToString().Replace("-", "")[..8];
        }
    }
}
