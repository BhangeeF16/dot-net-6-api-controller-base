using Domain.Common.Extensions;
using Domain.Entities.GeneralModule;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common.RequestModels.JobPostModule
{
    public class CreateJobRequest
    {
        public string? JobTitle { get; set; }
        public string? Description { get; set; }
        public WorkPlaceType WorkPlaceType { get; set; }
        public JobType JobType { get; set; }
        public string? Location { get; set; }

        public List<string>? Tags { get; set; }
        public IFormFileCollection? Files { get; set; }
    }

    public class CreateJobRequestValidator : AbstractValidator<CreateJobRequest>
    {
        public CreateJobRequestValidator()
        {
            RuleFor(c => c.JobTitle).ValidateProperty();
            RuleFor(c => c.WorkPlaceType).ValidateEnumProperty();
            RuleFor(c => c.JobType).ValidateEnumProperty();
        }
    }
}
