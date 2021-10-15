using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace service.server.Services
{
    public class UserValidation : AbstractValidator<AppUser>
    {


        public UserValidation()
        {

            RuleFor(x => x.FirstName).Length(1,250).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().Length(1,250).WithMessage("Please specify a Email").NotEmpty();
            RuleFor(x => x.PhoneNumber).Length(9,12).NotEmpty();

        }




    }
}