﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Account
{
    public class AccountPaymentMethodDeleteRequest
    {
        public string Username { get; set; }

        public string CardId { get; set; }

    }
}
