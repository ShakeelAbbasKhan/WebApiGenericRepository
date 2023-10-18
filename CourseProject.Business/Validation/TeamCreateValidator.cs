﻿using CourseProject.Common.Dtos.Team;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Validation
{
    public class TeamCreateValidator : AbstractValidator<TeamCreate>
    {
        public TeamCreateValidator()
        {
            RuleFor(teamCreate => teamCreate.Name).NotEmpty().MaximumLength(50);
        }
    }
}
