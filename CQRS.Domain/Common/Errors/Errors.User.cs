﻿using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Common.Errors;

public static partial class Errors
{
    public static class User 
    {

        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Eamil is already used"
            );
       
    }

}
