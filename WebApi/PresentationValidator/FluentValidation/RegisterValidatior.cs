using Entities.Dtos;
using FluentValidation;
using FluentValidation.Validators;

namespace WebApi.PresentationValidator.FluentValidation
{
	public class RegisterValidatior : AbstractValidator<UserForRegisterDto>
	{
        public RegisterValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş keçilə bilməz.");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Soyad boş keçilə bilməz.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Istifadəçi adı boş keçilə bilməz.");
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email bölməsi standart email strukturuna uyğun deyil.").NotEmpty().WithMessage("Ad boş keçilə bilməz.");
            RuleFor(x => x.SelectedDepartment).NotEmpty().WithMessage("Departament bölməsi seçilməsi mütləqdir.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifrə boş keçilə bilməz.");
            RuleFor(x => x.RePassword).NotEmpty().WithMessage("Təkrar şifrə boş keçilə bilməz.").Must(RePasswordMustBeEqual)
			.WithMessage("Təkrar şifrə ilə şifrə eyni deyil.")
			.When(x => !string.IsNullOrEmpty(x.Password));
		}

		private bool RePasswordMustBeEqual(UserForRegisterDto dto, string confirmPassword)
		{
			return dto.Password == confirmPassword;
		}
	}
}