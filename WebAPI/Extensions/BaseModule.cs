using Domain.Common.Exceptions;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.Models.GeneralModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WebAPI.Extensions
{
    public class BaseModule : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BaseModule(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _webHostEnvironment = webHostEnvironment;
        }
        #region Return Response Delegate

        protected IResult CreateResponse(Func<IResult> function)
        {
            IResult response;
            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException dbEx)
            {
                response = Results.Extensions.InternalServerProblem(new ErrorResponseModel
                {
                    Message = dbEx.InnerException?.Message ?? dbEx.Message,
                    Result = new ErrorDetailResponseModel()
                    {
                        ExceptionMessage = dbEx?.Message,
                        StackTrace = dbEx?.StackTrace,
                        ExceptionMessageDetail = dbEx?.InnerException?.Message,
                        ReferenceErrorCode = dbEx?.HResult.ToString(),
                        ValidationErrors = null
                    },
                    InternalResults = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                });
            }
            catch (ClientException customEx)
            {
                var ErrorModel = new ErrorResponseModel
                {
                    Message = customEx.ExceptionMessage,
                    InternalResults = customEx.Results,
                    Result = new ErrorDetailResponseModel()
                    {
                        ExceptionMessage = customEx?.Message,
                        StackTrace = customEx?.StackTrace,
                        ExceptionMessageDetail = customEx?.InnerException?.Message,
                        ReferenceErrorCode = customEx?.HResult.ToString(),
                        ValidationErrors = null
                    },
                    StatusCode = customEx?.StatusCode ?? HttpStatusCode.InternalServerError,
                    Success = false
                };
                switch (customEx.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        response = Results.BadRequest(ErrorModel);
                        break;
                    case HttpStatusCode.NotFound:
                        response = Results.NotFound(ErrorModel);
                        break;
                    case HttpStatusCode.UnprocessableEntity:
                        response = Results.UnprocessableEntity(ErrorModel);
                        break;
                    case HttpStatusCode.NotModified:
                        response = Results.Conflict(ErrorModel);
                        break;

                    default:
                        response = Results.Extensions.InternalServerProblem(ErrorModel);
                        break;
                }
            }
            catch (ValidationException ex)
            {
                response = Results.BadRequest(new ErrorResponseModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = new ErrorDetailResponseModel()
                    {
                        ExceptionMessage = ex.InnerException?.Message,
                        ExceptionMessageDetail = ex.InnerException?.Message,
                        ReferenceErrorCode = ex.HResult.ToString(),
                        ValidationErrors = (ex.InnerException as ValidationException)?.Errors
                    },
                    InternalResults = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException is ValidationException)
                {
                    response = Results.BadRequest(new ErrorResponseModel
                    {
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Result = new ErrorDetailResponseModel()
                        {
                            ExceptionMessage = ex.InnerException?.Message,
                            ExceptionMessageDetail = ex.InnerException?.Message,
                            ReferenceErrorCode = ex.HResult.ToString(),
                            ValidationErrors = (ex.InnerException as ValidationException)?.Errors
                        },
                        InternalResults = null,
                        StatusCode = HttpStatusCode.BadRequest,
                        Success = false
                    });
                }
                else
                {
                    response = Results.Extensions.InternalServerProblem(new ErrorResponseModel
                    {
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Result = new ErrorDetailResponseModel()
                        {
                            ExceptionMessage = ex.InnerException?.Message,
                            ExceptionMessageDetail = ex.InnerException?.Message,
                            ReferenceErrorCode = ex.HResult.ToString(),
                            ValidationErrors = null
                        },
                        InternalResults = null,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Success = false
                    });
                }
            }
            return response;
        }
        protected async Task<IResult> CreateResponseAsync(Func<Task<IResult>> function)
        {
            IResult response;
            try
            {
                response = await function.Invoke();
            }
            catch (DbUpdateException dbEx)
            {
                response = Results.Extensions.InternalServerProblem(new ErrorResponseModel
                {
                    Message = dbEx.InnerException?.Message ?? dbEx.Message,
                    Result = new ErrorDetailResponseModel()
                    {
                        ExceptionMessage = dbEx?.Message,
                        StackTrace = dbEx?.StackTrace,
                        ExceptionMessageDetail = dbEx?.InnerException?.Message,
                        ReferenceErrorCode = dbEx?.HResult.ToString(),
                        ValidationErrors = null
                    },
                    InternalResults = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                });
            }
            catch (ClientException customEx)
            {
                var ErrorModel = new ErrorResponseModel
                {
                    Message = customEx.ExceptionMessage,
                    InternalResults = customEx.Results,
                    Result = new ErrorDetailResponseModel()
                    {
                        ExceptionMessage = customEx?.Message,
                        StackTrace = customEx?.StackTrace,
                        ExceptionMessageDetail = customEx?.InnerException?.Message,
                        ReferenceErrorCode = customEx?.HResult.ToString(),
                        ValidationErrors = null
                    },
                    StatusCode = customEx?.StatusCode ?? HttpStatusCode.InternalServerError,
                    Success = false
                };
                switch (customEx.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        response = Results.BadRequest(ErrorModel);
                        break;
                    case HttpStatusCode.NotFound:
                        response = Results.NotFound(ErrorModel);
                        break;
                    case HttpStatusCode.UnprocessableEntity:
                        response = Results.UnprocessableEntity(ErrorModel);
                        break;
                    case HttpStatusCode.NotModified:
                        response = Results.Conflict(ErrorModel);
                        break;

                    default:
                        response = Results.Extensions.InternalServerProblem(ErrorModel);
                        break;
                }
            }
            catch (ValidationException ex)
            {
                response = Results.BadRequest(new ErrorResponseModel
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Result = new ErrorDetailResponseModel()
                    {
                        ExceptionMessage = ex.InnerException?.Message,
                        ExceptionMessageDetail = ex.InnerException?.Message,
                        ReferenceErrorCode = ex.HResult.ToString(),
                        ValidationErrors = (ex.InnerException as ValidationException)?.Errors
                    },
                    InternalResults = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException is ValidationException)
                {
                    response = Results.BadRequest(new ErrorResponseModel
                    {
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Result = new ErrorDetailResponseModel()
                        {
                            ExceptionMessage = ex.InnerException?.Message,
                            ExceptionMessageDetail = ex.InnerException?.Message,
                            ReferenceErrorCode = ex.HResult.ToString(),
                            ValidationErrors = (ex.InnerException as ValidationException)?.Errors
                        },
                        InternalResults = null,
                        StatusCode = HttpStatusCode.BadRequest,
                        Success = false
                    });
                }
                else
                {
                    response = Results.Extensions.InternalServerProblem(new ErrorResponseModel
                    {
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Result = new ErrorDetailResponseModel()
                        {
                            ExceptionMessage = ex.InnerException?.Message,
                            ExceptionMessageDetail = ex.InnerException?.Message,
                            ReferenceErrorCode = ex.HResult.ToString(),
                            ValidationErrors = null
                        },
                        InternalResults = null,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Success = false
                    });
                }
            }
            return response;
        }

        #endregion

    }
    public static class ResultExtensions
    {
        public static IResult InternalServerProblem(this IResultExtensions resultExtensions, ErrorResponseModel errorResponseModel)
        {
            return Results.Problem(new ProblemDetails()
            {
                Status = 500,
                Title = errorResponseModel?.Result?.ExceptionMessage,
                Detail = errorResponseModel?.Result?.ExceptionMessageDetail,
                Instance = errorResponseModel?.Result?.ReferenceErrorCode,
                Type = errorResponseModel?.Result?.ReferenceErrorCode,
            });
        }
        public static IResult UnAuthorizedProblem(this IResultExtensions resultExtensions, ErrorResponseModel errorResponseModel)
        {
            return Results.Problem(new ProblemDetails()
            {
                Status = 401,
                Title = errorResponseModel?.Result?.ExceptionMessage,
                Detail = errorResponseModel?.Result?.ExceptionMessageDetail,
                Instance = errorResponseModel?.Result?.ReferenceErrorCode,
                Type = errorResponseModel?.Result?.ReferenceErrorCode,
            });
        }
    }
}
