using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace SLOES.Model.Validator
{
    /// <summary>
    /// Student实体数据验证类
    /// </summary>
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(user => user.ChineseName).NotNull().NotEmpty().WithMessage(ErrorMessage.NOT_EMPTY_MSG);
            RuleFor(user => user.ChineseName).Length(0, 20).WithMessage(ErrorMessage.NOT_LENGTH_MSG);

            RuleFor(user => user.UserName).NotNull().NotEmpty().WithMessage(ErrorMessage.NOT_EMPTY_MSG);
            RuleFor(user => user.UserName).Length(0, 30).WithMessage(ErrorMessage.NOT_LENGTH_MSG);

            RuleFor(user => user.Password).NotNull().NotEmpty().WithMessage(ErrorMessage.NOT_EMPTY_MSG);
            RuleFor(user => user.Password).Length(0, 50).WithMessage(ErrorMessage.NOT_LENGTH_MSG);
        }
    }
}
