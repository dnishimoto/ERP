﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class AddressBook
    {
        public AddressBook()
        {
            Buyer = new HashSet<Buyer>();
            Carrier = new HashSet<Carrier>();
            Customer = new HashSet<Customer>();
            Emails = new HashSet<Emails>();
            Employee = new HashSet<Employee>();
            GeneralLedger = new HashSet<GeneralLedger>();
            LocationAddress = new HashSet<LocationAddress>();
            Phones = new HashSet<Phones>();
            Supervisor = new HashSet<Supervisor>();
            Supplier = new HashSet<Supplier>();
        }

        public long AddressId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? PeopleXrefId { get; set; }
        public string CategoryCodeChar1 { get; set; }
        public string CategoryCodeChar2 { get; set; }
        public string CategoryCodeChar3 { get; set; }
        public int? CategoryCodeInt1 { get; set; }
        public int? CategoryCodeInt2 { get; set; }
        public int? CategoryCodeInt3 { get; set; }
        public DateTime? CategoryCodeDate1 { get; set; }
        public DateTime? CategoryCodeDate2 { get; set; }
        public DateTime? CategoryCodeDate3 { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Buyer> Buyer { get; set; }
        public virtual ICollection<Carrier> Carrier { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Emails> Emails { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<GeneralLedger> GeneralLedger { get; set; }
        public virtual ICollection<LocationAddress> LocationAddress { get; set; }
        public virtual ICollection<Phones> Phones { get; set; }
        public virtual ICollection<Supervisor> Supervisor { get; set; }
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}