using ApniDukan.Domain.DTOs.Auth;
using FluentValidation;

namespace ApniDukan.Application.Validator.Auth
{
    public class ShopOwnerRegisterRequestValidator : AbstractValidator<ShopOwnerRegisterRequest>
    {
        public ShopOwnerRegisterRequestValidator()
        {
            // Owner Information

            RuleFor(so => so.Name)
                .NotEmpty().WithMessage("Owner name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(?:\+91[\s-]?)?[6-9]\d{9}$")
                .WithMessage("Please enter a valid Indian mobile number.");

            RuleFor(x => x.AlternativeNumber)
                .Matches(@"^(?:\+91[\s-]?)?[6-9]\d{9}$")
                .When(x => !string.IsNullOrWhiteSpace(x.AlternativeNumber))
                .WithMessage("Please enter a valid alternative mobile number.");

            RuleFor(x => x.CurrentAddress)
                .NotEmpty().WithMessage("Current address is required.")
                .MaximumLength(250).WithMessage("Current address cannot exceed 250 characters.");

            RuleFor(x => x.PermanentAddress)
                .MaximumLength(250).WithMessage("Permanent address cannot exceed 250 characters.");

            // Shop Information

            RuleFor(x => x.ShopName)
                .NotEmpty().WithMessage("Shop name is required.");

            RuleFor(x => x.ShopDescription)
                .NotEmpty().WithMessage("Shop description is required.")
                .MaximumLength(500).WithMessage("Shop description cannot exceed 500 characters.");

            RuleFor(x => x.ShopAddress)
                .NotEmpty().WithMessage("Shop address is required.")
                .MaximumLength(250).WithMessage("Shop address cannot exceed 250 characters.");

            RuleFor(x => x.ShopImageUrl)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.ShopImageUrl))
                .WithMessage("Please provide a valid shop image URL.");
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                   && (uri.Scheme == Uri.UriSchemeHttp ||
                       uri.Scheme == Uri.UriSchemeHttps);
        }
    }
    }
}
