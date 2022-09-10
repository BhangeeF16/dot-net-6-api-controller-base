using FluentValidation;

namespace Domain.Common.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> ValidatePermission<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage($"{nameof(TProperty)} Permission is required");
        }

        public static IRuleBuilderOptions<T, TProperty> ValidateNotNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage($"{nameof(TProperty)} is required");
        }
        public static IRuleBuilderOptions<T, TProperty> ValidateProperty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().NotNull().WithMessage($"{nameof(TProperty)} is required");
        }
        public static IRuleBuilderOptions<T, string> ValidateEmail<T, TProperty>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.EmailAddress().WithMessage("Email is invalid");
        }
    }
}
