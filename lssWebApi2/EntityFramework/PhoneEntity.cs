﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PhoneEntity
    {
        public long PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string Extension { get; set; }
        public long AddressId { get; set; }
        public long? PhoneEntityNumber { get; set; }

        public virtual AddressBook Address { get; set; }

    }
}