using FluentValidation;
using HotelierProject.WebUI.Dtos.GuestDto;

namespace HotelierProject.WebUI.ValidationRules.GuestValidationRules
{
    public class UpdateGuestValidator : AbstractValidator<UpdateGuestDto>
    {
        public UpdateGuestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim Alanı Boş Geçilemez!");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir Alanı Boş Geçilemez!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("En Az 3 Karakterlik İsim Girişi Yapmalısınız!");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("En Az 3 Karakterlik Soyisim Girişi Yapmalısınız!");
            RuleFor(x => x.City).MinimumLength(30).WithMessage("En Az 3 Karakterlik Şehir Girişi Yapmalısınız!");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("En Fazla 30 Karakterlik İsim Girişi Yapmalısınız!");
            RuleFor(x => x.Surname).MaximumLength(30).WithMessage("En Fazla 30 Karakterlik Soyisim Girişi Yapmalısınız!");
            RuleFor(x => x.City).MaximumLength(30).WithMessage("En Fazla 30 Karakterlik Şehir Girişi Yapmalısınız!");
        }
    }
}
