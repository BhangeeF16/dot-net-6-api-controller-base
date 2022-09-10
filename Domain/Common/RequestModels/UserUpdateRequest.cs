using Domain.Entities.GeneralModule;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Domain.Common.RequestModels
{
    public class UserUpdateRequest
    {
        [FromForm]
        public string? FirstName { get; set; }
        [FromForm]
        public string? LastName { get; set; }
        [FromForm]
        public string? Email { get; set; }
        [FromForm]
        public string? UserName { get; set; }
        [FromForm]
        public DateTime DOB { get; set; }
        [FromForm]
        public string? PhoneNumber { get; set; }
        [FromForm]
        public Gender? Gender { get; set; }
        [FromForm]
        public Ethnicity? Ethnicity { get; set; }
        [FromForm]
        public string? Address { get; set; }
        [FromForm]
        public IFormFile? Image { get; set; }

        //public static ValueTask<UserUpdateRequest?> BindAsync(HttpContext context, ParameterInfo parameter)
        //{
        //    var FormData = context.Request.Form;

        //    int.TryParse(FormData["gender"], out var gender);
        //    int.TryParse(FormData["ethnicity"], out var ethnicity);

        //    var dob = DateTime.Parse(FormData["dob"]);

        //    var result = new UserUpdateRequest
        //    {
        //        FirstName = FormData["firstName"],
        //        LastName = FormData["lastName"],
        //        Email = FormData["email"],
        //        UserName = FormData["userName"],
        //        PhoneNumber = FormData["phoneNumber"],
        //        Gender = (Gender?)gender ?? Entities.GeneralModule.Gender.PreferNotToAnswer,
        //        Ethnicity = (Ethnicity?)ethnicity ?? Entities.GeneralModule.Ethnicity.White,
        //        Image = FormData.Files.Any() ? FormData.Files[0] : null,
        //        Address = FormData["address"],
        //        DOB = dob,
        //    };

        //    //var result = new UserUpdateRequest
        //    //{
        //    //    FirstName = FormData["firstName"],
        //    //    LastName = FormData["lastName"],
        //    //    Email = FormData["email"],
        //    //    UserName = FormData["userName"],
        //    //    PhoneNumber = FormData["phoneNumber"],
        //    //    Gender = FormData["gender"],
        //    //    Ethnicity = FormData["ethnicity"],
        //    //    Image = FormData.Files.Any() ? FormData.Files[0] : null,
        //    //    Address = FormData["address"],
        //    //    DOB = FormData["dob"],
        //    //};
        //    return ValueTask.FromResult<UserUpdateRequest?>(result);
        //}
    }
    public class UpdateOperatorRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UpdateOperatorRequestValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().WithMessage("First name is required");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        }
    }
}
