using FluentValidation;
using Homework16.Model;

namespace Homework16
{
    public class PersonValidator: AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(d => d.CreateDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("date should not exceed current date");

            RuleFor(f => f.FirstName)
                .NotEmpty().WithMessage("Firstname should not be empty")
                .Length(1, 50).WithMessage("Name should not be longer than 50 characters");

            RuleFor(f => f.LastName)
                .NotEmpty().WithMessage("Lastname should not be empty")
                .Length(1, 50).WithMessage("LastName should not be longer than 50 characters");

            RuleFor(j => j.Jobposition)
                .NotEmpty().WithMessage("position should not be empty");

            RuleFor(w => w.WorkExperience)
                .NotEmpty().WithMessage("workexperience should not be empty");

            RuleFor(s => s.Salary)
                .LessThan(10000).WithMessage("Salary should not be greater than 10000")
                .GreaterThan(0).WithMessage("Salary should be greater than 0");
        }   

    }
}
